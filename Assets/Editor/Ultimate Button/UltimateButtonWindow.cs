/* Written by Kaz Crowe */
/* UltimateButtonWindow.cs ver 2.0.1 */
using UnityEngine;
using UnityEditor;

public class UltimateButtonWindow : EditorWindow
{
	static string ubVersionNumber = "2.1.0";// ALWAYS UDPATE
	static int importantChanges = 1;// UPDATE ON IMPORTANT CHANGES

	GUILayoutOption[] buttonSize = new GUILayoutOption[] { GUILayout.Width( 200 ), GUILayout.Height( 35 ) }; 

	GUILayoutOption[] docSize = new GUILayoutOption[] { GUILayout.Width( 300 ), GUILayout.Height( 330 ) };

	GUISkin style;

	GUIStyle wordWrapped, headerText, sectionHeader;
	GUIStyle boldTitle, versionNumber, windowTitle, backButton;

	enum CurrentMenu
	{
		MainMenu,
		HowTo,
		Overview,
		Documentation,
		Extras,
		OtherProducts,
		Feedback,
		ThankYou,
		VersionChanges
	}
	static CurrentMenu currentMenu;
	static string menuTitle = "Main Menu";

	Texture2D scriptReference;

	Texture2D ujPromo, usbPromo, fstpPromo;

	Vector2 scroll_HowTo = Vector2.zero, scroll_Overview = Vector2.zero, scroll_Docs = Vector2.zero, scroll_Extras = Vector2.zero;
	Vector2 scroll_OtherProd = Vector2.zero, scroll_Feedback = Vector2.zero, scroll_Thanks = Vector2.zero;

	int smallSpace = 8;
	int mediumSpace = 12;
	int largeSpace = 20;


	[MenuItem( "Window/Tank and Healer Studio/Ultimate Button", false, 20 )]
	static void RequestWindow ()
	{
		InitializeWindow();
	}

	static void InitializeWindow ()
	{
		EditorWindow window = GetWindow<UltimateButtonWindow>( true, "Tank and Healer Studio Asset Window", true );
		window.maxSize = new Vector2( 500, 500 );
		window.minSize = new Vector2( 500, 500 );
		window.Show();
	}

	public static void ShowDocumentationWindow ()
	{
		InitializeWindow();
		currentMenu = CurrentMenu.Documentation;
		menuTitle = "Documentation";
	}

