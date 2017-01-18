/* Written by Kaz Crowe */
/* UltimateStatusBarEditor.cs ver 1.0.3 */
// 1.0.1 - Fixed flashingSpeed option appearing when in colorblended mode with flashing enabled.
// 1.0.2 - Fixed editor error when text is not assigned.
// 1.0.3 - Cleaning up script to make it easier to navigate and customize.
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

/*
Ultimate Status Bar - Version 1.0.4 plans: ********
X	- Organize the script to being a bit more clear and optimized.
X	- Add advanced script reference.
*/

[CanEditMultipleObjects]
[CustomEditor( typeof( UltimateStatusBar ) )]
public class UltimateStatusBarEditor : Editor
{
	float testValue = 100.0f;
	
	SerializedProperty statusBar, statusBarColor;
	
	SerializedProperty showText, statusBarText;
	SerializedProperty statusBarTextColor, usePercentage;
	SerializedProperty additionalText;

	SerializedProperty alternateStateOption;
	SerializedProperty triggerValue, flashing;
	SerializedProperty flashingSpeed;
	SerializedProperty alternateStateColor, alternateStateColorAlt;

	SerializedProperty smoothFill;
	SerializedProperty smoothFillDuration;

	SerializedProperty statusBarName;

	SerializedProperty fillConstraint, fillConstraintMin, fillConstraintMax;

	/* Animated Values */
	AnimBool AssignedVariables, ScriptReference;
	AnimBool StyleAndOptions, Debugging;

	AnimBool ShowText, AlternateState;
	AnimBool SmoothFill, AltStateColorBlend;
	AnimBool AltStateDefaultOption, Flashing;
	AnimBool FillConstraint, KeepAwake;

	AnimBool nameAssigned, nameUnassigned;

	public enum ScriptCast
	{
		UpdateStatus,
		UpdateColor,
		UpdateTextColor,
		AlternateState
	}
	public ScriptCast scriptCast;

	SerializedProperty keepControllerAwake, keepAwakeTrigger;

	UltimateStatusBarController controller;
	

