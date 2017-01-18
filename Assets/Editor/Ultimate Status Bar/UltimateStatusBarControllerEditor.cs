/* Written by Kaz Crowe */
/* UltimateStatusBarControllerEditor.cs ver 1.0.3 */
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CanEditMultipleObjects]
[CustomEditor( typeof( UltimateStatusBarController ) )]
public class UltimateStatusBarControllerEditor : Editor
{
	string statusBarName = "Enter Name";

	/* ASSIGNED COMPONENTS */
	AnimBool AssignedVariables;
	SerializedProperty statusBarText, statusBarIcon;

	/* SIZE AND POSITIONING */
	AnimBool SizeAndPlacement;
	SerializedProperty scalingAxis;
	SerializedProperty statusBarSize, spacingX, spacingY;
	SerializedProperty preserveAspect, targetImage;
	SerializedProperty xRatio, yRatio;
	SerializedProperty positioningOption, findBy;
	SerializedProperty targetName, targetTag;
	SerializedProperty cameraTransform;
	AnimBool customAspectRatio, PosOpScreenSpace;
	AnimBool PosOpFollowCam, PosOpDisabled;

	/* STYLE AND OPTIONS */
	AnimBool StyleAndOptions;
	SerializedProperty initialState, idleSeconds;
	SerializedProperty enabledDuration, disabledDuration;
	SerializedProperty timeoutOption, statusBarAnimator;

	/* SCRIPT REFERENCE */
	AnimBool ScriptReference;
	SerializedProperty controllerName;
	AnimBool nameUnassigned, nameAssigned;
	public enum ScriptCast{ UpdatePositioning, UpdateName, UpdateIcon, ShowStatusBar, HideStatusBar }
	public ScriptCast scriptCast;

	/* DEBUGGING */
	AnimBool UltimateStatusBars;
	UltimateStatusBar[] myStatusBars;
	float[] testValues = new float[ 0 ];
	Canvas parentCanvas;


	void OnEnable ()
	{
		// Store the references to all variables.
		StoreReferences();

		// Register the UndoRedoCallback function to be called when an undo/redo is performed.
		Undo.undoRedoPerformed += UndoRedoCallback;

		if( Selection.activeGameObject == null )
			return;

		// Store the child status bars as soon as the status bar controller has been selected.
		myStatusBars = Selection.activeGameObject.GetComponentsInChildren<UltimateStatusBar>();

		testValues = new float[ myStatusBars.Length ];
		for( int i = 0; i < testValues.Length; i++ )
			testValues[ i ] = 1.0f;

		parentCanvas = GetParentCanvas();
	}

	void OnDisable ()
	{
		// Remove the UndoRedoCallback from the Undo event.
		Undo.undoRedoPerformed -= UndoRedoCallback;
	}

	// Function called for Undo/Redo operations.
	void UndoRedoCallback ()
	{
		// Re-reference all variables on undo/redo.
		StoreReferences();
	}

	Canvas GetParentCanvas ()
	{
		if( Selection.activeGameObject == null )
			return null;

		// Store the current parent.
		Transform parent = Selection.activeGameObject.transform.parent;

		// Loop through parents as long as there is one.
		while( parent != null )
		{ 
			// If there is a Canvas component, return that gameObject.
			if( parent.transform.GetComponent<Canvas>() && parent.transform.GetComponent<Canvas>().enabled == true )
				return parent.transform.GetComponent<Canvas>();
			
			// Else, shift to the next parent.
			parent = parent.transform.parent;
		}
		if( parent == null && PrefabUtility.GetPrefabType( Selection.activeGameObject ) != PrefabType.Prefab )
			UltimateStatusBarCreator.RequestCanvas( Selection.activeGameObject );
		return null;
	}

