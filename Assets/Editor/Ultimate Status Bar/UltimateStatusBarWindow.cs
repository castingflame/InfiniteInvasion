/* Written by Kaz Crowe */
/* UltimateStatusBarWindow.cs ver 1.0 */
using UnityEngine;
using UnityEditor;

public class UltimateStatusBarWindow : EditorWindow
{
	GUILayoutOption[] buttonSize = new GUILayoutOption[] { GUILayout.Width( 200 ), GUILayout.Height( 35 ) }; 

	GUILayoutOption[] docSize = new GUILayoutOption[] { GUILayout.Width( 300 ), GUILayout.Height( 330 ) };

	GUISkin style;

	enum CurrentMenu
	{
		MainMenu,
		HowTo,
		Overview,
		Documentation,
		Extras,
		OtherProducts,
		Feedback,
		ThankYou
	}
	static CurrentMenu currentMenu;
	static string menuTitle = "Main Menu";

	Texture2D scriptRef;
	Texture2D ubPromo, ujPromo, fstpPromo;

	Vector2 scroll_HowTo = Vector2.zero, scroll_Overview = Vector2.zero, scroll_OverviewUSB = Vector2.zero, scroll_OverviewUSBC = Vector2.zero;
	Vector2 scroll_Docs = Vector2.zero, scroll_DocsUSB = Vector2.zero, scroll_DocsUSBC = Vector2.zero, scroll_Extras = Vector2.zero;
	Vector2 scroll_OtherProd = Vector2.zero, scroll_Feedback = Vector2.zero, scroll_Thanks = Vector2.zero;

	delegate void BackFunction();
	BackFunction backFunction;

	int overviewMenu = 0;
	int documentationMenu = 0;

	
	[MenuItem( "Window/Ultimate UI/Ultimate Status Bar", false, 10 )]
	static void Init ()
	{
		InitializeWindow();
	}

	static void InitializeWindow ()
	{
		EditorWindow window = GetWindow<UltimateStatusBarWindow>( true, "Tank and Healer Studio Asset Window", true );
		window.maxSize = new Vector2( 500, 500 );
		window.minSize = new Vector2( 500, 500 );
		window.Show();
	}
	
	void OnEnable ()
	{
		style = ( GUISkin )EditorGUIUtility.Load( "Ultimate Status Bar/UltimateStatusBarEditorSkin.guiskin" );

		if( EditorPrefs.GetBool( "UltimateStatusBarStartup" ) == true )
			currentMenu = CurrentMenu.MainMenu;
		else
			currentMenu = CurrentMenu.ThankYou;

		overviewMenu = 0;
		documentationMenu = 0;

		backFunction += BackFunctionality;

		ubPromo = ( Texture2D )EditorGUIUtility.Load( "Ultimate UI/UB_Promo.png" );
		ujPromo = ( Texture2D )EditorGUIUtility.Load( "Ultimate UI/UJ_Promo.png" );
		fstpPromo = ( Texture2D )EditorGUIUtility.Load( "Ultimate UI/FSTP_Promo.png" );
		scriptRef = ( Texture2D )EditorGUIUtility.Load( "Ultimate Status Bar/USB_ScriptRef.png" );
	}
	