	void OnEnable ()
	{
		// Store the references to all variables.
		StoreReferences();

		// Register the UndoRedoCallback function to be called when an undo/redo is performed.
		Undo.undoRedoPerformed += UndoRedoCallback;

		UltimateStatusBar status = ( UltimateStatusBar )target;
		controller = status.GetComponentInParent<UltimateStatusBarController>();
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

	// Function called to display an interactive header.
	void DisplayHeaderDropdown ( string headerName, string editorPref, AnimBool targetAnim )
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
	
	/*
	For more information on the OnInspectorGUI and adding your own variables
	in the UltimateStatusBar.cs script and displaying them in this script,
	see the EditorGUILayout section in the Unity Documentation to help out.
	*/
	public override void OnInspectorGUI ()
	{
		serializedObject.Update();
		
		UltimateStatusBar usbLogic = ( UltimateStatusBar )target;
		
		EditorGUILayout.Space();

		#region ASSIGNED VARIABLES
		/* ---------------------------------------- > ASSIGNED VARIABLES < ---------------------------------------- */
		DisplayHeaderDropdown( "Assigned Variables", "UUI_Variables", AssignedVariables );
		if( EditorGUILayout.BeginFadeGroup( AssignedVariables.faded ) )
		{
			EditorGUILayout.Space();

			// Status Bar Variables
			EditorGUI.BeginChangeCheck();
			{
				EditorGUILayout.PropertyField( statusBar, new GUIContent( "Status Bar", "The targeted image to display the status." ) );
				EditorGUI.BeginDisabledGroup( usbLogic.statusBar == null );
				EditorGUI.indentLevel = 1;
				EditorGUILayout.PropertyField( statusBarColor, new GUIContent( "Status Bar Color", "The default color of the status bar." ) );
				EditorGUI.EndDisabledGroup();
				if( usbLogic.statusBar == null )
				EditorGUILayout.HelpBox( "Status Bar variable needs to be assigned.", MessageType.Error );
				else if( usbLogic.statusBar.type != UnityEngine.UI.Image.Type.Filled )
				{
					EditorGUILayout.HelpBox( "Status Bar image needs to be adjusted to type: Filled.", MessageType.Warning );
					EditorGUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					if( GUILayout.Button( "Adjust Image" ) )
					{
						usbLogic.statusBar.type = UnityEngine.UI.Image.Type.Filled;
						usbLogic.statusBar.fillMethod = UnityEngine.UI.Image.FillMethod.Horizontal;
					}

					GUILayout.FlexibleSpace();
					EditorGUILayout.EndHorizontal();
				}
				EditorGUI.indentLevel = 0;
			}
			if( EditorGUI.EndChangeCheck() )
			{
				serializedObject.ApplyModifiedProperties();
				usbLogic.UpdateStatusBarColor( usbLogic.statusBarColor );
			}

			EditorGUILayout.Space();

			// Text variables
			if( EditorGUILayout.BeginFadeGroup( ShowText.faded ) )
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField( statusBarText, new GUIContent( "Status Bar Text", "The text component used to display the values of the status." ) );
				EditorGUI.indentLevel = 1;
				EditorGUI.BeginDisabledGroup( usbLogic.statusBarText == null );
				EditorGUILayout.PropertyField( statusBarTextColor, new GUIContent( "Text Color", "The color of the text component." ) );
				EditorGUI.EndDisabledGroup();
				EditorGUI.indentLevel = 0;
				if( usbLogic.statusBarText != null && usbLogic.statusBarTextColor != usbLogic.statusBarText.color )
				{
					EditorGUILayout.HelpBox( "Please assign the color of the text component using the color property above.", MessageType.Warning );
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.FlexibleSpace();
						if( GUILayout.Button( new GUIContent( "Update Color", "Updates the Text Color property to match the color of the text component." ) ) )
							usbLogic.statusBarTextColor = usbLogic.statusBarText.color;
						GUILayout.FlexibleSpace();
					}
					EditorGUILayout.EndHorizontal();
				}
				if( EditorGUI.EndChangeCheck() )
				{
					serializedObject.ApplyModifiedProperties();
					if( usbLogic.statusBarText != null )
						usbLogic.UpdateStatusBarTextColor( usbLogic.statusBarTextColor );
				}
			}
			if( AssignedVariables.faded == 1 )
				EditorGUILayout.EndFadeGroup();

		}
		EditorGUILayout.EndFadeGroup();
		/* -------------------------------------- > END ASSIGNED VARIABLES < -------------------------------------- */
		#endregion

		EditorGUILayout.Space();