	bool CanvasErrors ()
	{
		// If the selection is actually the prefab within the Project window, then return no errors.
		if( PrefabUtility.GetPrefabType( Selection.activeGameObject ) == PrefabType.Prefab )
			return false;

		// If parentCanvas is unassigned, then get a new canvas and return no errors.
		if( parentCanvas == null )
		{
			parentCanvas = GetParentCanvas();
			return false;
		}

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
	
	/*
	For more information on the OnInspectorGUI and adding your own variables
	in the UltimateStatusBarController.cs script and displaying them in this script,
	see the EditorGUILayout section in the Unity Documentation to help out.
	*/
	public override void OnInspectorGUI ()
	{
		serializedObject.Update();

		UltimateStatusBarController usbController = ( UltimateStatusBarController )target;

		EditorGUILayout.Space();

		#region ASSIGNED VARIABLES
		/* ---------------------------------------- > ASSIGNED VARIABLES < ---------------------------------------- */
		DisplayHeader( "Assigned Variables", "UUI_Variables", AssignedVariables );
		if( EditorGUILayout.BeginFadeGroup( AssignedVariables.faded ) )
		{
			EditorGUILayout.Space();
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( statusBarText, new GUIContent( "Status Bar Text", "The main text of the status bar. Commonly showing the name or function." ) );
			if( EditorGUI.EndChangeCheck() )
				serializedObject.ApplyModifiedProperties();

			if( usbController.statusBarText == null )
				EditorGUILayout.HelpBox( "Status Bar Name variable needs to be assigned in order for the UpdateStatusBarName() function to work.", MessageType.Warning );
			else
			{
				EditorGUI.BeginChangeCheck();
				statusBarName = EditorGUILayout.TextField( "Status Bar Name", statusBarName );
				if( EditorGUI.EndChangeCheck() )
				{
					usbController.statusBarText.enabled = false;
					usbController.UpdateStatusBarName( statusBarName );
					usbController.statusBarText.enabled = true;
				}
			}

			EditorGUILayout.Space();

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( statusBarIcon, new GUIContent( "Status Bar Icon", "The icon of the status bar." ) );
			if( usbController.statusBarIcon == null )
				EditorGUILayout.HelpBox( "Status Bar Icon variable needs to be assigned in order for the UpdateStatusBarIcon() function to work.", MessageType.Warning );
			if( EditorGUI.EndChangeCheck() )
				serializedObject.ApplyModifiedProperties();
		}
		EditorGUILayout.EndFadeGroup();
		/* -------------------------------------- > END ASSIGNED VARIABLES < -------------------------------------- */
		#endregion

		EditorGUILayout.Space();

		#region SIZE AND PLACEMENT
		/* ---------------------------------------- > SIZE AND PLACEMENT < ---------------------------------------- */
		DisplayHeader( "Size and Placement", "UUI_SizeAndPlacement", SizeAndPlacement );
		if( EditorGUILayout.BeginFadeGroup( SizeAndPlacement.faded ) )
		{
			EditorGUILayout.Space();
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( positioningOption, new GUIContent( "Positioning" ) );
			if( EditorGUI.EndChangeCheck() )
			{
				serializedObject.ApplyModifiedProperties();

				PosOpScreenSpace.target = usbController.positioningOption == UltimateStatusBarController.PositioningOption.ScreenSpace ? true : false;
				PosOpFollowCam.target = usbController.positioningOption == UltimateStatusBarController.PositioningOption.FollowCameraRotation ? true : false;
				PosOpDisabled.target = usbController.positioningOption == UltimateStatusBarController.PositioningOption.Disabled ? true : false;
			}

			if( EditorGUILayout.BeginFadeGroup( PosOpScreenSpace.faded ) )
			{
				if( CanvasErrors() == true )
				{
					if( parentCanvas.renderMode != RenderMode.ScreenSpaceOverlay )
					{
						EditorGUILayout.LabelField( "Canvas", EditorStyles.boldLabel );
						EditorGUILayout.HelpBox( "The parent Canvas needs to be set to 'Screen Space - Overlay' in order for the Ultimate Status Bar to function correctly.", MessageType.Error );
						EditorGUILayout.BeginHorizontal();
						GUILayout.Space( 5 );
						if( GUILayout.Button( "Update Canvas" ) )
						{
							parentCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
						}
						GUILayout.Space( 5 );
						if( GUILayout.Button( "Update Status Bar" ) )
						{
							UltimateStatusBarCreator.RequestCanvas( Selection.activeGameObject );
							parentCanvas = GetParentCanvas();
						}
						GUILayout.Space( 5 );
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Space();
					}
					if( parentCanvas.GetComponent<CanvasScaler>() )
					{
						if( parentCanvas.GetComponent<CanvasScaler>().uiScaleMode != CanvasScaler.ScaleMode.ConstantPixelSize )
						{
							EditorGUILayout.LabelField( "Canvas Scaler", EditorStyles.boldLabel );
							EditorGUILayout.HelpBox( "The Canvas Scaler component located on the parent Canvas needs to be set to 'Constant Pixel Size' in order for the Ultimate Status Bar to function correctly.", MessageType.Error );
							EditorGUILayout.BeginHorizontal();
							GUILayout.Space( 5 );
							if( GUILayout.Button( "Update Canvas" ) )
							{
								parentCanvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
							}
							GUILayout.Space( 5 );
							if( GUILayout.Button( "Update Status Bar" ) )
							{
								UltimateStatusBarCreator.RequestCanvas( Selection.activeGameObject );
								parentCanvas = GetParentCanvas();
							}
							GUILayout.Space( 5 );
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.Space();
						}
					}
				}
				else
				{
					GUILayout.Space( 5 );

					EditorGUI.BeginChangeCheck();
					EditorGUILayout.PropertyField( scalingAxis, new GUIContent( "Scaling Axis", "Should the Ultimate Status Bar be sized according to screen Height or Width?" ) );
					EditorGUILayout.Slider( statusBarSize, 0.0f, 10.0f, new GUIContent( "Status Bar Size", "Determines the overall size of the status bar." ) );
					EditorGUILayout.PropertyField( preserveAspect, new GUIContent( "Preserve Aspect", "Should the Ultimate Status Bar preserve the aspect ratio of the targeted image?" ) );
					EditorGUI.BeginDisabledGroup( usbController.preserveAspect == false );
					EditorGUI.indentLevel = 1;
					EditorGUILayout.PropertyField( targetImage, new GUIContent( "Target Image", "The targeted image to preserve the aspect ratio of." ) );
					if( usbController.preserveAspect == true && usbController.targetImage == null )
						EditorGUILayout.HelpBox( "Target Image needs to be assigned for the Preserve Aspect option to work.", MessageType.Error );
					EditorGUI.indentLevel = 0;
					EditorGUI.EndDisabledGroup();
					if( EditorGUILayout.BeginFadeGroup( customAspectRatio.faded ) )
					{
						EditorGUI.indentLevel = 1;
						EditorGUILayout.Slider( xRatio, 0.0f, 1.0f, new GUIContent( "X Ratio", "The desired width of the image." ) );
						EditorGUILayout.Slider( yRatio, 0.0f, 1.0f, new GUIContent( "Y Ratio", "The desired height of the image." ) );
						EditorGUI.indentLevel = 0;
					}
					if( SizeAndPlacement.faded == 1 && PosOpScreenSpace.faded == 1 )
						EditorGUILayout.EndFadeGroup();

					if( EditorGUI.EndChangeCheck() )
					{
						serializedObject.ApplyModifiedProperties();
						customAspectRatio.target = usbController.preserveAspect == true ? false : true;
					}

					EditorGUILayout.Space();

					EditorGUILayout.BeginVertical( "Box" );
					GUILayout.BeginHorizontal();
					EditorGUILayout.LabelField( "Status Bar Position", EditorStyles.boldLabel );
					GUILayout.EndHorizontal();
					EditorGUI.indentLevel = 1;
					EditorGUI.BeginChangeCheck();
					EditorGUILayout.Slider( spacingX, 0.0f, 100.0f, new GUIContent( "X Position", "The horizontal position of the image." ) );
					EditorGUILayout.Slider( spacingY, 0.0f, 100.0f, new GUIContent( "Y Position", "The vertical position of the image." ) );
					if( EditorGUI.EndChangeCheck() )
						serializedObject.ApplyModifiedProperties();
				
					GUILayout.Space( 1 );

					EditorGUI.indentLevel = 0;
					EditorGUILayout.EndVertical();
				}
			}
			if( SizeAndPlacement.faded == 1 )
				EditorGUILayout.EndFadeGroup();

			if( EditorGUILayout.BeginFadeGroup( PosOpFollowCam.faded ) )
			{
				GUILayout.Space( 5 );

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField( findBy );
				if( EditorGUI.EndChangeCheck() )
					serializedObject.ApplyModifiedProperties();

				EditorGUI.BeginChangeCheck();
				if( usbController.findBy == UltimateStatusBarController.FindBy.Camera )
					EditorGUILayout.PropertyField( cameraTransform );
				else if( usbController.findBy == UltimateStatusBarController.FindBy.Name )
					EditorGUILayout.PropertyField( targetName );
				else
					EditorGUILayout.PropertyField( targetTag );
				if( EditorGUI.EndChangeCheck() )
					serializedObject.ApplyModifiedProperties();
			}
			if( SizeAndPlacement.faded == 1 )
				EditorGUILayout.EndFadeGroup();
		}
		EditorGUILayout.EndFadeGroup();
		/* -------------------------------------- > END SIZE AND PLACEMENT < -------------------------------------- */
		#endregion

		EditorGUILayout.Space();

		#region STYLE AND OPTIONS
		/* ----------------------------------------- > STYLE AND OPTIONS < ----------------------------------------- */
		DisplayHeader( "Style and Options", "UUI_StyleAndOptions", StyleAndOptions );
		if( EditorGUILayout.BeginFadeGroup( StyleAndOptions.faded ) )
		{
			EditorGUILayout.Space();
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( initialState, new GUIContent( "Initial State", "Determines if the Ultimate Status Bar Controller should be enabled or disabled from start." ) );
			EditorGUILayout.PropertyField( timeoutOption, new GUIContent( "Timeout Option", "Should the Ultimate Status Bar be disabled visually after being idle for a designated time?" ) );
			EditorGUI.indentLevel = 1;
			if( usbController.timeoutOption == UltimateStatusBarController.TimeoutOption.Fade )
			{
				EditorGUILayout.PropertyField( idleSeconds, new GUIContent( "Idle Seconds", "Time in seconds after being idle for the status bar to be disabled." ) );
				EditorGUILayout.PropertyField( enabledDuration, new GUIContent( "Enabled Duration", "The duration in which to fade in." ) );
				EditorGUILayout.PropertyField( disabledDuration, new GUIContent( "Disabled Duration", "The duration in which to fade out." ) );
			}
			else if( usbController.timeoutOption == UltimateStatusBarController.TimeoutOption.Animation )
			{
				EditorGUILayout.PropertyField( idleSeconds, new GUIContent( "Idle Seconds", "Time in seconds after being idle for the status bar to be disabled." ) );
				EditorGUILayout.PropertyField( statusBarAnimator, new GUIContent( "Animator", "The Animator to be used for enabling and disabling the Ultimate Status Bar." ) );

				if( usbController.statusBarAnimator == null )
					EditorGUILayout.HelpBox( "The Animator component is not assigned. Please make sure to assign the Animator before continuing.", MessageType.Error );
			}
			EditorGUI.indentLevel = 0;
			if( EditorGUI.EndChangeCheck() )
				serializedObject.ApplyModifiedProperties();
		}
		EditorGUILayout.EndFadeGroup();
		/* --------------------------------------- > END STYLE AND OPTIONS < --------------------------------------- */
		#endregion

		EditorGUILayout.Space();

		#region SCRIPT REFERENCE
		/* ----------------------------------------- > SCRIPT REFERENCE < ----------------------------------------- */
		DisplayHeader( "Script Reference", "UUI_ScriptReference", ScriptReference );
		if( EditorGUILayout.BeginFadeGroup( ScriptReference.faded ) )
		{
			EditorGUILayout.Space();
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( controllerName, new GUIContent( "Controller Name", "The name to be used for reference from scripts." ) );
			if( EditorGUI.EndChangeCheck() )
			{
				serializedObject.ApplyModifiedProperties();

				nameUnassigned.target = usbController.controllerName == string.Empty ? true : false;
				nameAssigned.target = usbController.controllerName != string.Empty ? true : false;
			}

			if( EditorGUILayout.BeginFadeGroup( nameUnassigned.faded ) )
			{
				EditorGUILayout.HelpBox( "Please make sure to assign a name so that this controller can be referenced from your scripts.", MessageType.Warning );
			}
			if( ScriptReference.faded == 1.0f )
				EditorGUILayout.EndFadeGroup();

			if( EditorGUILayout.BeginFadeGroup( nameAssigned.faded ) )
			{
				EditorGUILayout.BeginVertical( "Box" );
				GUILayout.Space( 1 );
				scriptCast = ( ScriptCast )EditorGUILayout.EnumPopup( "Script Use: ", scriptCast );
				if( scriptCast == ScriptCast.UpdatePositioning )
					EditorGUILayout.TextField( "UltimateStatusBarController.UpdatePositioning( \"" + usbController.controllerName + "\" );" );
				else if( scriptCast == ScriptCast.UpdateName )
					EditorGUILayout.TextField( "UltimateStatusBarController.UpdateStatusBarName( \"" + usbController.controllerName + "\", targetName );" );
				else if( scriptCast == ScriptCast.UpdateIcon )
					EditorGUILayout.TextField( "UltimateStatusBarController.UpdateStatusBarIcon( \"" + usbController.controllerName + "\", targetIcon );" );
				else if( scriptCast == ScriptCast.ShowStatusBar )
					EditorGUILayout.TextField( "UltimateStatusBarController.ShowStatusBar( \"" + usbController.controllerName + "\" );" );
				else if( scriptCast == ScriptCast.HideStatusBar )
					EditorGUILayout.TextField( "UltimateStatusBarController.HideStatusBar( \"" + usbController.controllerName + "\" );" );
				GUILayout.Space( 1 );
				EditorGUILayout.EndVertical();
			}
			if( ScriptReference.faded == 1.0f )
				EditorGUILayout.EndFadeGroup();
		}
		EditorGUILayout.EndFadeGroup();
		/* --------------------------------------- > END SCRIPT REFERENCE < --------------------------------------- */
		#endregion

		EditorGUILayout.Space();

		#region DEBUGGING
		/* --------------------------------------- > ULTIMATE STATUS BARS < --------------------------------------- */
		DisplayHeader( "Debugging", "UUI_ExtraOption_01", UltimateStatusBars );
		if( EditorGUILayout.BeginFadeGroup( UltimateStatusBars.faded ) )
		{
			EditorGUILayout.Space();

			if( myStatusBars.Length == 0 )
				EditorGUILayout.HelpBox( "There are no Ultimate Status Bar scripts attached to any children of this object. " +
					"Please be sure there is at least one Ultimate Status Bar before attempting to make changes in this section.", MessageType.Warning );
			else
			{
				bool hasDuplicates = false;
				EditorGUI.indentLevel = 0;
				for( int i = 0; i < myStatusBars.Length; i++ )
				{
					for( int eachStatus = 0; eachStatus < myStatusBars.Length; eachStatus++ )
					{
						if( myStatusBars[ i ] != myStatusBars[ eachStatus ] && myStatusBars[ i ].statusBarName == myStatusBars[ eachStatus ].statusBarName )
							hasDuplicates = true;
					}
				}
				if( hasDuplicates == true )
					EditorGUILayout.HelpBox( "Some statusBarName references are the same. Please be sure to make every Ultimate Status Bar name unique.", MessageType.Error );

				for( int i = 0; i < myStatusBars.Length; i++ )
				{
					EditorGUILayout.BeginVertical( "Box" );

					GUILayout.Space( 1 );

					if( GUILayout.Button( myStatusBars[ i ].gameObject.name ) )
						Selection.activeGameObject = myStatusBars[ i ].gameObject;

					GUILayout.Space( 5 );

					EditorGUI.BeginChangeCheck();
					myStatusBars[ i ].statusBarName = EditorGUILayout.TextField( new GUIContent( "Status Name:" ), myStatusBars[ i ].statusBarName );
					if( EditorGUI.EndChangeCheck() )
						EditorUtility.SetDirty( myStatusBars[i] );

					EditorGUI.BeginChangeCheck();
					testValues[ i ] = EditorGUILayout.Slider( "Test Value", testValues[ i ], 0.0f, 1.0f );
					if( EditorGUI.EndChangeCheck() )
					{
						myStatusBars[ i ].statusBar.enabled = false;
						myStatusBars[ i ].UpdateStatusBar( testValues[ i ], 1.0f );
						myStatusBars[ i ].statusBar.enabled = true;
					}
					GUILayout.Space( 1 );
					EditorGUILayout.EndVertical();

					if( i != ( myStatusBars.Length - 1 ) )
						GUILayout.Space( 5 );
				}
			}
		}
		EditorGUILayout.EndFadeGroup();
		/* ------------------------------------- > END ULTIMATE STATUS BARS < ------------------------------------- */
		#endregion

		EditorGUILayout.Space();

		Repaint();
	}

	// Function called to display an interactive header.
	void DisplayHeader ( string headerName, string editorPref, AnimBool targetAnim )
	{
		EditorGUILayout.BeginVertical( "Toolbar" );
		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField( headerName, EditorStyles.boldLabel );
		if( GUILayout.Button( EditorPrefs.GetBool( editorPref ) == true ? "Hide" : "Show", EditorStyles.miniButton, GUILayout.Width( 50 ), GUILayout.Height( 14f ) ) )
		{
			EditorPrefs.SetBool( editorPref, EditorPrefs.GetBool( editorPref ) == true ? false : true );
			targetAnim.target = EditorPrefs.GetBool( editorPref );
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
	}
	
	void StoreReferences ()
	{
		UltimateStatusBarController cont = ( UltimateStatusBarController )target;
		if( cont.statusBarText != null && cont.statusBarText.text != "New Text" && cont.statusBarText.text != string.Empty )
			statusBarName = cont.statusBarText.text;

		/* --------------------- > ASSIGNED COMPONENTS < --------------------- */
		AssignedVariables = new AnimBool( EditorPrefs.GetBool( "UUI_Variables" ) );

		statusBarText = serializedObject.FindProperty( "statusBarText" );
		statusBarIcon = serializedObject.FindProperty( "statusBarIcon" );
		/* ------------------- > END ASSIGNED COMPONENTS < ------------------- */

		/* --------------------- > SIZE AND POSITIONING < -------------------- */
		SizeAndPlacement = new AnimBool( EditorPrefs.GetBool( "UUI_SizeAndPlacement" ) );

		scalingAxis = serializedObject.FindProperty( "scalingAxis" );
		statusBarSize = serializedObject.FindProperty( "statusBarSize" );
		spacingX = serializedObject.FindProperty( "spacingX" );
		spacingY = serializedObject.FindProperty( "spacingY" );
		preserveAspect = serializedObject.FindProperty( "preserveAspect" );
		targetImage = serializedObject.FindProperty( "targetImage" );
		xRatio = serializedObject.FindProperty( "xRatio" );
		yRatio = serializedObject.FindProperty( "yRatio" );
		positioningOption = serializedObject.FindProperty( "positioningOption" );
		findBy = serializedObject.FindProperty( "findBy" );
		targetName = serializedObject.FindProperty( "targetName" );
		targetTag = serializedObject.FindProperty( "targetTag" );
		cameraTransform = serializedObject.FindProperty( "cameraTransform" );

		PosOpScreenSpace = new AnimBool( cont.positioningOption == UltimateStatusBarController.PositioningOption.ScreenSpace ? true : false );
		PosOpFollowCam = new AnimBool( cont.positioningOption == UltimateStatusBarController.PositioningOption.FollowCameraRotation ? true : false );
		PosOpDisabled = new AnimBool( cont.positioningOption == UltimateStatusBarController.PositioningOption.Disabled ? true : false );
		customAspectRatio = new AnimBool( cont.preserveAspect == true ? false : true );
		/* ------------------- > END SIZE AND POSITIONING < ------------------ */

		/* ---------------------- > STYLE AND OPTIONS < ---------------------- */
		StyleAndOptions = new AnimBool( EditorPrefs.GetBool( "UUI_StyleAndOptions" ) );

		initialState = serializedObject.FindProperty( "initialState" );
		timeoutOption = serializedObject.FindProperty( "timeoutOption" );
		idleSeconds = serializedObject.FindProperty( "idleSeconds" );
		enabledDuration = serializedObject.FindProperty( "enabledDuration" );
		disabledDuration = serializedObject.FindProperty( "disabledDuration" );
		statusBarAnimator = serializedObject.FindProperty( "statusBarAnimator" );
		/* ---------------------- > STYLE AND OPTIONS < ---------------------- */

		/* ----------------------- > SCRIPT REFERENCE < ---------------------- */
		ScriptReference = new AnimBool( EditorPrefs.GetBool( "UUI_ScriptReference" ) );

		controllerName = serializedObject.FindProperty( "controllerName" );

		nameUnassigned = new AnimBool( cont.controllerName == string.Empty ? true : false );
		nameAssigned = new AnimBool( cont.controllerName != string.Empty ? true : false );
		/* --------------------- > END SCRIPT REFERENCE < -------------------- */
		
		/* -------------------------- > DEBUGGING < -------------------------- */
		UltimateStatusBars = new AnimBool( EditorPrefs.GetBool( "UUI_ExtraOption_01" ) );
		/* ------------------------ > END DEBUGGING < ------------------------ */
	}
}

/* Written by Kaz Crowe */
/* UltimateJoystickCreator.cs ver. 1.0.2 */
// 1.0.1 - Removed the adding of a CanvasScaler onto the Canvas
// 1.0.2 - Change a few of the checks for the needed canvas before creating a new one
public class UltimateStatusBarCreator
{
	[MenuItem( "GameObject/UI/Ultimate UI/Ultimate Status Bar", false, 20 )]
	private static void CreateUltimateStatusBar ()
	{
		GameObject prefab = EditorGUIUtility.Load( "Ultimate Status Bar/UltimateStatusBar.prefab" ) as GameObject;

		if( prefab == null )
		{
			Debug.LogError( "Could not find 'UltimateStatusBar.prefab' in any Editor Default Resources folders." );
			return;
		}
		CreateNewUI( prefab );
	}
	
	[MenuItem( "GameObject/UI/Ultimate UI/Simple Status Bar", false, 21 )]// Update this, also check for prefabs existing and also add more for each prefab
	private static void CreateSimpleStatusBar ()
	{
		GameObject prefab = EditorGUIUtility.Load( "Ultimate Status Bar/SimpleStatusBar.prefab" ) as GameObject;

		if( prefab == null )
		{
			Debug.LogError( "Could not find 'SimpleStatusBar.prefab' in any Editor Default Resources folders." );
			return;
		}
		CreateNewUI( prefab );
	}
	
	[MenuItem( "GameObject/UI/Ultimate UI/Medium Status Bar", false, 22 )]
	private static void CreateTargetStatusBar ()
	{
		GameObject prefab = EditorGUIUtility.Load( "Ultimate Status Bar/MediumStatusBar.prefab" ) as GameObject;

		if( prefab == null )
		{
			Debug.LogError( "Could not find 'MediumStatusBar.prefab' in any Editor Default Resources folders." );
			return;
		}
		CreateNewUI( prefab );
	}

	[MenuItem( "GameObject/UI/Ultimate UI/Progress Status Bar", false, 23 )]
	private static void CreateInteractStatusBar ()
	{
		GameObject prefab = EditorGUIUtility.Load( "Ultimate Status Bar/ProgressStatusBar.prefab" ) as GameObject;

		if( prefab == null )
		{
			Debug.LogError( "Could not find 'ProgressStatusBar.prefab' in any Editor Default Resources folders." );
			return;
		}
		CreateNewUI( prefab );
	}
	
	private static void CreateNewUI ( Object objectPrefab )
	{
		GameObject prefab = ( GameObject )Object.Instantiate( objectPrefab, Vector3.zero, Quaternion.identity );
		prefab.name = objectPrefab.name;
		Selection.activeGameObject = prefab;
		RequestCanvas( prefab );
	}

	private static void CreateNewCanvas ( GameObject child )
	{
		GameObject root = new GameObject( "Ultimate UI Canvas" );
		root.layer = LayerMask.NameToLayer( "UI" );
		Canvas canvas = root.AddComponent<Canvas>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		// Think about this....GraphicRaycaster
		root.AddComponent<GraphicRaycaster>();
		Undo.RegisterCreatedObjectUndo( root, "Create " + root.name );

		child.transform.SetParent( root.transform, false );
		
		CreateEventSystem();
	}

	private static void CreateEventSystem ()
	{
		Object esys = Object.FindObjectOfType<EventSystem>();
		if( esys == null )
		{
			GameObject eventSystem = new GameObject( "EventSystem" );
			esys = eventSystem.AddComponent<EventSystem>();
			eventSystem.AddComponent<StandaloneInputModule>();
			
			Undo.RegisterCreatedObjectUndo( eventSystem, "Create " + eventSystem.name );
		}
	}

	/* PUBLIC STATIC FUNCTIONS */
	public static void RequestCanvas ( GameObject child )
	{
		Canvas[] allCanvas = Object.FindObjectsOfType( typeof( Canvas ) ) as Canvas[];

		for( int i = 0; i < allCanvas.Length; i++ )
		{
			if( allCanvas[ i ].renderMode == RenderMode.ScreenSpaceOverlay && allCanvas[ i ].enabled == true && !allCanvas[ i ].GetComponent<CanvasScaler>() )
			{
				child.transform.SetParent( allCanvas[ i ].transform, false );
				CreateEventSystem();
				return;
			}
		}
		CreateNewCanvas( child );
	}
}