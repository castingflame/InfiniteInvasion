/*******************************************************************************************************
Author: Paul Land
Creation Date: 25/01/2017
Game: Infinite Invasion
Unity Script: HUD.cs
Location: /_Scripts/DataManager
Parent: DataManager prefab object used in the game levels
Description: Responsible for displaying all 
             Heads Up Display data - Score, Level, Healh, Shield, ...

Methods:    Score_Display(),
            HighScore_Display(). 
            Shield_Display(),
            Health_Display(),
            InvasionLevel_Display()


NOTES: Part of the DataManager object


PROJECT PLAN FOR HUD
====================
Overview - 
      
        Job List
        --------
DONE    1. Create some basic Methods that outline the functionality for HUD
        2. Create functionality for ...
        Score_Display(),
        HighScore_Display(). 
        Shield_Display(),
        Health_Display(),
        InvasionLevel_Display()


*******************************************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {



    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    //Script Connections
    public NumberCruncher nc;

    //Score
    public Text score_txt;
    public Text highScore_txt;
    


    //LOCAL 
    private string score;
    private string highscore;

    /* -----< DECLARATIONS - END >----- */



    private void Start() {

        //INITIALISATION

        //score_txt.text = nc.Score_Get().ToString();  //Get the score and display it
        //highScore_txt.text = nc.Score_Get().ToString();  //Get the high score and display it

    }






    /* -----< SCORE FUNCTIONALITY >----- */
    // Display the score
    public void Score_Display(float score) {

        score_txt.text = score.ToString();  //Display the score

    }//Score_Display() -end
     /* -----< SCORE FUNCTIONALITY -END>----- */




    /* -----< HIGH SCORE FUNCTIONALITY >----- */
    // Display the high score
    public void HighScore_Display(float highscore) {
   
        highScore_txt.text = highscore.ToString();  //Display the high score

        //Debug.Log("HUD: HighScore_Display():" + highscore);


    }//HighScore_Display() -end
     /* -----< HIGH SCORE FUNCTIONALITY -END>----- */







    /* -----< SHIELD FUNCTIONALITY >----- */
    // Display the shield value
    public void PlayerShield_Display(float shield, float max) {

       UltimateStatusBar.UpdateStatus("PlayerShield", shield, max); 

    }//Shield_Display() -end
     /* -----< SHIELD FUNCTIONALITY -END>----- */







    /* -----< HEALTH FUNCTIONALITY >----- */
    // Display the health value
    public void PlayerHealth_Display(float health, float max) {

        UltimateStatusBar.UpdateStatus("PlayerHealth", health, max);

    }//Health_Display() -end
     
    
    
    
    
    
    /* -----< HEALTH FUNCTIONALITY -END>----- */







    /* -----< INVASION LEVEL FUNCTIONALITY >----- */
    // Display the invasion level value
    public void InvasionLevel_Display(float invasionlevel) {

        //1.Display the invasion level value

    }//InvasionLevel_Display() -end
     /* -----< INVASION LEVEL FUNCTIONALITY -END>----- */





}//THE END