		/* ----------------------------------------- > STYLE AND OPTIONS < ----------------------------------------- */
		DisplayHeaderDropdown( "Style and Options", "UUI_StyleAndOptions", StyleAndOptions );
		if( EditorGUILayout.BeginFadeGroup( StyleAndOptions.faded ) )
		{
			/* ---------------------- < SHOW TEXT > ---------------------- */
			EditorGUILayout.Space();
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( showText );
			if( EditorGUI.EndChangeCheck() )
			{
				serializedObject.ApplyModifiedProperties();
				if( usbLogic.showText == false && usbLogic.statusBarText != null )
					usbLogic.statusBarText.gameObject.SetActive( false );
				else if( usbLogic.showText == true && usbLogic.statusBarText != null )
					usbLogic.statusBarText.gameObject.SetActive( true );

				ShowText.target = usbLogic.showText;
			}

			if( EditorGUILayout.BeginFadeGroup( ShowText.faded ) )
			{
				EditorGUI.indentLevel = 1;
				if( usbLogic.statusBarText != null )
				{
					EditorGUI.BeginChangeCheck();
					EditorGUILayout.PropertyField( usePercentage, new GUIContent( "Use Percentage", "Should the status bar values be displayed as a percentage?" ) );
					EditorGUILayout.PropertyField( additionalText, new GUIContent( "Additional Text", "Determines what additional text is displayed before the values." ) );
					if( EditorGUI.EndChangeCheck() )
					{
						serializedObject.ApplyModifiedProperties();
						usbLogic.UpdateStatusBar( testValue, 100.0f );
					}
				}
				else
				{
					EditorGUILayout.HelpBox( "The Status Bar Text Variable needs to be assigned before making any changes.", MessageType.Error );
				}
				EditorGUI.indentLevel = 0;
				EditorGUILayout.Space();
			}
			if( StyleAndOptions.faded == 1 )
				EditorGUILayout.EndFadeGroup();
			/* -------------------- > END SHOW TEXT < -------------------- */

			/* ---------------------- < ALTERNATE STATES > ---------------------- */
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( alternateStateOption, new GUIContent( "Alternate State", "Does this status require having an alternate state to display?" ) );
			if( EditorGUI.EndChangeCheck() )
			{
				serializedObject.ApplyModifiedProperties();
				if( usbLogic.alternateStateOption == UltimateStatusBar.AlternateStateOption.None )
					usbLogic.UpdateStatusBarColor( usbLogic.statusBarColor );
				else
					usbLogic.UpdateStatusBar( testValue, 1.0f );

				AlternateState.target = usbLogic.alternateStateOption != UltimateStatusBar.AlternateStateOption.None ? true : false;
			}

			if( EditorGUILayout.BeginFadeGroup( AlternateState.faded ) )
			{
				EditorGUI.indentLevel = 1;
				EditorGUI.BeginChangeCheck();
				{
					if( usbLogic.alternateStateOption != UltimateStatusBar.AlternateStateOption.ColorBlended )
					{
						if( usbLogic.alternateStateOption == UltimateStatusBar.AlternateStateOption.Percentage )
							EditorGUILayout.Slider( triggerValue, 0.0f, 1.0f, new GUIContent( "Trigger Value", "The value at which the state will trigger." ) );

						EditorGUILayout.PropertyField( alternateStateColor, new GUIContent( "Alt State Color", "The color of the alternate state." ) );

						EditorGUILayout.PropertyField( flashing );
						if( EditorGUILayout.BeginFadeGroup( Flashing.faded ) )
						{
							EditorGUI.indentLevel = 2;
							EditorGUILayout.PropertyField( alternateStateColorAlt, new GUIContent( "Flashing Alt", "The alternate color of the alternate state." ) );
							EditorGUILayout.Slider( flashingSpeed, 10.0f, 20.0f, new GUIContent( "Flashing Speed", "The speed at which the status bar should transition between the colors." ) );
							EditorGUI.indentLevel = 1;
						}
						if( AlternateState.faded == 1 && StyleAndOptions.faded == 1 )
							EditorGUILayout.EndFadeGroup();

					}
					else if( usbLogic.alternateStateOption == UltimateStatusBar.AlternateStateOption.ColorBlended )
					{
						EditorGUILayout.PropertyField( statusBarColor, new GUIContent( "Alt State Full", "The color of the alternate state." ) );
						EditorGUILayout.PropertyField( alternateStateColor, new GUIContent( "Alt State Mid", "The color of the alternate state." ) );
						EditorGUILayout.PropertyField( alternateStateColorAlt, new GUIContent( "Alt State Low", "The alternate color of the alternate state." ) );
					}
				}
				if( EditorGUI.EndChangeCheck() )
				{
					serializedObject.ApplyModifiedProperties();
					usbLogic.UpdateStatusBar( testValue, 100.0f );

					AltStateColorBlend.target = usbLogic.alternateStateOption == UltimateStatusBar.AlternateStateOption.ColorBlended ? true : false;
					AltStateDefaultOption.target = usbLogic.alternateStateOption != UltimateStatusBar.AlternateStateOption.ColorBlended ? true : false;
					Flashing.target = usbLogic.flashing;
				}
				EditorGUI.indentLevel = 0;
				EditorGUILayout.Space();
			}
			if( StyleAndOptions.faded == 1 )
				EditorGUILayout.EndFadeGroup();
			/* -------------------- < END ALTERNATE STATES > -------------------- */

			/* ---------------------- < SMOOTH FILL > ---------------------- */
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( smoothFill, new GUIContent( "Smooth Fill", "Fills the status bar from the current amount to the target amount over time." ) );
			if( EditorGUI.EndChangeCheck() )
			{
				serializedObject.ApplyModifiedProperties();
				SmoothFill.target = usbLogic.smoothFill;
			}

			if( EditorGUILayout.BeginFadeGroup( SmoothFill.faded ) )
			{
				EditorGUI.indentLevel = 1;
				EditorGUI.BeginChangeCheck();
				{
					EditorGUILayout.PropertyField( smoothFillDuration, new GUIContent( "Duration", "The time in which it takes to fill the status bar to the target amount." ) );
				}
				if( EditorGUI.EndChangeCheck() )
					serializedObject.ApplyModifiedProperties();
				EditorGUI.indentLevel = 0;
				EditorGUILayout.Space();
			}
			if( StyleAndOptions.faded == 1 )
				EditorGUILayout.EndFadeGroup();
			/* ---------------------- < END SMOOTH FILL > ---------------------- */

			// Fill Constraint
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( fillConstraint, new GUIContent( "Fill Constraint", "Constrains the image fill amount to a minimum and maximum value." ) );
			if( EditorGUI.EndChangeCheck() )
			{
				serializedObject.ApplyModifiedProperties();
				FillConstraint.target = usbLogic.fillConstraint;
				usbLogic.UpdateStatusBar( testValue, 100.0f );
			}
			if( EditorGUILayout.BeginFadeGroup( FillConstraint.faded ) )
			{
				EditorGUI.indentLevel = 1;
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.Slider( fillConstraintMin, 0.0f, usbLogic.fillConstraintMax, new GUIContent( "Constraint Min", "The minimum fill amount to be constrained by." ) );
				EditorGUILayout.Slider( fillConstraintMax, usbLogic.fillConstraintMin, 1.0f, new GUIContent( "Constraint Max", "The maximum fill amount to be constrained by." ) );
				if( EditorGUI.EndChangeCheck() )
				{
					serializedObject.ApplyModifiedProperties();
					usbLogic.UpdateStatusBar( testValue, 100.0f );
				}
				EditorGUI.indentLevel = 0;
			}
			if( StyleAndOptions.faded == 1 )
				EditorGUILayout.EndFadeGroup();

			if( controller.timeoutOption != UltimateStatusBarController.TimeoutOption.None )
			{
				EditorGUI.BeginChangeCheck();// EDIT MAJOR:
				EditorGUILayout.PropertyField( keepControllerAwake, new GUIContent( "Keep Visible", "Should this status keep the controller awake if it is below a certain amount?" ) );
				if( EditorGUI.EndChangeCheck() )
				{
					serializedObject.ApplyModifiedProperties();
					KeepAwake.target = usbLogic.keepControllerAwake;
				}

				if( EditorGUILayout.BeginFadeGroup( KeepAwake.faded ) )
				{
					EditorGUI.indentLevel = 1;
					EditorGUI.BeginChangeCheck();
					EditorGUILayout.Slider( keepAwakeTrigger, 0.0f, 1.0f, new GUIContent( "Trigger", "The amount at which to keep the controller awake." ) );
					if( EditorGUI.EndChangeCheck() )
						serializedObject.ApplyModifiedProperties();
					EditorGUI.indentLevel = 0;
				}

				if( StyleAndOptions.faded == 1 )
					EditorGUILayout.EndFadeGroup();
			}
		}
		EditorGUILayout.EndFadeGroup();
		/* --------------------------------------- > END STYLE AND OPTIONS < --------------------------------------- */

