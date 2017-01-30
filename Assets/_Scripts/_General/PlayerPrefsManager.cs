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





    //Controls
    //********
    //Set the Controls scale  
    public static void SetControlsScale(float Controls_size) {  // used by the setting menu

        PlayerPrefs.SetFloat(CONTROLS_SCALE_KEY, Controls_size);
    }


    //Get the Controls size
    public static float GetControlsScale() {  // used by the setting menu

        return PlayerPrefs.GetFloat(CONTROLS_SCALE_KEY);
    }



    // Save the User Controls I/F size
    public static void SetControlsSize(float controls_size) {

        PlayerPrefs.SetFloat(CURRENT_CONTROLS_SIZE_KEY, controls_size);
    }


    //Get the User Controls I/F size
    public static float GetControlsSize() {

        return PlayerPrefs.GetFloat(CURRENT_CONTROLS_SIZE_KEY);
    }


    //Controls -end


    public static void NukePrefs() {
        PlayerPrefs.DeleteAll();
    }



}//THE END