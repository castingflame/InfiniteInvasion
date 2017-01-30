using UnityEngine;
using UnityEngine.UI;

public class DevSettings : MonoBehaviour {

    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    //Score
    public Text highScore_txt;
    public Text NukePPKeys_txt;


    //LOCAL 
    private string score;
    private string highscore;

    /* -----< DECLARATIONS - END >----- */


    private void Start()
    {
        highScore_txt.text = PlayerPrefsManager.HighScore_Get().ToString();

    }



    public void HighScore_Reset()
    {
        PlayerPrefsManager.HighScore_Set(0);  // Set the High Score to 0 in the PPM
        highScore_txt.text = PlayerPrefsManager.HighScore_Get().ToString(); // Get it from PPM again update the screen
    }




    public void PPM_WipeKeys()
    {
        PlayerPrefsManager.NukePrefs();         //Wipes the entire Player Prefs of all Keys and Values
        NukePPKeys_txt.text = "All Keys Erased!";
        highScore_txt.text = "0";
    }





}