		EditorGUILayout.Space();

		/* -------------------------------------- > REFERENCE < -------------------------------------- */
		DisplayHeaderDropdown( "Script Reference", "UUI_ScriptReference", ScriptReference );
		if( EditorGUILayout.BeginFadeGroup( ScriptReference.faded ) )
		{
			EditorGUILayout.Space();
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( statusBarName, new GUIContent( "Status Bar Name", "The name to be used for reference from scripts." ) );
			if( EditorGUI.EndChangeCheck() )
			{
				serializedObject.ApplyModifiedProperties();

				nameUnassigned.target = usbLogic.statusBarName == string.Empty ? true : false;
				nameAssigned.target = usbLogic.statusBarName != string.Empty ? true : false;
			}

			if( EditorGUILayout.BeginFadeGroup( nameUnassigned.faded ) )
			{
				EditorGUILayout.HelpBox( "Please make sure to assign a name so that this status bar can be referenced from your scripts.", MessageType.Warning );
			}
			if( ScriptReference.faded == 1.0f )
				EditorGUILayout.EndFadeGroup();

			if( EditorGUILayout.BeginFadeGroup( nameAssigned.faded ) )
			{
				EditorGUILayout.BeginVertical( "Box" );
				GUILayout.Space( 1 );
				scriptCast = ( ScriptCast )EditorGUILayout.EnumPopup( "Script Use: ", scriptCast );
				if( scriptCast == ScriptCast.UpdateStatus )
					EditorGUILayout.TextField( "UltimateStatusBar.UpdateStatus( \"" + usbLogic.statusBarName + "\", currentValue, maxValue );" );
				else if( scriptCast == ScriptCast.UpdateColor )
					EditorGUILayout.TextField( "UltimateStatusBar.UpdateStatusBarColor( \"" + usbLogic.statusBarName + "\", targetColor );" );
				else if( scriptCast == ScriptCast.UpdateTextColor )
					EditorGUILayout.TextField( "UltimateStatusBar.UpdateStatusBarTextColor( \"" + usbLogic.statusBarName + "\", targetTextColor );" );
				else if( scriptCast == ScriptCast.AlternateState )
					EditorGUILayout.TextField( "UltimateStatusBar.AlternateState( \"" + usbLogic.statusBarName + "\", targetState );" );
				GUILayout.Space( 1 );
				EditorGUILayout.EndVertical();
			}
			if( ScriptReference.faded == 1.0f )
				EditorGUILayout.EndFadeGroup();
		}
		EditorGUILayout.EndFadeGroup();
		/* ------------------------------------ > END REFERENCE < ------------------------------------ */

