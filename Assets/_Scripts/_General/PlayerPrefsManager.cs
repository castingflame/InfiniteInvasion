//Wrapper for the Unity PlayerPrefs Method
// Paul Land
//****************************************


using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    //Key Pairs
    const string HIGHSCORE_KEY = "highscore";
    const string CONTROLS_SCALE_KEY = "Controls_scale";
    const string CURRENT_CONTROLS_SIZE_KEY = "controls_size";
    //Key Pairs -end


    public static void Initialise(){

        // Initialise Player Preferences
        // If a key does not exist, create it.

        //Player prefs initialisation for Player 'highscore'
        if (PlayerPrefs.HasKey(HIGHSCORE_KEY) == false){        //Prefs missing 
            HighScore_Set(0);                              //Create a new Pref 
            //Debug.Log("PPM: Created Pref for highscore = " + HighScore_Get());
        }
        else if (PlayerPrefs.HasKey(HIGHSCORE_KEY) == true)     //Prefs found
        {
            //Debug.Log("PPM: Found - highscore");
        }



        //Player prefs initialisation for 'Controls Scale'
        if (PlayerPrefs.HasKey(CONTROLS_SCALE_KEY) == false){        //Prefs missing 
            ControlsScale_Set(1);                                    //Create a new Pref 
            //Debug.Log("PPM: Created Pref for Controls Scale = " + ControlsScale_Get());
        }
        else if (PlayerPrefs.HasKey(CONTROLS_SCALE_KEY) == true)     //Prefs found 
        {
            //Debug.Log("PPM: Found - Controls Scale");
        }



        //Player prefs initialisation for Player Controls Size
        if (PlayerPrefs.HasKey(CURRENT_CONTROLS_SIZE_KEY) == false){        //Prefs missing
            ControlsSize_Set(1);                                            //Create a new Pref
            //Debug.Log("PPM: Created Pref for Controls Size = " + ControlsSize_Get());
        }
        else if (PlayerPrefs.HasKey(CURRENT_CONTROLS_SIZE_KEY) == true)     //Prefs found 
        {
            //Debug.Log("PPM: Found - Controls Size");
        }

    }//Initialise() -end




    //HIGH SCORES 
    //***********
    //Set the High Score
    public static void HighScore_Set(float highscore) {
        PlayerPrefs.SetFloat(HIGHSCORE_KEY, highscore);
    }

    
    //Get the High Score
    public static float HighScore_Get() {
        return PlayerPrefs.GetFloat(HIGHSCORE_KEY);
        }


    //Reset the High Score
    public static void HighScore_Reset() {
        //Debug.Log("PPM - Going to reset the High Score ");
        PlayerPrefs.SetFloat(HIGHSCORE_KEY, 0);
    }

    //HIGH SCORES -end





    //CONTROLS
    //********
    //Set the Controls scale  
    public static void ControlsScale_Set(float Controls_size) {  // used by the setting menu
        PlayerPrefs.SetFloat(CONTROLS_SCALE_KEY, Controls_size);
    }


    //Get the Controls size
    public static float ControlsScale_Get() {  // used by the setting menu
        return PlayerPrefs.GetFloat(CONTROLS_SCALE_KEY);
    }



    // Save the User Controls I/F size
    public static void ControlsSize_Set(float controls_size) {
        PlayerPrefs.SetFloat(CURRENT_CONTROLS_SIZE_KEY, controls_size);
    }


    //Get the User Controls I/F size
    public static float ControlsSize_Get() {
        return PlayerPrefs.GetFloat(CURRENT_CONTROLS_SIZE_KEY);
    }
    //CONTROLS -end


    public static void NukePrefs() {
        PlayerPrefs.DeleteAll();
    }



}//THE END