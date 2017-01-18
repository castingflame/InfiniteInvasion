//Wrapper for the Unity PlayerPrefs Method
//****************************************


using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    //Key Pairs
    const string HIGHSCORE_KEY = "highscore";
    const string JOYSTICK_SCALE_KEY = "joystick_scale";
    const string BUTTON1_SCALE_KEY = "button1_scale";

    
    //Key Pairs -end



    //HIGH SCORES 
    //***********
    //Set the High Score
    public static void SetHighScore(float highscore) {

        PlayerPrefs.SetFloat(HIGHSCORE_KEY, highscore);

    }

    
    //Get the High Score
    public static float GetHighScore() {

        return PlayerPrefs.GetFloat (HIGHSCORE_KEY);

    }


    //Reset the High Score
    public static void ResetHighScore() {

        Debug.Log("PPM - Going to reset the High Score ");
        PlayerPrefs.SetFloat(HIGHSCORE_KEY, 0);

    }

    //HIGH SCORES -end





    //JOYSTICK
    //********
    //Set the joystick size
    public static void SetJoystickScale(float joystick_size) {

        PlayerPrefs.SetFloat(JOYSTICK_SCALE_KEY, joystick_size);

    }


    //Get the joystick size
    public static float GetJoystickScale() {

        return PlayerPrefs.GetFloat(JOYSTICK_SCALE_KEY);

    }

    //JOYSTICK -end




    //BUTTON1
    //********

    //BUTTON1 -end

}