		EditorGUILayout.Space();
		
		/* -------------------------------------- > DEBUGGING < -------------------------------------- */
		DisplayHeaderDropdown( "Debugging", "UUI_ExtraOption_01", Debugging );
		if( EditorGUILayout.BeginFadeGroup( Debugging.faded ) )
		{
			EditorGUILayout.Space();

			EditorGUILayout.BeginVertical( "Box" );
			GUILayout.Space( 1 );
			if( GUILayout.Button( "Controller" ) )
				Selection.activeGameObject = Selection.activeGameObject.GetComponentInParent<UltimateStatusBarController>().gameObject;

			if( usbLogic.statusBar != null )
				EditorGUILayout.LabelField( new GUIContent( "Fill Method: " + usbLogic.statusBar.fillMethod, "The Fill Method as determined by the Image Component on the Status Bar." ) );

			EditorGUI.BeginChangeCheck();
			testValue = EditorGUILayout.Slider( new GUIContent( "Test Value", "A test value to preview the Ultimate Status Bar when values change." ), testValue, 0.0f, 100.0f );
			if( EditorGUI.EndChangeCheck() )
			{
				usbLogic.statusBar.enabled = false;
				usbLogic.UpdateStatusBar( testValue, 100.0f );
				usbLogic.statusBar.enabled = true;
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndFadeGroup();
		/* ------------------------------------ > END DEBUGGING < ------------------------------------ */

		EditorGUILayout.Space();
		Repaint();
	}

	// This function stores the references to the variables of the target.
	void StoreReferences ()
	{
		UltimateStatusBar status = ( UltimateStatusBar )target;
		if( status.statusBar != null )// This is a stupid elaborate way to find the target testValue, but it works for the most part.
			testValue = ( status.fillConstraint == true ? ( ( ( status.statusBar.fillAmount - status.fillConstraintMin ) /
				( status.fillConstraintMax - status.fillConstraintMin ) ) * 100.0f ) : status.statusBar.fillAmount * 100.0f );

		/* --------------------- > STATUS BAR SECTION < --------------------- */
		statusBar = serializedObject.FindProperty( "statusBar" );
		statusBarColor = serializedObject.FindProperty( "statusBarColor" );
		/* ------------------- > END STATUS BAR SECTION < ------------------- */

		/* ------------------------ > TEXT SECTION < ------------------------ */
		showText = serializedObject.FindProperty( "showText" );
		statusBarText = serializedObject.FindProperty( "statusBarText" );
		statusBarTextColor = serializedObject.FindProperty( "statusBarTextColor" );
		usePercentage = serializedObject.FindProperty( "usePercentage" );
		additionalText = serializedObject.FindProperty( "additionalText" );
		/* ---------------------- > END TEXT SECTION < ---------------------- */

		/* ---------------------- > ALTERNATE STATE < ----------------------- */
		alternateStateOption = serializedObject.FindProperty( "alternateStateOption" );
		triggerValue = serializedObject.FindProperty( "triggerValue" );
		flashing = serializedObject.FindProperty( "flashing" );
		flashingSpeed = serializedObject.FindProperty( "flashingSpeed" );
		alternateStateColor = serializedObject.FindProperty( "alternateStateColor" );
		alternateStateColorAlt = serializedObject.FindProperty( "alternateStateColorAlt" );
		/* -------------------- > END ALTERNATE STATE < --------------------- */

		// smooth fill options
		smoothFill = serializedObject.FindProperty( "smoothFill" );
		smoothFillDuration = serializedObject.FindProperty( "smoothFillDuration" );

		statusBarName = serializedObject.FindProperty( "statusBarName" );

		AssignedVariables = new AnimBool( EditorPrefs.GetBool( "UUI_Variables" ) );
		StyleAndOptions = new AnimBool( EditorPrefs.GetBool( "UUI_StyleAndOptions" ) );
		ScriptReference = new AnimBool( EditorPrefs.GetBool( "UUI_ScriptReference" ) );
		Debugging = new AnimBool( EditorPrefs.GetBool( "UUI_ExtraOption_01" ) );

		// Varaible AnimBools
		ShowText = new AnimBool( status.showText );
		AlternateState = new AnimBool( status.alternateStateOption != UltimateStatusBar.AlternateStateOption.None ? true : false );
		SmoothFill = new AnimBool( status.smoothFill );
		Flashing = new AnimBool( status.flashing );
		AltStateColorBlend = new AnimBool( status.alternateStateOption == UltimateStatusBar.AlternateStateOption.ColorBlended ? true : false );
		AltStateDefaultOption = new AnimBool( status.alternateStateOption != UltimateStatusBar.AlternateStateOption.ColorBlended ? true : false );

		FillConstraint = new AnimBool( status.fillConstraint );
		KeepAwake = new AnimBool( status.keepControllerAwake );

		fillConstraint = serializedObject.FindProperty( "fillConstraint" );
		fillConstraintMin = serializedObject.FindProperty( "fillConstraintMin" );
		fillConstraintMax = serializedObject.FindProperty( "fillConstraintMax" );

		nameUnassigned = new AnimBool( status.statusBarName == string.Empty ? true : false );
		nameAssigned = new AnimBool( status.statusBarName != string.Empty ? true : false );

		keepControllerAwake = serializedObject.FindProperty( "keepControllerAwake" );
		keepAwakeTrigger = serializedObject.FindProperty( "keepAwakeTrigger" );
	}
}