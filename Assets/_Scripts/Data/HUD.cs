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
   
    //Score
    public Text score_txt;
    public Text highScore_txt;
    public Text invasionLevel_txt;
    public Text levelComplete_txt;
    public Text invaderCount_txt;
    public Text level_txt;
   



    //SCRIPT
    //Number Cruncher
    public NumberCruncher nc;

    //Score
    private string score;
    private string highscore;

    //History values
    public float historyPlayerHealth;
    public float historyPlayerShield;



    /* -----< DECLARATIONS - END >----- */



    private void Start() {

        //INITIALISATION
        //Find Objects     
        nc = GameObject.FindObjectOfType<NumberCruncher>();
        levelComplete_txt.enabled = false;

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







    #region SHIELD
    // Display the shield value
    public void PlayerShield_Display(float shield, float max) {
        historyPlayerShield = shield;
        Debug.Log("hud: historyPlayerShield " + historyPlayerShield);
        UltimateStatusBar.UpdateStatus("PlayerShield", shield, max);        
    }//Shield_Display() -end
    #endregion SHIELD




    /* -----< HEALTH FUNCTIONALITY >----- */
    // Display the health value
    public void PlayerHealth_Display(float health, float max) {

        UltimateStatusBar.UpdateStatus("PlayerHealth", health, max);

    }//Health_Display() -end
     
 
    
    /* -----< HEALTH FUNCTIONALITY -END>----- */







    /* -----< INVASION LEVEL FUNCTIONALITY >----- */
    // Display the invasion level value
    public void InvasionLevel_Complete(bool level) {

        levelComplete_txt.enabled = level;   //Display the Level Complete message


    }//InvasionLevel_Display() -end
     /* -----< INVASION LEVEL FUNCTIONALITY -END>----- */




    /* -----< ENEMY FUNCTIONALITY >----- */
    // Display the health value
    public void EnemyCount_Display(int count) {
        //will only update invaderCount if its still in the scene
        if (invaderCount_txt) {     
            invaderCount_txt.text = count.ToString();
            }
        }//Health_Display() -end



    /* -----< ENEMY FUNCTIONALITY -END>----- */






    // Display the Level
    public void Level_Display(int level) {
        if (level_txt) {
            level_txt.text = level.ToString();
            }
        }//Level_Display() -end


  





}//THE END