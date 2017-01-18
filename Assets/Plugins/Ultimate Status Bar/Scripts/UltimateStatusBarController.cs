/* Written by Kaz Crowe */
/* UltimateStatusBarController.cs ver 1.0.5 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent( typeof( CanvasGroup ) )]
public class UltimateStatusBarController : MonoBehaviour
{
	/* -----< ASSIGNED VARIABLES >----- */ // add static refs for updating name and icon for targets and such.
	public Text statusBarText;
	public Image statusBarIcon;

	/* -----< SIZE AND PLACEMENT >----- */
	RectTransform controllerTransform;
	public enum ScalingAxis{ Height, Width }
	public ScalingAxis scalingAxis = ScalingAxis.Width;
	public float statusBarSize = 1.75f;
	public float spacingX = 0.0f, spacingY = 0.0f;
	public bool preserveAspect = false;
	public Image targetImage;
	Sprite s_TargetImage;
	Vector2 aspectRatio = Vector2.zero;
	public float xRatio = 1.0f, yRatio = 1.0f;

	/* -----< STYLE AND OPTIONS >----- */
	public enum InitialState{ Enabled, Disabled }
	public InitialState initialState = InitialState.Enabled;

	public enum TimeoutOption { None, Fade, Animation }// EDIT: Change this to a ToggleOption. Also, remake the idleTimeout boolean...
	public TimeoutOption timeoutOption = TimeoutOption.None;
	public float idleSeconds = 2.0f;
	public float enabledDuration = 1.0f, disabledDuration = 1.0f;
	float enabledSpeed = 1.0f, disabledSpeed = 1.0f;
	bool isFading = false, isCountingDown = false;
	float countdownTime = 0.0f;
	CanvasGroup statusBarGroup;

	// Version 1.0.3+ Additions // For the option of FaceCamera, need an option for modifying either the canvas, or this rect transform.
	public enum PositioningOption{ ScreenSpace, FollowCameraRotation, Disabled }
	public PositioningOption positioningOption = PositioningOption.ScreenSpace;

	// Add options for finding the targeted camera.
	public enum FindBy{ Camera, Name, Tag }
	public FindBy findBy = FindBy.Camera;
	public string targetName = "Main Camera";
	public string targetTag = "MainCamera";
	public Transform cameraTransform;

	UltimateStatusBar[] statusBars = new UltimateStatusBar[ 0 ];
	public Animator statusBarAnimator;
	
	/* -----< SCRIPT REFERENCE >----- */
	static Dictionary<string, UltimateStatusBarController> StatusBarControllers = new Dictionary<string, UltimateStatusBarController>();
	public string controllerName = string.Empty;


	void Awake ()
	{
		// If the controllerName is assigned, then register this controller for reference.
		if( Application.isPlaying == true && controllerName != string.Empty )
			RegisterController( controllerName, gameObject.GetComponent<UltimateStatusBarController>() );
	}

	void Start ()
	{
		// If the game isn't running, then this is still within the editor, so return.
		if( Application.isPlaying == false )
			return;

		// Get the CanvasGroup component for Enable/Disable options.
		statusBarGroup = GetComponent<CanvasGroup>();
		if( statusBarGroup == null )
		{
			gameObject.AddComponent( typeof( CanvasGroup ) );
			statusBarGroup = GetComponent<CanvasGroup>();
		}

		// If the user wants the status bar disabled from start, do that here.
		if( initialState == InitialState.Disabled )
			statusBarGroup.alpha = 0.0f;
		// Else show the status bar.
		else
			RequestShowStatusBar();

		// If the user wants to use the positioning of this script...
		if( positioningOption == PositioningOption.ScreenSpace )
		{
			// If the parent canvas does not have an Updater component, then add one so that the controller will update when the screen size changes.
			if( !GetParentCanvas().GetComponent<UltimateStatusBarUpdater>() )
				GetParentCanvas().gameObject.AddComponent( typeof( UltimateStatusBarUpdater ) );

			// Call UpdatePositioning() to apply the users positioning options on Start().
			UpdatePositioning();
		}
		// Else if they are wanting to follow the rotation of a camera, then start the coroutine.
		else if( positioningOption == PositioningOption.FollowCameraRotation )
		{
			controllerTransform = GetParentCanvas().GetComponent<RectTransform>();
			StartCoroutine( "FollowCameraRotation" );
		}

		// If the user is wanting to use some sort of Timeout option...
		if( timeoutOption != TimeoutOption.None )
		{
			// If the user is using fade, then configure the speeds for enabling and disabling the status bar.
			if( timeoutOption == TimeoutOption.Fade )
			{
				enabledSpeed = 1.0f / enabledDuration;
				disabledSpeed = 1.0f / disabledDuration;
			}

			// Store all child status bars for reference.
			statusBars = GetComponentsInChildren<UltimateStatusBar>();

			for( int i = 0; i < statusBars.Length; i++ )
			{
				statusBars[ i ].controllerEvent += RequestShowStatusBar;
			}
		}
	}
	
	void RegisterController ( string controllerName, UltimateStatusBarController controller )
	{
		// If the static list already contains a key that matches the controllerName, then remove it.
		if( StatusBarControllers.ContainsKey( controllerName ) )
			StatusBarControllers.Remove( controllerName );

		// Add the controllerName and controller to the static list of Controllers for reference.
		StatusBarControllers.Add( controllerName, controller );
	}

	// This function will configure a Vector2 for the position of the image.
	Vector2 ConfigureImagePosition ( Vector2 baseSize )
	{
		// Create a temporary Vector2 to modify and return.
		Vector2 tempPosVector;
		
		// Fix the custom spacing variables to something that is easy to work with.
		float fixedCSX = spacingX / 100;
		float fixedCSY = spacingY / 100;
		
		// Create two floats for applying our spacers according to our canvas size.
		float positionSpacerX = Screen.width * fixedCSX - ( baseSize.x * fixedCSX );
		float positionSpacerY = Screen.height * fixedCSY - ( baseSize.y * fixedCSY );
		
		// Apple the position spacers to the temporary Vector2.
		tempPosVector.x = positionSpacerX;
		tempPosVector.y = positionSpacerY;
		
		// Return the updated Vector2.
		return tempPosVector;
	}

	void RequestShowStatusBar ()
	{
		// If the user doesn't have the timeout option enabled, return.
		if( timeoutOption == TimeoutOption.None )
			return;

		if( statusBars.Length == 0 )
			statusBars = GetComponentsInChildren<UltimateStatusBar>();

		for( int i = 0; i < statusBars.Length; i++ )
		{
			if( statusBars[ i ].keepAwake == true )
			{
				isCountingDown = false;
				StopCoroutine( "ShowStatusBarCountdown" );
				ShowStatusBar();
				return;
			}
		}

		// If the timeout is currently not counting down, then start the countdown timer.
		if( isCountingDown == false )
			StartCoroutine( "ShowStatusBarCountdown" );
		// Else reset the countdownTime to the max time.
		else
			countdownTime = idleSeconds;

		// Show the status bar.
		ShowStatusBar();
	}

	// This function is used only to find the canvas parent if it is not located on the root object.
	Canvas GetParentCanvas ()
	{
		// Store the current parent.
		Transform parent = transform.parent;

		// Loop through parents as long as there is one.
		while( parent != null )
		{ 
			// If there is a Canvas component, return the component.
			if( parent.transform.GetComponent<Canvas>() )
				return parent.transform.GetComponent<Canvas>();

			// Else, shift to the next parent.
			parent = parent.transform.parent;
		}

		// If no Canvas was found on any parents, inform the user and return nothing.
		Debug.LogError( "No Canvas component is attached to the parent gameObects. Please make sure there is a Canvas component on the root canvas." );
		return null;
	}

	IEnumerator FadeInHandler ()
	{
		// Set isFading to true so that other functions will know that this coroutine is running.
		isFading = true;

		// Store the current value of the Canvas Group's alpha.
		float currentAlpha = statusBarGroup.alpha;

		// Loop for the duration of the enabled duration variable.
		for( float t = 0.0f; t < 1.0f && isFading == true; t += Time.deltaTime * enabledSpeed )
		{
			statusBarGroup.alpha = Mathf.Lerp( currentAlpha, 1.0f, t );

			// If the speed is NaN, then break the coroutine.
			if( float.IsInfinity( enabledSpeed ) )
				break;

			yield return null;
		}
		// If the coroutine was not interupted, then apply the final value.
		if( isFading == true )
			statusBarGroup.alpha = 1.0f;

		// Set isFading to false so that other functions know that this coroutine is not running anymore.
		isFading = false;
	}

	// For details on this coroutine, see the FadeInHandler() function above.
	IEnumerator FadeOutHandler ()
	{
		isFading = true;
		float currentAlpha = statusBarGroup.alpha;
		for( float t = 0.0f; t < 1.0f && isFading == true; t += Time.deltaTime * disabledSpeed )
		{
			statusBarGroup.alpha = Mathf.Lerp( currentAlpha, 0.0f, t );
			if( float.IsInfinity( disabledSpeed ) )
				break;

			yield return null;
		}
		if( isFading == true )
			statusBarGroup.alpha = 0.0f;

		isFading = false;
	}

	IEnumerator ShowStatusBarCountdown ()
	{
		// Set isCountingDown to true for checks.
		isCountingDown = true;

		// Set the starting time.
		countdownTime = idleSeconds;

		// While the countdownTime is greater than zero, continue counting down.
		while( countdownTime > 0 )
		{
			countdownTime -= Time.deltaTime;
			yield return null;
		}

		// Once the countdown is complete, set isCountingDown to false and hide the status bar.
		isCountingDown = false;
		HideStatusBar();
	}

	bool CanvasErrors ()
	{
		Canvas parentCanvas = GetParentCanvas();

		// If parentCanvas is null, then return true for errors.
		if( parentCanvas == null )
			return true;

		// If the parentCanvas is not enabled, then return true for errors.
		if( parentCanvas.enabled == false )
			return true;

		// If the canvas' renderMode is not the needed one, then return true for errors.
		if( parentCanvas.renderMode != RenderMode.ScreenSpaceOverlay )
			return true;

		// If the canvas has a CanvasScaler component and it is not the correct option.
		if( parentCanvas.GetComponent<CanvasScaler>() && parentCanvas.GetComponent<CanvasScaler>().uiScaleMode != CanvasScaler.ScaleMode.ConstantPixelSize )
			return true;

		return false;
	}

	IEnumerator FollowCameraRotation ()
	{
		bool continueFollowing = true;
		while( continueFollowing == true )
		{
			if( cameraTransform == null )
				cameraTransform = FindCamera();
			else
				controllerTransform.LookAt( controllerTransform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up );

			yield return null;
		}
	}

	Transform FindCamera ()
	{
		Transform tempTrans;
		if( findBy == FindBy.Camera )
			tempTrans = cameraTransform;
		else if( findBy == FindBy.Name && GameObject.Find( targetName ) )
			tempTrans = GameObject.Find( targetName ).GetComponent<Transform>();
		else if( findBy == FindBy.Tag && GameObject.FindGameObjectWithTag( targetTag ) )
			tempTrans = GameObject.FindGameObjectWithTag( targetTag ).GetComponent<Transform>();
		else
			tempTrans = null;

		if( tempTrans == null )
			Debug.LogError( gameObject.name + " - Ultimate Status Bar Controller could not find the targeted camera. Please make sure that the Transform/Name/Tag is correct for the FindBy type." );

		return tempTrans;
	}

	void TimeoutHandler ( bool state )
	{
		if( timeoutOption == TimeoutOption.Fade )
		{
			if( state == true )
				StartCoroutine( "FadeInHandler" );
			else
				StartCoroutine( "FadeOutHandler" );
		}
		else if( timeoutOption == TimeoutOption.Animation )
		{
			if( statusBarAnimator == null )
				return;

			if( state == false )
				statusBarAnimator.SetBool( "BarActive", false );
			else
				statusBarAnimator.SetBool( "BarActive", true );
		}
	}

	#if UNITY_EDITOR
	void Update ()
	{
		if( !Application.isPlaying )
			UpdatePositioning();
	}
	#endif

	#region Public Functions
	/* --------------------------------------------- *** PUBLIC FUNCTIONS FOR THE USER *** --------------------------------------------- */
	/// <summary>
	/// This function updates the size and positioning of the Status Bar.
	/// </summary>
	public void UpdatePositioning ()
	{
		// If the user doesn't want to use the positioning of this script, then return.
		if( positioningOption != PositioningOption.ScreenSpace || CanvasErrors() )
			return;

		// If controllerTransform is not assigned...
		if( controllerTransform == null )
		{
			// If there is no RectTransform component on this gameObject, then return.
			if( !GetComponent<RectTransform>() )
			{
				Debug.LogError( "There is no RectTransform component attached to this gameobject. Please make sure to apply this script to a uGUI element." );
				return;
			}

			// Assign controllerTransform to this gameObject's RectTransform.
			controllerTransform = GetComponent<RectTransform>();
		}

		// If the user is wanting to preserve the aspect ratio of the selected image...
		if( preserveAspect == true )
		{
			// If the targetImage variable has been left unassigned, then inform the user and return.
			if( targetImage == null )
				return;

			// Store the original sprite so it's size can be referenced.
			s_TargetImage = targetImage.sprite;

			if( s_TargetImage == null )
			{
				Debug.LogError( "The targeted image that was assigned in the Ultimate Status Bar Controller script located on the " + gameObject.name + " Game Object is null. Has it been deleted or moved?" );
				return;
			}

			// Store the raw values of the sprites ratio so that a smaller value can be configured.
			Vector2 rawRatio = new Vector2( s_TargetImage.rect.width, s_TargetImage.rect.height );

			// Temporary float to store the largest side of the sprite.
			float maxValue = rawRatio.x > rawRatio.y ? rawRatio.x : rawRatio.y;

			// Now configure the ratio based on the above information.
			aspectRatio.x = rawRatio.x / maxValue;
			aspectRatio.y = rawRatio.y / maxValue;
		}
		// Else store the sizing variables as our aspect ratio.
		else
			aspectRatio = new Vector2( xRatio, yRatio );

		// Store the calculation value of either Height or Width.
		float referenceSize = scalingAxis == ScalingAxis.Height ? Screen.height : Screen.width;

		// Configure a size for the image based on the Canvas's size and scale.
		float textureSize = referenceSize * ( statusBarSize / 10 );

		// Apply the configured size to the controllerTransform's sizeDelta.
		controllerTransform.sizeDelta = new Vector2( textureSize * aspectRatio.x, textureSize * aspectRatio.y );

		// CONFIGURE THE PIVOT SPACAH!!!
		Vector2 pivotSpacer = new Vector2( controllerTransform.sizeDelta.x * controllerTransform.pivot.x, controllerTransform.sizeDelta.y * controllerTransform.pivot.y );

		// Configure the position of the image according to the information that was gathered above.
		Vector2 imagePosition = ConfigureImagePosition( new Vector2( controllerTransform.sizeDelta.x, controllerTransform.sizeDelta.y ) );

		// Apply the positioning.
		controllerTransform.position = imagePosition + pivotSpacer;
	}

	/// <summary>
	/// Updates the name of the status bar.
	/// </summary>
	/// <param name="newName">The new name to apply to the Ultimate Status Bar.</param>
	public void UpdateStatusBarName ( string newName )
	{
		// If the statusBarText component is left unassigned, inform the user and return.
		if( statusBarText == null )
		{
			Debug.Log( "The Text for Status Bar Text must be assigned in order to update the name of the status bar. Please exit play mode " +
			          "and assign the Status Bar Text variable in the inspector." );
			return;
		}

		// Set the text to being the newName that the user has passed.
		statusBarText.text = newName;
	}
	
	/// <summary>
	/// Updates the icon shown on the status bar.
	/// </summary>
	/// <param name="newIcon">The new targeted icon to apply to the Ultimate Status Bar.</param>
	public void UpdateStatusBarIcon ( Sprite newIcon )
	{
		// If the statusBarIcon is left unassigned, then inform the user and return.
		if( statusBarIcon == null )
		{
			Debug.Log( "The Image for Status Bar Icon must be assigned in order to update the icon of the status bar. Please exit play mode " +
			          "and assign the Status Bar Icon variable in the inspector." );
			return;
		}

		// Apply the newIcon to the statusBarIcon.
		statusBarIcon.sprite = newIcon;
	}

	/// <summary>
	/// Shows the status bar.
	/// </summary>
	public void ShowStatusBar ()
	{
		// If there is no CanvasGroup, then return.
		if( statusBarGroup == null )// EDIT: Add the Canvas Group to the controller and assign the group var.
			return;

		// If the status bar is currently fading, then stop the FadeOutHandler.
		if( isFading == true )
			StopCoroutine( "FadeOutHandler" );

		if( timeoutOption != TimeoutOption.None )
			TimeoutHandler( true );
		else
			statusBarGroup.alpha = 1.0f;
	}

	/// <summary>
	/// Hides the status bar.
	/// </summary>
	public void HideStatusBar ()
	{
		// If the statusBarGroup isn't assigned, return.
		if( statusBarGroup == null )// EDIT: Add the Canvas Group to the controller and assign the group var.
			return;

		// If the status bar is currently fading, then stop the coroutine.
		if( isFading == true )
			StopCoroutine( "FadeInHandler" );

		if( timeoutOption != TimeoutOption.None )
			TimeoutHandler( false );
		else
			statusBarGroup.alpha = 0.0f;
	}
	/* ------------------------------------------- *** END PUBLIC FUNCTIONS FOR THE USER *** ------------------------------------------- */
	#endregion

	#region Static Functions
	/* --------------------------------------------- *** STATIC FUNCTIONS FOR THE USER *** --------------------------------------------- */
	/// <summary>
	/// This function updates the size and positioning of the Status Bar.
	/// </summary>
	/// <param name="controllerName">The name of the targeted Ultimate Status Bar Controller.</param>
	static public void UpdatePositioning ( string controllerName )
	{
		if( !ControllerConfirmed( controllerName ) )
			return;
		
		StatusBarControllers[ controllerName ].UpdatePositioning();
	}

	/// <summary>
	/// Updates the name of the status bar.
	/// </summary>
	/// <param name="controllerName">The name of the targeted Ultimate Status Bar Controller.</param>
	/// <param name="newName">The targeted string to apply to the text of the Ultimate Status Bar.</param>
	static public void UpdateStatusBarName ( string controllerName, string newName )
	{
		if( !ControllerConfirmed( controllerName ) )
			return;
		
		StatusBarControllers[ controllerName ].UpdateStatusBarName( newName );
	}

	/// <summary>
	/// Updates the sprite that is used for the icon of the Ultimate Status Bar.
	/// </summary>
	/// <param name="controllerName">The name of the targeted Ultimate Status Bar Controller.</param>
	/// <param name="newIcon">The targeted sprite to be applied to the Ultimate Status Bar.</param>
	static public void UpdateStatusBarIcon ( string controllerName, Sprite newIcon )
	{
		if( !ControllerConfirmed( controllerName ) )
			return;
		
		StatusBarControllers[ controllerName ].UpdateStatusBarIcon( newIcon );
	}

	/// <summary>
	/// Enables the visuals of the Ultimate Status Bar.
	/// </summary>
	/// <param name="controllerName">The name of the targeted Ultimate Status Bar Controller.</param>
	static public void ShowStatusBar ( string controllerName )
	{
		if( !ControllerConfirmed( controllerName ) )
			return;
		
		StatusBarControllers[ controllerName ].ShowStatusBar();
	}

	/// <summary>
	/// Disables the visuals of the Ultimate Status Bar.
	/// </summary>
	/// <param name="controllerName">The name of the targeted Ultimate Status Bar Controller.</param>
	static public void HideStatusBar ( string controllerName )
	{
		if( !ControllerConfirmed( controllerName ) )
			return;
		
		StatusBarControllers[ controllerName ].HideStatusBar();
	}

	static bool ControllerConfirmed ( string controllerName )
	{
		if( !StatusBarControllers.ContainsKey( controllerName ) )
		{
			Debug.LogWarning( "No Ultimate Status Bar Controller has been registered with the name: " + controllerName + "." );
			return false;
		}
		return true;
	}
	/* ------------------------------------------- *** END STATIC FUNCTIONS FOR THE USER *** ------------------------------------------- */
	#endregion
}

/* Written by Kaz Crowe */
/* UltimateStatusBarUpdater.cs ver 1.0.2 */
public class UltimateStatusBarUpdater : UIBehaviour
{
	UltimateStatusBarController[] allControllers;

	protected override void OnRectTransformDimensionsChange()
	{
		StartCoroutine( "YieldPositioning" );
	}

	IEnumerator YieldPositioning ()
	{
		yield return new WaitForEndOfFrame();

		allControllers = FindObjectsOfType( typeof( UltimateStatusBarController ) ) as UltimateStatusBarController[];
		foreach( UltimateStatusBarController cont in allControllers )
			cont.UpdatePositioning();
	}
}