	void OnEnable ()
	{
		style = ( GUISkin )EditorGUIUtility.Load( "Ultimate Button/UltimateButtonEditorSkin.guiskin" );

		scriptReference = ( Texture2D )EditorGUIUtility.Load( "Ultimate Button/UltimateButtonScriptReference.jpg" );
		ujPromo = ( Texture2D )EditorGUIUtility.Load( "Ultimate UI/UJ_Promo.png" );
		usbPromo = ( Texture2D )EditorGUIUtility.Load( "Ultimate UI/USB_Promo.png" );
		fstpPromo = ( Texture2D )EditorGUIUtility.Load( "Ultimate UI/FSTP_Promo.png" );

		if( style != null )
		{
			wordWrapped = style.GetStyle( "NormalWordWrapped" );
			headerText = style.GetStyle( "HeaderText" );
			sectionHeader = style.GetStyle( "SectionHeader" );
			boldTitle = style.GetStyle( "BoldTitle" );
			versionNumber = style.GetStyle( "VersionNumber" );
			windowTitle = style.GetStyle( "WindowTitle" );
			backButton = style.GetStyle( "BackButton" );
		}
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
		
		EditorGUILayout.LabelField( "Ultimate Button", windowTitle );

		GUILayout.Space( 3 );

		EditorGUILayout.LabelField( "Version " + ubVersionNumber, versionNumber );

		GUILayout.Space( 12 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.Space( 5 );
		if( currentMenu != CurrentMenu.MainMenu && currentMenu != CurrentMenu.ThankYou )
		{
			EditorGUILayout.BeginVertical();
			GUILayout.Space( 5 );
			if( GUILayout.Button( "", backButton, GUILayout.Width( 80 ), GUILayout.Height( 40 ) ) )
				BackToMainMenu();
			EditorGUILayout.EndVertical();
		}
		else
			GUILayout.Space( 80 );

		GUILayout.Space( 15 );
		EditorGUILayout.BeginVertical();
		GUILayout.Space( 14 );
		EditorGUILayout.LabelField( menuTitle, headerText );
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
				Overview();
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
			case CurrentMenu.VersionChanges:
			{
				VersionChanges();
			}break;
			default:
			{
				MainMenu();
			}break;
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		GUILayout.Space( 20 );
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
		EditorGUILayout.LabelField( "Could not find the needed GUISkin located in the Editor Default Resources folder. Please ensure that the correct GUISkin, UltimateButtonEditorSkin, is in the right folder( Editor Default Resources/Ultimate Button ) before trying to access the Ultimate Button Window.", wordWrapped );
		GUILayout.Space( 50 );
		EditorGUILayout.EndHorizontal();
	}

	void BackToMainMenu ()
	{
		currentMenu = CurrentMenu.MainMenu;
		menuTitle = "Main Menu";
	}
	
	#region MainMenu
	void MainMenu ()
	{
		EditorGUILayout.BeginVertical();

		GUILayout.FlexibleSpace();

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

		EditorGUILayout.LabelField( "How To Create", sectionHeader );

		EditorGUILayout.LabelField( "   To create a Ultimate Button in your scene, simply go up to GameObject / UI / Ultimate UI / Ultimate Button. What this does is locates the Ultimate Button prefab that is located within the Editor Default Resources folder, and creates an Ultimate Button within the scene. Alternatively, you can locate the Prefabs folder within the Ultimate Button files and simply drag and drop Prefab out into the Hierarchy window. This will create an Ultimate Button, and create a Canvas and EventSystem if one is not already present.", wordWrapped );

		EditorGUILayout.LabelField( "This method of adding an Ultimate Button to your scene ensures that the button will have a Canvas and an EventSystem so that it can work correctly.", wordWrapped );

		GUILayout.Space( largeSpace );

		EditorGUILayout.LabelField( "How To Reference", sectionHeader );
		EditorGUILayout.LabelField( "   One of the great things about the Ultimate Button is how easy it is to reference to other scripts. The first thing that you will want to make sure to do is determine how you want to use the Ultimate Button within your scripts. If you are used to using the Events that are used in Unity's default UI buttons, then you may want to use the Unity Events options located within the Button Events section of the Ultimate Button inspector. However, if you are used to using Unity's Input system for getting input, then the Script Reference section would probably suit you better.", wordWrapped );

		GUILayout.Space( smallSpace );

		EditorGUILayout.LabelField( "For this example, we'll go over how to use the Script Reference section. First thing to do is assign the Button Name within the Script Reference section. After this is complete, you will be able to reference that particular button by it's name from a static function within the Ultimate Button script.", wordWrapped );

		GUILayout.Space( largeSpace );

		EditorGUILayout.LabelField( "Example", sectionHeader );

		EditorGUILayout.LabelField( "   If you are going to use the Ultimate Button for making a player jump, then you will need to check the button's state to determine when the user has touched the button and is wanting the player to jump. So for this example, let's assign the name \"Jump\" in the Script Reference section of the Ultimate Button.", wordWrapped );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label( scriptReference );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( mediumSpace );

		EditorGUILayout.LabelField( "There are several functions that allow you to check the different states that the Ultimate Button is in. For more information on all the functions that you have available to you, please see the documentation section of this Help Window.", wordWrapped );

		GUILayout.Space( smallSpace );

		EditorGUILayout.LabelField( "For this example we will be using the GetButtonDown function to see if the user has pressed down on the button. It is worth noting that this function is useful when wanting to make the player start the jump action on the exact frame that the user has pressed down on the button, and not after at all.", wordWrapped );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.LabelField( "C# and Javascript Example:", boldTitle );
		EditorGUILayout.TextArea( "if( UltimateButton.GetButtonDown( \"Jump\" ) )\n{\n	// Call player jump function.\n}", GUI.skin.GetStyle( "TextArea" ) );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.LabelField( "Feel free to experiment with the different functions of the Ultimate Button to get it working exactly the way you want to. Additionally, if you are curious about how the Ulimate Button has been implemented into an Official Tank and Healer Studio example, then please see the README.txt that is included with the example files for the project.", wordWrapped );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.EndScrollView();
	}
	#endregion
	
	#region Overview
	void Overview ()
	{
		scroll_Overview = EditorGUILayout.BeginScrollView( scroll_Overview, false, false, docSize );

		EditorGUILayout.LabelField( "Assigned Variables", sectionHeader );
		EditorGUILayout.LabelField( "   In the Assigned Variables section, there are a few components that should already be assigned if you are using one of the Prefabs that has been provided. If not, you will see error messages on the Ultimate Button inspector that will help you to see if any of these variables are left unassigned. Please note that these need to be assigned in order for the Ultimate Button to work properly.", wordWrapped );

		GUILayout.Space( largeSpace );
		
		/* //// --------------------------- < SIZE AND PLACEMENT > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Size And Placement", sectionHeader );
		EditorGUILayout.LabelField( "   The Size and Placement section allows you to customize the button's size and placement on the screen, as well as determine where the user's touch can be processed for the selected button.", wordWrapped );

		GUILayout.Space( smallSpace );

		// Scaling Axis
		EditorGUILayout.LabelField( "« Scaling Axis »", boldTitle );
		EditorGUILayout.LabelField( "Determines which axis the button will be scaled from. If Height is chosen, then the button will scale itself proportionately to the Height of the screen.", wordWrapped );

		GUILayout.Space( smallSpace );

		// Anchor
		EditorGUILayout.LabelField( "« Anchor »", boldTitle );
		EditorGUILayout.LabelField( "Determines which side of the screen that the button will be anchored to.", wordWrapped );
		
		GUILayout.Space( smallSpace );

		// Touch Size
		EditorGUILayout.LabelField( "« Touch Size »", boldTitle );
		EditorGUILayout.LabelField( "Touch Size configures the size of the area where the user can touch. You have the options of either 'Default','Medium', or 'Large'.", wordWrapped );

		GUILayout.Space( smallSpace );
		
		// Button Size
		EditorGUILayout.LabelField( "« Button Size »", boldTitle );
		EditorGUILayout.LabelField( "Button Size will change the scale of the button. Since everything is calculated out according to screen size, your Touch Size option and other properties will scale proportionately with the button's size along your specified Scaling Axis.", wordWrapped );
		
		GUILayout.Space( smallSpace );

		// Button Position
		EditorGUILayout.LabelField( "« Button Position »", boldTitle );
		EditorGUILayout.LabelField( "Button Position will present you with two sliders. The X value will determine how far the button is away from the Left and Right sides of the screen, and the Y value from the Top and Bottom. This will encompass 50% of your screen, relevant to your Anchor selection.", wordWrapped );
		/* \\\\ -------------------------- < END SIZE AND PLACEMENT > --------------------------- //// */

		GUILayout.Space( largeSpace );

		/* //// ----------------------------- < STYLE AND OPTIONS > ----------------------------- \\\\ */
		EditorGUILayout.LabelField( "Style And Options", sectionHeader );
		EditorGUILayout.LabelField( "   The Style and Options section contains options that affect how the button is visually presented to the user.", wordWrapped );
		
		GUILayout.Space( smallSpace );

		// Image Style
		EditorGUILayout.LabelField( "« Image Style »", boldTitle );
		EditorGUILayout.LabelField( "Determines whether the input range should be circular or square. This option affects how the Input Range and Track Input options function.", wordWrapped );

		GUILayout.Space( smallSpace );

		// Input Range
		EditorGUILayout.LabelField( "« Input Range »", boldTitle );
		EditorGUILayout.LabelField( "The range that the Ultimate Button will react to when initiating and dragging the input.", wordWrapped );

		GUILayout.Space( smallSpace );

		// Track Input
		EditorGUILayout.LabelField( "« Track Input »", boldTitle );
		EditorGUILayout.LabelField( "If the Track Input option is enabled, then the Ultimate Button will reflect it's state according to where the user's input currently is. This means that if the input moves off of the button, then the button state will turn to false. When the input returns to the button the state will return to true. If the Track Input option is disabled, then the button will reflect the state of only pressing and releasing the button.", wordWrapped );

		GUILayout.Space( smallSpace );

		// Transmit Input
		EditorGUILayout.LabelField( "« Transmit Input »", boldTitle );
		EditorGUILayout.LabelField( "The Transmit Input option will allow you to send the input data to another script that uses Unity's EventSystem. For example, if you are using the Ultimate Joystick package, you could place the Ultimate Button on top of the Ultimate Joystick, and still have the Ultimate Button and Ultimate Joystick function correctly when interacted with.", wordWrapped );

		GUILayout.Space( smallSpace );

		// Tap Count Option
		EditorGUILayout.LabelField( "« Tap Count Option »", boldTitle );
		EditorGUILayout.LabelField( "The Tap Count option allows you to decide if you want to store the amount of taps that the button recieves. The options provided with the Tap Count will allow you to customize the target amount of taps, the tap time window, and the event to be called when the tap count has been achieved.", wordWrapped );
		
		GUILayout.Space( smallSpace );

		// Base Color
		EditorGUILayout.LabelField( "« Base Color »", boldTitle );
		EditorGUILayout.LabelField( "The Base Color option determines the color of the button base images.", wordWrapped );

		GUILayout.Space( smallSpace );

		// Show Highlight
		EditorGUILayout.LabelField( "« Show Highlight »", boldTitle );
		EditorGUILayout.LabelField( "Show Highlight will allow you to customize the set highlight images with a custom color. With this option, you will also be able to customize and set these images at runtime using the UpdateHighlightColor function. See the Documentation section for more details.", wordWrapped );

		GUILayout.Space( smallSpace );
		
		// Show Tension
		EditorGUILayout.LabelField( "« Show Tension »", boldTitle );
		EditorGUILayout.LabelField( "With Show Tension enabled, the button will display interactions visually using custom colors and images to display the intensity of the press. With this option enabled, you will be able to update the tension colors at runtime using the UpdateTensionColors function. See the Documentation section for more information.", wordWrapped );
		
		GUILayout.Space( smallSpace );

		// Use Animation
		EditorGUILayout.LabelField( "« Use Animation »", boldTitle );
		EditorGUILayout.LabelField( "If you would like the button to play an animation when being interacted with, then you will want to enable the Use Animation option.", wordWrapped );
		
		GUILayout.Space( smallSpace );

		// Use Fade
		EditorGUILayout.LabelField( "« Use Fade »", boldTitle );
		EditorGUILayout.LabelField( "The Use Fade option will present you with settings for the targeted alpha for the touched and untouched states, as well as the duration for the fade between the targeted alpha settings.", wordWrapped );
		/* //// --------------------------- < END STYLE AND OPTIONS > --------------------------- \\\\ */

		GUILayout.Space( largeSpace );

		/* //// ----------------------------- < SCRIPT REFERENCE > ------------------------------ \\\\ */
		EditorGUILayout.LabelField( "Script Reference", sectionHeader );
		EditorGUILayout.LabelField( "   The Script Reference section contains fields for naming and helpful code snippets that you can copy and paste into your scripts. Be sure to refer to the Documentation Window for information about the functions that you have available to you.", wordWrapped );
		
		GUILayout.Space( smallSpace );
		
		// Button Name
		EditorGUILayout.LabelField( "« Button Name »", boldTitle );
		EditorGUILayout.LabelField( "The unique name of your Ultimate Button. This name is what will be used to reference this particular button from the public static functions.", wordWrapped );
		
		GUILayout.Space( smallSpace );

		// Example Code
		EditorGUILayout.LabelField( "« Example Code »", boldTitle );
		EditorGUILayout.LabelField( "This section will present you with code snippets that are determined by your selection. This code can be copy and pasted into your custom scripts. Please note that this section is only designed to help you get the Ultimate Button working in your scripts quickly. Any options within this section do have affect the actual functionality of the button.", wordWrapped );
		/* //// --------------------------- < END SCRIPT REFERENCE > ---------------------------- \\\\ */

		GUILayout.Space( largeSpace );

		/* //// ------------------------------- < BUTTON EVENTS > ------------------------------- \\\\ */
		EditorGUILayout.LabelField( "Button Events", sectionHeader );
		EditorGUILayout.LabelField( "   The Button Events section contains Unity Events that can be created for when the Ultimate Button is pressed and released. Also, if you have the Tap Count Option set, then you can assign a Unity Event for the Tap Count Event option.", wordWrapped );
		
		GUILayout.Space( mediumSpace );
		/* //// ----------------------------- < END BUTTON EVENTS > ----------------------------- \\\\ */
		EditorGUILayout.EndScrollView();
	}
	#endregion
	
	#region Documentation
	void Documentation ()
	{
		scroll_Docs = EditorGUILayout.BeginScrollView( scroll_Docs, false, false, docSize );
		
		/* //// --------------------------- < PUBLIC FUNCTIONS > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Public Functions", sectionHeader );

		GUILayout.Space( smallSpace );

		// UpdatePositioning()
		EditorGUILayout.LabelField( "UpdatePositioning()", boldTitle );
		EditorGUILayout.LabelField( "Updates the size and positioning of the Ultimate Button. This function can be used to update any options that may have been changed prior to Start().", wordWrapped );

		GUILayout.Space( smallSpace );

		// UpdateBaseColor()
		EditorGUILayout.LabelField( "UpdateBaseColor( Color targetColor )", boldTitle );
		EditorGUILayout.LabelField( "Updates the color of the assigned button base images with the targeted color. The targetColor option will overwrite the current setting for base color.", wordWrapped );

		GUILayout.Space( smallSpace );

		// UpdateHighlightColor()
		EditorGUILayout.LabelField( "UpdateHighlightColor( Color targetColor )", boldTitle );
		EditorGUILayout.LabelField( "Updates the colors of the assigned highlight images with the targeted color if the showHighlight variable is set to true. The targetColor variable will overwrite the current color setting for highlightColor and apply immidiately to the highlight images.", wordWrapped );
				
		GUILayout.Space( smallSpace );

		// UpdateTensionColors()
		EditorGUILayout.LabelField( "UpdateTensionColors( Color targetTensionNone, Color targetTensionFull )", boldTitle );
		EditorGUILayout.LabelField( "Updates the tension accent image colors with the targeted colors if the showTension variable is true. The tension colors will be set to the targeted colors, and will be applied when the button is next used.", wordWrapped );

		GUILayout.Space( largeSpace );
		
		/* //// --------------------------- < STATIC FUNCTIONS > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Static Functions", sectionHeader );

		GUILayout.Space( smallSpace );

		// UltimateButton.GetUltimateButton
		EditorGUILayout.LabelField( "UltimateButton UltimateButton.GetUltimateButton( string buttonName )", boldTitle );
		EditorGUILayout.LabelField( "Returns the Ultimate Button registered with the buttonName string. This function can be used to call local functions on the Ultimate Button to apply color changes or position updates at runtime.", wordWrapped );
						
		GUILayout.Space( smallSpace );

		// UltimateButton.GetButtonDown
		EditorGUILayout.LabelField( "bool UltimateButton.GetButtonDown( string buttonName )", boldTitle );
		EditorGUILayout.LabelField( "Returns true on the frame that the targeted Ultimate Button is pressed down.", wordWrapped );
						
		GUILayout.Space( smallSpace );
		
		// UltimateButton.GetButtonUp
		EditorGUILayout.LabelField( "bool UltimateButton.GetButtonUp( string buttonName )", boldTitle );
		EditorGUILayout.LabelField( "Returns true on the frame that the targeted Ultimate Button is released.", wordWrapped );
						
		GUILayout.Space( smallSpace );
		
		// UltimateButton.GetButton
		EditorGUILayout.LabelField( "bool UltimateButton.GetButton( string buttonName )", boldTitle );
		EditorGUILayout.LabelField( "Returns true on the frames that the targeted Ultimate Button is being interacted with.", wordWrapped );
						
		GUILayout.Space( smallSpace );
				
		// UltimateButton.GetTapCount
		EditorGUILayout.LabelField( "bool UltimateButton.GetTapCount( string buttonName )", boldTitle );
		EditorGUILayout.LabelField( "Returns true on the frame that the Tap Count option has been achieved.", wordWrapped );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.EndScrollView();
	}
	#endregion
	
	#region Extras
	void Extras ()
	{
		scroll_Extras = EditorGUILayout.BeginScrollView( scroll_Extras, false, false, docSize );
		EditorGUILayout.LabelField( "Videos", sectionHeader );
		EditorGUILayout.LabelField( "   The links below are to the collection of videos that we have made in connection with the Ultimate Button. The Tutorial Videos are designed to get the Ultimate Button implemented into your project as fast as possible, and give you a good understanding of what you can achieve using it in your projects, whereas the demonstrations are videos showing how we, and others in the Unity community, have used assets created by Tank & Healer Studio in our projects.", wordWrapped );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Tutorials", buttonSize ) )
			Application.OpenURL( "https://www.youtube.com/playlist?playnext=1&list=PL7crd9xMJ9TmBoDnteNyuTLMwUuV6fZa9" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( mediumSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Demonstrations", buttonSize ) )
			Application.OpenURL( "https://www.youtube.com/playlist?list=PL7crd9xMJ9TlkjepDAY_GnpA1CX-rFltz" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndScrollView();
	}
	#endregion
	
	#region OtherProducts
	void OtherProducts ()
	{
		scroll_OtherProd = EditorGUILayout.BeginScrollView( scroll_OtherProd, false, false, docSize );

		/* -------------- < ULTIMATE JOYSTICK > -------------- */
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Space( 15 );
		GUILayout.Label( ujPromo, GUILayout.Width( 250 ), GUILayout.Height( 125 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( smallSpace );

		EditorGUILayout.LabelField( "Ultimate Joystick", sectionHeader );

		EditorGUILayout.LabelField( "   The Ultimate Joystick is a simple, yet powerful tool for the development of your mobile games. The Ultimate Joystick was created with the goal of giving Developers an incredibly versatile joystick solution, while being extremely easy to implement into existing, or new scripts. You don't need to be a programmer to work with the Ultimate Joystick, and it is very easy to implement into any type of character controller that you need. Additionally, Ultimate Joystick's source code is extremely well commented, easy to modify, and features a complete in-engine documentation window, making it ideal for game-specific adjustments. In its entirety, Ultimate Joystick is an elegant and easy to utilize mobile joystick solution.", wordWrapped );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "More Info", buttonSize ) )
			Application.OpenURL( "http://www.tankandhealerstudio.com/ultimate-joystick.html" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		/* ------------ < END ULTIMATE JOYSTICK > ------------ */

		GUILayout.Space( largeSpace );

		/* ------------ < ULTIMATE STATUS BAR > ------------ */
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Space( 15 );
		GUILayout.Label( usbPromo, GUILayout.Width( 250 ), GUILayout.Height( 125 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( smallSpace );

		EditorGUILayout.LabelField( "Ultimate Status Bar", sectionHeader );

		EditorGUILayout.LabelField( "   The Ultimate Status Bar is a complete solution for displaying health, mana, energy, stamina, experience, or virtually any condition that you'd like like your player to be aware of. It can also be used to show a selected target's health, the progress of loading or casting, and even interacting with objects. Whatever type of progress display that you need, the Ultimate Status Bar can make it visually happen. Additionally, you have the option of using the many \"Ultimate\" textures provided, or you can easily use your own! If you are looking for a way to neatly display any type of status for your game, then consider the Ultimate Status Bar!", wordWrapped );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "More Info", buttonSize ) )
			Application.OpenURL( "http://www.tankandhealerstudio.com/ultimate-status-bar.html" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		/* -------------- < END STATUS BAR > --------------- */

		GUILayout.Space( largeSpace );

		/* -------------- < FROST STONE > -------------- */
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Space( 15 );
		GUILayout.Label( fstpPromo, GUILayout.Width( 250 ), GUILayout.Height( 125 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( smallSpace );

		EditorGUILayout.LabelField( "Frost Stone: UI Texture Pack", sectionHeader );

		EditorGUILayout.LabelField( "   This package is made to compliment Ultimate Joystick, Ultimate Button and Ultimate Status Bar. The Frost Stone: UI Texture Pack is an inspiring new look for your Ultimate Joystick, Ultimate Button and Ultimate Status Bar. These Frost Stone Textures will flawlessly blend with your current Ultimate UI code to give your game an incredible new look.", wordWrapped );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "More Info", buttonSize ) )
			Application.OpenURL( "http://www.tankandhealerstudio.com/frost-stone-texture-pack.html" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		/* ------------ < END FROST STONE > ------------ */

		GUILayout.Space( mediumSpace );

		EditorGUILayout.EndScrollView();
	}
	#endregion

	#region Feedback
	void Feedback ()
	{
		scroll_Feedback = EditorGUILayout.BeginScrollView( scroll_Feedback, false, false, docSize );

		EditorGUILayout.LabelField( "Having Problems?", sectionHeader );

		EditorGUILayout.LabelField( "   If you experience any issues with the Ultimate Button, please email us right away. We will lend any assistance that we can to resolve any issues that you have.", wordWrapped );

		GUILayout.Space( smallSpace );
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		EditorGUILayout.LabelField( "tankandhealerstudio@outlook.com", boldTitle, GUILayout.Width( 230 ) );
		GUILayout.Space( 10 );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( smallSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Contact Form", buttonSize ) )
			Application.OpenURL( "http://www.tankandhealerstudio.com/contact-us.html" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( largeSpace );

		EditorGUILayout.LabelField( "Good Experiences?", sectionHeader );

		EditorGUILayout.LabelField( "   If you have appreciated how easy the Ultimate Button is to get into your project, leave us a comment and rating on the Unity Asset Store. We are very grateful for all positive feedback that we get.", wordWrapped );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Rate Us", buttonSize ) )
			Application.OpenURL( "https://www.assetstore.unity3d.com/en/#!/content/28824" );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( largeSpace );

		EditorGUILayout.LabelField( "Show Us What You've Done!", sectionHeader );

		EditorGUILayout.LabelField( "   If you have used any of the assets created by Tank & Healer Studio in your project, we would love to see what you have done. Contact us with any information on your game and we will be happy to support you in any way that we can!", wordWrapped );

		GUILayout.Space( smallSpace );

		EditorGUILayout.LabelField( "Contact Us:\n    tankandhealerstudio@outlook.com" , boldTitle, GUILayout.Height( 30 ) );

		GUILayout.Space( mediumSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		EditorGUILayout.LabelField( "Happy Game Making,\n	-Tank & Healer Studio", GUILayout.Height( 30 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( mediumSpace );

		EditorGUILayout.EndScrollView();
	}
	#endregion

	#region ThankYou
	void ThankYou ()
	{
		scroll_Thanks = EditorGUILayout.BeginScrollView( scroll_Thanks, false, false, docSize );

		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "    The two of us at Tank & Healer Studio would like to thank you for purchasing the Ultimate Button asset package from the Unity Asset Store. If you have any questions about the Ultimate Button that are not covered in this Documentation Window, please don't hesitate to contact us at: ", wordWrapped );

		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "       tankandhealerstudio@outlook.com" , boldTitle );

		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "    We hope that the Ultimate Button will be a great help to you in the development of your game. After pressing the continue button below, you will be presented with helpful information on this asset to assist you in implementing Ultimate Button into your project.\n", wordWrapped );

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
			EditorPrefs.SetBool( "UltimateButtonStartup", true );
			BackToMainMenu();
		}
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndScrollView();
	}
	#endregion

	#region VersionChanges
	void VersionChanges ()
	{
		scroll_Thanks = EditorGUILayout.BeginScrollView( scroll_Thanks, false, false, docSize );
		
		EditorGUILayout.LabelField( "  Thank you for downloading the most recent version of the Ultimate Button. There is some exciting new functionality as well as some changes that could affect any existing reference of the Ultimate Button. Please check out the sections below to see all the important changes that have been made. As always, if you run into any issues with the Ultimate Button, please contact us at:", wordWrapped );

		GUILayout.Space( 3 );
		EditorGUILayout.LabelField( "  TankAndHealerStudio@outlook.com" , boldTitle );
		GUILayout.Space( 15 );

		EditorGUILayout.LabelField( "GENERAL CHANGES", sectionHeader );
		EditorGUILayout.LabelField( "  Some folder structure and existing functionality has been updated and improved. None of these changes should affect any existing use of the Ultimate Button.", wordWrapped );
		EditorGUILayout.LabelField( "  • Removed the Touch Actions section. All options previously located in the Touch Actions section are now located in the Style and Options seciton.", wordWrapped );
		EditorGUILayout.LabelField( "  • Expanded the functionality of using the Ultimate Button in your scripts. Added a new section titled Button Events. Now you can use either the Script Reference or the Button Events section to implement into your scripts.", wordWrapped );
		EditorGUILayout.LabelField( "  • Removed example files from the Plugins folder. All example files will now be in the folder named: Ultimate Button Examples.", wordWrapped );
		EditorGUILayout.LabelField( "  • Added four new Ultimate Button textures that can be used in your projects.", wordWrapped );
		EditorGUILayout.LabelField( "  • Removed the Ultimate Button PSD from the project files.", wordWrapped );
		EditorGUILayout.LabelField( "  • Improved Tension Accent functionality.", wordWrapped );
		
		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "NEW FUNCTIONS", sectionHeader );
		EditorGUILayout.LabelField( "  Some new functions have been added to help reference the Ultimate Button more efficiently. For information on what each new function does, please refer to the Documentation section of this help window.", wordWrapped );
		EditorGUILayout.LabelField( "  • UltimateButton GetUltimateButton()", wordWrapped );

		GUILayout.Space( 15 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Got it!", buttonSize ) )
			BackToMainMenu();
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndScrollView();
	}
	#endregion

	[InitializeOnLoad]
	class UltimateButtonInitialLoad
	{
		static UltimateButtonInitialLoad ()
		{
			// If the user has a older version of Ultimate Button that used the bool for startup...
			if( EditorPrefs.HasKey( "UltimateButtonStartup" ) && !EditorPrefs.HasKey( "UltimateButtonVersion" ) )
			{
				// Set the new pref to 0 so that the pref will exist and the version changes will be shown.
				EditorPrefs.SetInt( "UltimateButtonVersion", 0 );
			}

			// If this is the first time that the user has downloaded the Ultimate Button...
			if( !EditorPrefs.HasKey( "UltimateButtonVersion" ) )
			{
				// Set the current menu to the thank you page.
				currentMenu = CurrentMenu.ThankYou;
				menuTitle = "Thank You";

				// Set the version to current so they won't see these version changes.
				EditorPrefs.SetInt( "UltimateButtonVersion", importantChanges );

				EditorApplication.update += WaitForCompile;
			}
			else if( EditorPrefs.GetInt( "UltimateButtonVersion" ) < importantChanges )
			{
				// Set the current menu to the version changes page.
				currentMenu = CurrentMenu.VersionChanges;
				menuTitle = "Version Changes";

				// Set the version to current so they won't see this page again.
				EditorPrefs.SetInt( "UltimateButtonVersion", importantChanges );

				EditorApplication.update += WaitForCompile;
			}
		}

		static void WaitForCompile ()
		{
			if( EditorApplication.isCompiling )
				return;

			EditorApplication.update -= WaitForCompile;
			
			InitializeWindow();
		}
	}
}