Thank you for purchasing the Ultimate Status Bar UnityPackage!

If you need some help getting started, please feel free to email us at support@tankandhealerstudio.com.

/* ------- < IMPORTANT INFORMATION > ------- */
Within Unity, please go to Window / Ultimate UI / Ultimate Status Bar to access important information on how to get started using the Ultimate Joystick. There is
a ton of information available to help you get the Ultimate Status Bar into your project as fast as possible. However, if you can't view the in-engine documentation
window, please see the information below.
/* ----- < END IMPORTANT INFORMATION > ----- */


// --- IF YOU CAN'T VIEW THE ULTIMATE JOYSTICK WINDOW, READ THIS SECTION --- //

	// --- HOW TO CREATE --- //
To create a Ultimate Status Bar within your scene, just go up to GameObject / UI / Ultimate UI / Ultimate Status Bar. What this will do is locate a Ultimate Status
Bar prefab that is located within the Editor Default Resources folder, and created a Ultimate Status Bar within the scene. This method of adding an Ultimate Status
Bar to your scene ensures that the status bars will have a Canvas and an EventSystem so that it can work correctly.

	// --- HOW TO REFERENCE --- //
The Ultimate Status Bar is incredibly easy to get implemented into your custom scripts. The first thing that you'll want to make sure to do is give the status a
unique name. This is done in the Script Reference section located within the Inspector window. As an example, let's implement the Ultimate Status Bar into a character's
health. First off, we'll name an Ultimate Status Bar within our scene 'Health'. After this is done, you can copy and paste the code provided inside the Script Reference
section. Make sure that the Script Use option is set to Update Status. After copying the code that is provided, find the function where your player is receiving damage
from and paste the example code into the function.

Be sure to put it after the damage or healing has modified the health value. Of course, be sure to replace the currentValue and maxValue of the example code with your
character's current and maximum health values. Whenever the character's health is updated, either by damage or healing done to the character, you will want to send the
new information of the health's value. This process can be used for any status that you need to be displayed to the user. For more information about the individual
functions available to both the Ultimate Status Bar and the Ultimate Status Bar Controller classes, please refer to the Documentation section of this window.


/* ------------------< CHANGE >------------------ */
/* --------------------< LOG >------------------- */
UltimateStatusBar.cs
	v1.0.1 - Created seperate funciton for showing text so that it can be called from multiple functions.
	v1.0.2 - Beginning smooth fill options.
	v1.0.3 - Added fill constraints.
	v1.0.4 - Total redo of all functionality to accept fill contraints to all options.
	v1.0.5 - Final script cleanup for release. Added comments for user's benefit.
	v1.0.6 - Cleaning up script more to be easier to understand and more performant.
	v1.0.7 - Updated the function that registers the status bars to be correct.
	v1.0.8 - Complete overhaul to all functionality.

UltimateStatusBarController.cs
	v1.0.4 - Remove CanvasScaler support to avoid errors is Unity 5.3.3+.
	v1.0.5 - Complete overhaul to all functionality.