	void OnGUI ()
	{
		if( style == null )
		{
			GUILayout.BeginVertical( "Box" );
			GUILayout.FlexibleSpace();

			ErrorScreen();

			GUILayout.FlexibleSpace();
			EditorGUILayout.EndVertical();
			return;
		}

		GUI.skin = style;

		EditorGUILayout.Space();

		GUILayout.BeginVertical( "Box" );
		
		EditorGUILayout.LabelField( "Ultimate Status Bar", GUI.skin.GetStyle( "WindowTitle" ) );

		GUILayout.Space( 3 );

		EditorGUILayout.LabelField( " Version 2.0.3", EditorStyles.whiteMiniLabel );//< ---- ALWAYS UPDATE

		GUILayout.Space( 12 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.Space( 5 );
		if( currentMenu != CurrentMenu.MainMenu && currentMenu != CurrentMenu.ThankYou )
		{
			EditorGUILayout.BeginVertical();
			GUILayout.Space( 5 );
			if( GUILayout.Button( "", GUI.skin.GetStyle( "BackButton" ), GUILayout.Width( 80 ), GUILayout.Height( 40 ) ) )
				backFunction();
			EditorGUILayout.EndVertical();
		}
		else
			GUILayout.Space( 80 );

		GUILayout.Space( 15 );
		EditorGUILayout.BeginVertical();
		GUILayout.Space( 10 );
		EditorGUILayout.LabelField( menuTitle, GUI.skin.GetStyle( "HeaderText" ) );
		EditorGUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.Space( 80 );
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		switch( currentMenu )
		{
			case CurrentMenu.MainMenu:
			{
				MainMenu();
			}break;
			case CurrentMenu.HowTo:
			{
				HowTo();
			}break;
			case CurrentMenu.Overview:
			{
				OverviewMenu();
			}break;
			case CurrentMenu.Documentation:
			{
				Documentation();
			}break;
			case CurrentMenu.Extras:
			{
				Extras();
			}break;
			case CurrentMenu.OtherProducts:
			{
				OtherProducts();
			}break;
			case CurrentMenu.Feedback:
			{
				Feedback();
			}break;
			case CurrentMenu.ThankYou:
			{
				ThankYou();
			}break;
			default:
			{
				MainMenu();
			}break;
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		GUILayout.Space( 25 );

		EditorGUILayout.EndVertical();
	}

	void ErrorScreen ()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space( 50 );
		EditorGUILayout.LabelField( "ERROR", EditorStyles.boldLabel );
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Space( 50 );
		EditorGUILayout.LabelField( "Could not find the needed GUISkin located in the Editor Default Resources folder. Please ensure that the correct GUISkin, UltimateStatusBarEditorSkin, is in the right folder( Editor Default Resources/Ultimate Status Bar ) before trying to access the Ultimate Status Bar Window.", EditorStyles.wordWrappedLabel );
		GUILayout.Space( 50 );
		EditorGUILayout.EndHorizontal();
	}

	void BackFunctionality ()
	{
		if( overviewMenu != 0 )
		{
			overviewMenu = 0;
			menuTitle = "Overview";
		}
		else if( documentationMenu != 0 )
		{
			documentationMenu = 0;
			menuTitle = "Documentation";
		}
		else
		{
			currentMenu = CurrentMenu.MainMenu;
			menuTitle = "Main Menu";
		}
	}
	
	#region MainMenu
	void MainMenu ()
	{
		EditorGUILayout.BeginVertical();
		GUILayout.Space( 25 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "How To", buttonSize ) )
		{
			currentMenu = CurrentMenu.HowTo;
			menuTitle = "How To";
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Overview", buttonSize ) )
		{
			currentMenu = CurrentMenu.Overview;
			menuTitle = "Overview";
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Documentation", buttonSize ) )
		{
			currentMenu = CurrentMenu.Documentation;
			menuTitle = "Documentation";
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Extras", buttonSize ) )
		{
			currentMenu = CurrentMenu.Extras;
			menuTitle = "Extras";
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Other Products", buttonSize ) )
		{
			currentMenu = CurrentMenu.OtherProducts;
			menuTitle = "Other Products";
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Feedback", buttonSize ) )
		{
			currentMenu = CurrentMenu.Feedback;
			menuTitle = "Feedback";
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndVertical();
	}
	#endregion

	#region HowTo
	void HowTo ()
	{
		scroll_HowTo = EditorGUILayout.BeginScrollView( scroll_HowTo, false, false, docSize );

		EditorGUILayout.LabelField( "How To Create", GUI.skin.GetStyle( "SectionHeader" ) );

		EditorGUILayout.LabelField( "   To create a Ultimate Status Bar within your scene, just go up to GameObject / UI / Ultimate UI / Ultimate Status Bar. What this will do is locate a Ultimate Status Bar prefab that is located within the Editor Default Resources folder, and created a Ultimate Status Bar within the scene.", EditorStyles.wordWrappedLabel );
		EditorGUILayout.LabelField( "This method of adding an Ultimate Status Bar to your scene ensures that the status bars will have a Canvas and an EventSystem so that it can work correctly.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 20 );

		EditorGUILayout.LabelField( "How To Customize", GUI.skin.GetStyle( "SectionHeader" ) );
		EditorGUILayout.LabelField( "   There are many ways to use the Ultimate Status Bar within your projects. The two main components have very different functionality, however they work together to deliver powerful capabilities to your projects.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		EditorGUILayout.LabelField( "Ultimate Status Bar", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "The Ultimate Status Bar is used to display any kind of status within your project. Anything from Health to Loading Progress can easily be implemented using the Ultimate Status Bar. The options available allow you to make each status behave the way that you want them to.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		EditorGUILayout.LabelField( "Ultimate Status Bar Controller", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "The Ultimate Status Bar Controller can be used to position the status bar on the screen, or even follow the rotation of a camera so that it can be used in world space above an enemy or character.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 20 );

		EditorGUILayout.LabelField( "How To Reference", GUI.skin.GetStyle( "SectionHeader" ) );

		EditorGUILayout.LabelField( "   The Ultimate Status Bar is incredibly easy to get implemented into your custom scripts. The first thing that you'll want to make sure to do is give the status a unique name. This is done in the Script Reference section located within the Inspector window.", EditorStyles.wordWrappedLabel );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label( scriptRef );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.LabelField( "As an example, let's implement the Ultimate Status Bar into a character's health. First off, we'll name an Ultimate Status Bar within our scene 'Health'. After this is done, you can copy and paste the code provided inside the Script Reference section. Make sure that the Script Use option is set to Update Status. After copying the code that is provided, find the function where your player is receiving damage from and paste the example code into the function.", EditorStyles.wordWrappedLabel );

		EditorGUILayout.LabelField( "Be sure to put it after the damage or healing has modified the health value. Of course, be sure to replace the currentValue and maxValue of the example code with your character's current and maximum health values. Whenever the character's health is updated, either by damage or healing done to the character, you will want to send the new information of the health's value. This process can be used for any status that you need to be displayed to the user. For more information about the individual functions available to both the Ultimate Status Bar and the Ultimate Status Bar Controller classes, please refer to the Documentation section of this window.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 20 );

		EditorGUILayout.EndScrollView();
	}
	#endregion

	#region Overview
	void OverviewMenu ()
	{
		if( overviewMenu == 1 )
		{
			scroll_OverviewUSB = EditorGUILayout.BeginScrollView( scroll_OverviewUSB, false, false, docSize );
			UltimateStatusBarOverview();
		}
		else if( overviewMenu == 2 )
		{
			scroll_OverviewUSBC = EditorGUILayout.BeginScrollView( scroll_OverviewUSBC, false, false, docSize );
			UltimateStatusBarControllerOverview();
		}
		else
		{
			scroll_Overview = EditorGUILayout.BeginScrollView( scroll_Overview, false, false, docSize );

			EditorGUILayout.LabelField( "Assigned Variables", GUI.skin.GetStyle( "SectionHeader" ) );
			EditorGUILayout.LabelField( "   In the Assigned Variables section of both the Ultimate Status Bar and the Ultimate Status Bar Controller, there are a few components that should already be assigned if you are using one of the Prefabs that has been provided. If not, you will see error messages in the inspector that will help you to see if any of these variables are left unassigned. Please note that these need to be assigned in order for the Ultimate Status Bar and the Ultimate Status Bar Controller to work properly.", EditorStyles.wordWrappedLabel );

			GUILayout.Space( 20 );

			EditorGUILayout.LabelField( "Script Reference", GUI.skin.GetStyle( "SectionHeader" ) );
			EditorGUILayout.LabelField( "   In the Script Reference section of both the Ultimate Status Bar and the Ultimate Status Bar Controller, you will have at your disposal the option for incredible options for a fast work flow to help you get the Ultimate Status Bar implemented into your project. After naming the targeted component, you will be presented with lines of code that you can simple copy and paste into your custom code to help get the Ultimate Status Bar working fast.", EditorStyles.wordWrappedLabel );

			GUILayout.Space( 20 );

			EditorGUILayout.LabelField( "Debugging", GUI.skin.GetStyle( "SectionHeader" ) );
			EditorGUILayout.LabelField( "   In the Debugging section of both the Ultimate Status Bar and the Ultimate Status Bar Controller, there are several fields that will be presented to help speed up the work flow of your project when customizing the Ultimate Status Bar to your liking.", EditorStyles.wordWrappedLabel );

			GUILayout.Space( 20 );

			EditorGUILayout.LabelField( "Ultimate Status Bar", GUI.skin.GetStyle( "SectionHeader" ) );
			EditorGUILayout.LabelField( "   The Ultimate Status Bar displays the many status' on anything from your Player, to the NPC's in your scene, to the Loading Bar when starting your game. Click 'More Info' to find out more about the options that you have available to you in the Ultimate Status Bar inspector.", EditorStyles.wordWrappedLabel );

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();

			if( GUILayout.Button( "More Info", buttonSize ) )
			{
				overviewMenu = 1;
				menuTitle = "Status Bar Overview";
			}
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			GUILayout.Space( 20 );

			EditorGUILayout.LabelField( "Ultimate Status Bar Controller", GUI.skin.GetStyle( "SectionHeader" ) );
			EditorGUILayout.LabelField( "   The Ultimate Status Bar Controller takes care of the positioning and visibility of the Ultimate Status Bar's that are children of it. Click 'More Info' to find out more about the options that you have available to you in the Ultimate Status Bar inspector.", EditorStyles.wordWrappedLabel );

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if( GUILayout.Button( "More Info", buttonSize ) )
			{
				overviewMenu = 2;
				menuTitle = "Controller Overview";
			}
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
		}

		GUILayout.Space( 20 );

		EditorGUILayout.EndScrollView();
	}

	void UltimateStatusBarOverview ()
	{
		// Style and Options
		EditorGUILayout.LabelField( "Style and Options", GUI.skin.GetStyle( "SectionHeader" ) );
		EditorGUILayout.LabelField( "   The Style and Options section contains options that affect how the user will see the status displayed in the scene.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// Show Text
		EditorGUILayout.LabelField( "Show Text", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "The Show Text option will determine whether or not the selected status will display the values of the status. You have options that will determine how the values will be presented to the user, like if the value should be a percentage or not.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// Alternate State
		EditorGUILayout.LabelField( "Alternate State", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "You can use the Alternate State option to give your players visual feedback when playing your game. After customizing your Alternate State you can choose how you want it to be used. For example, you can manually trigger it in order to show the user that thier character has been poisened or stunned. The Percentage option can help alert the user to a low amount of health, and the Color Blended option can show a visual representation using color to show the user the amount of health that character has.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// Smooth Fill
		EditorGUILayout.LabelField( "Smooth Fill", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "Smooth Fill can be used to give a nice smooth feel to your status by gradually transitioning from the current to the target value. If this option is selected, you will be presented with another option that you can set for the duration that it will take to get to the target value.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// Fill Constraint
		EditorGUILayout.LabelField( "Fill Constraint", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "If you have an image that isn't correctly sliced by Unity, or if you have an status image that is circular but not a complete circle, then the Fill Constraint option can help to display the status correctly to the user.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// Keep Visible
		EditorGUILayout.LabelField( "Keep Visible", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "If the Timeout Option of the Ultimate Status Bar Controller is enabled, then you will be able to choose if any of the status' used as children of it should stop the countdown so that the Ultimate Status Bar should not disappear from sight.", EditorStyles.wordWrappedLabel );
	}

	void UltimateStatusBarControllerOverview ()
	{
		/* //// --------------------------- < SIZE AND PLACEMENT > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Size and Placement", GUI.skin.GetStyle( "SectionHeader" ) );
		EditorGUILayout.LabelField( "   The Size and Placement section allows you to customize the Ultimate Status Bar's size and placement on the screen.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// Positioning
		EditorGUILayout.LabelField( "Positioning", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "Determines whether the Ultimate Status Bar should position itself on the screen, inside the scene, or not at all. If the option, Screen Space, is chosen, then you will be presented with options that will allow you to set where the status should be displayed on the screen. If the Follow Camera Rotation option is selected, then you will have options that will set how it will follow the camera in the scene. Of coarse, you can always disable the positioning by selecting the Disabled option.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// Scaling Axis
		EditorGUILayout.LabelField( "Scaling Axis", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "This option is only available when Screen Space is selected of the Positioning option. Determines which axis the Rect Transform will be scaled from. If Height is chosen, then the Ultimate Status Bar will scale itself proportionately to the Height of the screen.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// Status Bar Size
		EditorGUILayout.LabelField( "Status Bar Size", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "This option is only available when Screen Space is selected of the Positioning option. This options changes the overall size of the Ultimate Status Bar on the screen.", EditorStyles.wordWrappedLabel );
				
		GUILayout.Space( 5 );

		// Preserve Aspect
		EditorGUILayout.LabelField( "Preserve Aspect", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "This option is only available when Screen Space is selected of the Positioning option. This option will allow you to preserve the aspect ratio of the targeted images, so that you will not have to calculate out the dimensions that it must be to look right.", EditorStyles.wordWrappedLabel );
				
		GUILayout.Space( 5 );

		// Status Bar Position
		EditorGUILayout.LabelField( "Status Bar Position", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "This option is only available when Screen Space is selected of the Positioning option. This section of options will help to position the Ultimate Status Bar on the screen exactly where you want it.", EditorStyles.wordWrappedLabel );
				
		GUILayout.Space( 5 );

		// Status Bar Position
		EditorGUILayout.LabelField( "Find By", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "This option is only available when Follow Camera Rotation is selected of the Positioning option. After selecting one of the options for how the camera to follow should be found, then you will prompted with new variables to assign. For example, if you were to choose the option, Name, then you will be prompted to insert a string that is the name of the targeted camera within the scene.", EditorStyles.wordWrappedLabel );
		/* \\\\ -------------------------- < END SIZE AND PLACEMENT > --------------------------- //// */

		GUILayout.Space( 20 );

		/* //// ----------------------------- < STYLE AND OPTIONS > ----------------------------- \\\\ */
		EditorGUILayout.LabelField( "Style and Options", GUI.skin.GetStyle( "SectionHeader" ) );
		EditorGUILayout.LabelField( "   The Style and Options section allows set the Ultimate Status Bar's visual states for a smooth look that will help draw attention to your UI when needed.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// Scaling Axis
		EditorGUILayout.LabelField( "Initial State", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "If you wish to have your status' hidden from sight when the game is first started, then the Initial State option should be set to Disabled. This option will only affect the state that the Ultimate Status Bar will be in initially.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// Status Bar Size
		EditorGUILayout.LabelField( "Timeout Option", EditorStyles.boldLabel );
		EditorGUILayout.LabelField( "The Timeout Option will allow you to have the Ultimate Status Bar disappear from sight after not being interacted with for a set time. When this option is set to Animation, then the Ultimate Status Bar will use an animation to show a Enabled and Disabled state.", EditorStyles.wordWrappedLabel );
		/* \\\\ -------------------------- < END STYLE AND OPTIONS > --------------------------- //// */
	}
	#endregion
	
	#region Documentation
	void Documentation ()
	{
		if( documentationMenu == 1 )
		{
			scroll_DocsUSB = EditorGUILayout.BeginScrollView( scroll_DocsUSB, false, false, docSize );
			UltimateStatusBarDocumentation();
		}
		else if( documentationMenu == 2 )
		{
			scroll_DocsUSBC = EditorGUILayout.BeginScrollView( scroll_DocsUSBC, false, false, docSize );
			UltimateStatusBarControllerDocumentation();
		}
		else
		{
			scroll_Docs = EditorGUILayout.BeginScrollView( scroll_Docs, false, false, docSize );

			EditorGUILayout.LabelField( "Ultimate Status Bar", GUI.skin.GetStyle( "SectionHeader" ) );
			EditorGUILayout.LabelField( "   The Ultimate Status Bar displays the many status' on anything from your Player, to the NPC's in your scene, to the Loading Bar when starting your game.", EditorStyles.wordWrappedLabel );

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if( GUILayout.Button( "More Info", buttonSize ) )
			{
				documentationMenu = 1;
				menuTitle = "Status Bar Docs";
			}
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			GUILayout.Space( 20 );

			EditorGUILayout.LabelField( "Ultimate Status Bar Controller", GUI.skin.GetStyle( "SectionHeader" ) );
			EditorGUILayout.LabelField( "   The Ultimate Status Bar Controller takes care of the positioning and visibility of the Ultimate Status Bar's that are children of it.", EditorStyles.wordWrappedLabel );

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if( GUILayout.Button( "More Info", buttonSize ) )
			{
				documentationMenu = 2;
				menuTitle = "Controller Docs";
			}
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			GUILayout.FlexibleSpace();
		}

		GUILayout.Space( 20 );

		EditorGUILayout.EndScrollView();
	}

	void UltimateStatusBarDocumentation ()
	{
		/* //// --------------------------- < PUBLIC FUNCTIONS > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Public Functions", GUI.skin.GetStyle( "SectionHeader" ) );

		GUILayout.Space( 5 );

		// UpdateStatusBar
		EditorGUILayout.LabelField( "UpdateStatusBar( float currentValue, float maxValue )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Updates the values of the status in order to display them to the user. This function has two parameters that need to be passed into it. The currentValue should be the current amount of the targeted status, whereas the maxValue should be the maximum amount that the status can be. These values must be passed into the function in order to correctly display them to the user.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// UpdateStatusBarColor
		EditorGUILayout.LabelField( "UpdateStatusBarColor( Color targetColor )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "This function will update the status bar's color to being the new targeted color. The parameter, targetColor, must be passed through to allow the function to change to the new color.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// UpdateStatusBarTextColor
		EditorGUILayout.LabelField( "UpdateStatusBarTextColor( Color targetColor )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "If you are using the text of a status, you can update the color of it at runtime using this function. The parameter, targetColor, must be assigned in order for the text to change colors correctly.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// AlternateState
		EditorGUILayout.LabelField( "AlternateState( bool targetState )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "This function will only work if you have an option other than None for the Alternate Status option. If you would like to manually change the state of the Ultimate Status Bar, then you can call this function with the desired state that you want the status in. Passing in true for the target state will change to the alternate state, whereas passing false will change back to the default state.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 20 );
		
		/* //// --------------------------- < STATIC FUNCTIONS > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Static Functions", GUI.skin.GetStyle( "SectionHeader" ) );

		GUILayout.Space( 5 );

		EditorGUILayout.LabelField( "   All static functions require a string to be passed through the function first. The statusName parameter is used to locate the targeted Ultimate Status Bar from a static list of Ultimate Status Bar that has been stored.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// UpdateStatusBar
		EditorGUILayout.LabelField( "UpdateStatusBar( string statusName, float currentValue, float maxValue )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Calls the UpdateStatusBar() function of the targeted Ultimate Status Bar. See the public function, UpdateStatusBar(), for more details.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// UpdateStatusBarColor
		EditorGUILayout.LabelField( "UpdateStatusBarColor( string statusName, Color targetColor )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Calls the UpdateStatusBarColor() function of the targeted Ultimate Status Bar. See the public function, UpdateStatusBarColor(), for more details.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// UpdateStatusBarTextColor
		EditorGUILayout.LabelField( "UpdateStatusBarTextColor( string statusName, Color targetColor )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Calls the UpdateStatusBarTextColor() function of the targeted Ultimate Status Bar. See the public function, UpdateStatusBarTextColor(), for more details.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// AlternateState
		EditorGUILayout.LabelField( "AlternateState( string statusName, bool targetState )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Calls the AlternateState() function of the targeted Ultimate Status Bar. See the public function, AlternateState(), for more details.", EditorStyles.wordWrappedLabel );
	}

	void UltimateStatusBarControllerDocumentation ()
	{
		/* //// --------------------------- < PUBLIC FUNCTIONS > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Public Functions", GUI.skin.GetStyle( "SectionHeader" ) );

		GUILayout.Space( 5 );

		// UpdatePositioning
		EditorGUILayout.LabelField( "UpdatePositioning()", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Updates the size and positioning of the Ultimate Status Bar. This function can be used to update any options that may have been changed prior to Start(). Any changes made to the size or positioning variables at runtime, can be applied to the Ultimate Status Bar Controller by calling this function.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// UpdateStatusBarName
		EditorGUILayout.LabelField( "UpdateStatusBarName( string newName )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Updates the text component with the desired name passed as the newName parameter. This function will only work correctly if the Status Bar Text component is assigned in the Assigned Variables section.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// UpdateStatusBarIcon
		EditorGUILayout.LabelField( "UpdateStatusBarIcon( Sprite newIcon )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Updates the Status Bar Icon with the new sprite passed through the function. This function will only work if the Status Bar Icon variable is assigned.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// ShowStatusBar
		EditorGUILayout.LabelField( "ShowStatusBar()", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Shows the Ultimate Status Bar using the alpha of the Canvas Group component.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// HideStatusBar
		EditorGUILayout.LabelField( "HideStatusBar()", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Hides the Ultimate Status Bar using the alpha of the Canvas Group component.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 20 );
		
		/* //// --------------------------- < STATIC FUNCTIONS > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Static Functions", GUI.skin.GetStyle( "SectionHeader" ) );

		GUILayout.Space( 5 );

		EditorGUILayout.LabelField( "   All static functions require a string to be passed through the function first. The controllerName parameter is used to locate the targeted Ultimate Status Bar Controller from a static list of Ultimate Status Bar Controllers that has been stored.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// UpdatePositioning
		EditorGUILayout.LabelField( "UpdatePositioning( string controllerName )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Calls the UpdatePositioning() function of the targeted Ultimate Status Bar Controller. See the public function, UpdatePositioning(), for more details.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		// UpdateStatusBarName
		EditorGUILayout.LabelField( "UpdateStatusBarName( string controllerName, string newName )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Calls the UpdateStatusBarName() function of the targeted Ultimate Status Bar Controller, passing in the desired string as the name. See the public function, UpdateStatusBarName() for more information.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// UpdateStatusBarIcon
		EditorGUILayout.LabelField( "UpdateStatusBarIcon( string controllerName, Sprite newIcon )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Calls the UpdateStatusBarIcon() function of the targeted Ultimate Status Bar Controller, passing in the desired Sprite as the new icon. See the public function, UpdateStatusBarIcon() for more information.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// ShowStatusBar
		EditorGUILayout.LabelField( "ShowStatusBar( string controllerName )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Calls the ShowStatusBar() function of the targeted Ultimate Status Bar Controller. See the public function, ShowStatusBar(), for more details.", EditorStyles.wordWrappedLabel );
		
		GUILayout.Space( 5 );

		// HideStatusBar
		EditorGUILayout.LabelField( "HideStatusBar( string controllerName )", GUI.skin.GetStyle( "ItemHeader" ) );
		EditorGUILayout.LabelField( "Calls the HideStatusBar() function of the targeted Ultimate Status Bar Controller. See the public function, HideStatusBar(), for more details.", EditorStyles.wordWrappedLabel );
	}
	#endregion

	#region Extras
	void Extras ()
	{
		scroll_Extras = EditorGUILayout.BeginScrollView( scroll_Extras, false, false, docSize );

		EditorGUILayout.LabelField( "Videos", GUI.skin.GetStyle( "SectionHeader" ) );
		EditorGUILayout.LabelField( "   The links below are to the collection of videos that we have made in connection with the Ultimate Status Bar. The Tutorial Videos are designed to get the Ultimate Status Bar implemented into your project as fast as possible, and give you a good understanding of what you can achieve using it in your projects, whereas the demonstrations are videos showing how we, and others in the Unity community, have used assets created by Tank & Healer Studio in our projects.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Tutorials", buttonSize ) )
			Application.OpenURL( "https://www.youtube.com/playlist?list=PL7crd9xMJ9Tl0VRLpo3VoU2U-SbLgwB3-" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Demonstrations", buttonSize ) )
			Application.OpenURL( "https://www.youtube.com/playlist?playnext=1&list=PL7crd9xMJ9TlkjepDAY_GnpA1CX-rFltz" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndScrollView();
	}
	#endregion
	
	#region OtherProducts
	void OtherProducts ()
	{
		scroll_OtherProd = EditorGUILayout.BeginScrollView( scroll_OtherProd, false, false, docSize );

		/* ------------ < ULTIMATE JOYSTICK > ------------ */
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Space( 15 );
		GUILayout.Label( ujPromo, GUILayout.Width( 250 ), GUILayout.Height( 125 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 5 );

		EditorGUILayout.LabelField( "Ultimate Joystick", GUI.skin.GetStyle( "SectionHeader" ) );

		EditorGUILayout.LabelField( "   The Ultimate Joystick is a simple, yet powerful tool for the development of your mobile games. The Ultimate Joystick was created with the goal of giving Unity Developers a incredibly versatile joystick solution, while being extremely easy to implement into existing, or new scripts. You don't need to be a programmer to work with the Ultimate Joystick, and it is very easy to implement into any type of character controller that you need. Additionally, Ultimate Joystick's source code is extremely well commented, easy to modify, and has complete documentation, making it ideal for game-specific adjustments. All in all, with Ultimate Joystick you can't go wrong!", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "More Info", buttonSize ) )
			Application.OpenURL( "http://www.tankandhealerstudio.com/ultimate-joystick.html" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		/* -------------- < END ULTIMATE JOYSTICK > --------------- */

		GUILayout.Space( 25 );

		/* -------------- < ULTIMATE BUTTON > -------------- */
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Space( 15 );
		GUILayout.Label( ubPromo, GUILayout.Width( 250 ), GUILayout.Height( 125 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 5 );

		EditorGUILayout.LabelField( "Ultimate Button", GUI.skin.GetStyle( "SectionHeader" ) );

		EditorGUILayout.LabelField( "   Buttons are a core element of UI, and as such they should be easy to customize and implement. The Ultimate Button is the embodiment of that very idea. This code package takes the best of Unity's Input and UnityEvent methods and pairs it with exceptional customization to give you the most versatile button for your mobile project. Are you in need of a button for attacking, jumping, shooting, or all of the above? With Ultimate Button's easy size and placement options, style options, and touch actions, you'll have everything you need to create your custom buttons, whether they are simple or complex.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "More Info", buttonSize ) )
			Application.OpenURL( "http://www.tankandhealerstudio.com/ultimate-button.html" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		/* ------------ < END ULTIMATE BUTTON > ------------ */

		GUILayout.Space( 25 );

		/* -------------- < FROST STONE TEXTURE PACK > -------------- */
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Space( 15 );
		GUILayout.Label( fstpPromo, GUILayout.Width( 250 ), GUILayout.Height( 125 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 5 );

		EditorGUILayout.LabelField( "Frost Stone: UI Texture Pack", GUI.skin.GetStyle( "SectionHeader" ) );

		EditorGUILayout.LabelField( "   This package is made to compliment Ultimate Joystick, Ultimate Button and Ultimate Status Bar. The Frost Stone: UI Texture Pack is an inspiring new look for your Ultimate Joystick, Ultimate Button and Ultimate Status Bar. These Frost Stone Textures will flawlessly blend with your current Ultimate UI code to give your game an incredible new look.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "More Info", buttonSize ) )
			Application.OpenURL( "http://www.tankandhealerstudio.com/frost-stone-texture-pack.html" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		/* ------------ < END FROST STONE TEXTURE PACK > ------------ */
		GUILayout.Space( 20 );

		EditorGUILayout.EndScrollView();
	}
	#endregion

	#region Feedback
	void Feedback ()
	{
		scroll_Feedback = EditorGUILayout.BeginScrollView( scroll_Feedback, false, false, docSize );

		EditorGUILayout.LabelField( "Having Problems?", GUI.skin.GetStyle( "SectionHeader" ) );

		EditorGUILayout.LabelField( "   If you experience any issues with the Ultimate Status Bar, please contact us right away at tankandhealerstudio@outlook.com. We will lend any assistance that we can to resolve any issues that you have.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		EditorGUILayout.LabelField( "Support Email:\n    tankandhealerstudio@outlook.com" , EditorStyles.boldLabel, GUILayout.Height( 30 ) );

		GUILayout.Space( 25 );

		EditorGUILayout.LabelField( "Good Experiences?", GUI.skin.GetStyle( "SectionHeader" ) );

		EditorGUILayout.LabelField( "   If you have appreciated how easy the Ultimate Status Bar is to get into your project, leave us a comment and rating on the Unity Asset Store. We are very grateful for all positive feedback that we get.", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Rate Us", buttonSize ) )
			Application.OpenURL( "https://www.assetstore.unity3d.com/en/#!/content/48320" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 25 );

		EditorGUILayout.LabelField( "Show Us What You've Done!", GUI.skin.GetStyle( "SectionHeader" ) );

		EditorGUILayout.LabelField( "   If you have used any of the assets created by Tank & Healer Studio in your project, we would love to see what you have done. Contact us with any information on your game and we will be happy to support you in any way that we can!", EditorStyles.wordWrappedLabel );

		GUILayout.Space( 5 );

		EditorGUILayout.LabelField( "Contact Us:\n    tankandhealerstudio@outlook.com" , EditorStyles.boldLabel, GUILayout.Height( 30 ) );

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		EditorGUILayout.LabelField( "Happy Game Making,\n	-Tank & Healer Studio", GUILayout.Height( 30 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 25 );

		EditorGUILayout.EndScrollView();
	}
	#endregion

	#region ThankYou
	void ThankYou ()
	{
		scroll_Thanks = EditorGUILayout.BeginScrollView( scroll_Thanks, false, false, docSize );

		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "We here at Tank & Healer Studio would like to thank you for purchasing the Ultimate Status Bar asset package from the Unity Asset Store. If you have any questions about this product please don't hesitate to contact us at: ", EditorStyles.wordWrappedLabel );

		EditorGUILayout.LabelField( "tankandhealerstudio@outlook.com" , EditorStyles.boldLabel );

		EditorGUILayout.LabelField( "\nWe hope that the Ultimate Status Bar will be a great help to you in the development of your game. After pressing the continue button below, you will be presented with helpful information on this asset to assist you in implementing it into your project.\n", EditorStyles.wordWrappedLabel );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		EditorGUILayout.LabelField( "Happy Game Making,\n	-Tank & Healer Studio", GUILayout.Height( 30 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 15 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Continue", buttonSize ) )
		{
			EditorPrefs.SetBool( "UltimateStatusBarStartup", true );
			Selection.activeObject = AssetDatabase.LoadMainAssetAtPath( "Assets/Plugins/Ultimate Status Bar/README.txt" );

			currentMenu = CurrentMenu.MainMenu;
			menuTitle = "Main Menu";
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndScrollView();
	}
	#endregion
	
	[InitializeOnLoad]
	class UltimateStatusBarInitialLoad
	{
		static UltimateStatusBarInitialLoad ()
		{
			if( EditorPrefs.GetBool( "UltimateStatusBarStartup" ) == false )
				EditorApplication.update += WaitForCompile;
		}

		static void WaitForCompile ()
		{
			if( EditorApplication.isCompiling )
				return;

			EditorApplication.update -= WaitForCompile;
				
			currentMenu = CurrentMenu.ThankYou;
			menuTitle = "Thank You!";

			InitializeWindow();

			Selection.activeObject = AssetDatabase.LoadMainAssetAtPath( "Assets/Plugins/Ultimate Status Bar/README.txt" );
		}
	}
}