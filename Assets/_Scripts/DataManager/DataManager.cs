/*******************************************************************************************************
Author: Paul Land
Creation Date: 25/01/2017
Game: Infinite Invasion
Unity Script: DataManager.cs
Location: /_Scripts/DataManager
Parent: DataManager prefab object used in the game levels
Description: Controls order of execution of scripts & methods for
            displaying, updating and saving the Score, Record Score,
            Levels, Shield, Health, ...

Methods:    Score_AddTo(), Score_SubtractFrom(), Score_NewRecord(), 
            Shield_GetValue(), Shield_SetValue(),
            Health_GetValue(), Health_SetValue(),
            InvasionLevel_GetValue(), InvasionLevel_SetValue(),


NOTES: Part of the DataManager object


PROJECT PLAN FOR DataManager
============================
Overview - Migrate all data (scoring, shield, health, etc) management functions from other 
scripts to here. Manage all data processing(ScoreKeeper) and display(HUD) tasks from here in DM.

      Job List
      --------
DONE  1.Create Declarations | Inspector - with links to the other scripts
      required HUD.cs, ScoreKeeper.cs, PlayerPrefsManager.cs
DONE  2.Public inspector link on Scorekeeper.cs for PlayerPrefsManager_obj object is not needed as PPM is 'Static'
      3.DM - Create some basic Methods that outline the functionality for DM
      4.DM - Gather initial data from ScoreKeeper.cs and pipe it to HUD to display it on the screen
      5.Remove all the score functionality in Player.cs and move it to DataManager.cs

/*******************************************************************************************************/

using UnityEngine;

public class DataManager : MonoBehaviour {

    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    public HUD hud;
    public ScoreKeeper scorekeeper;
    //PlayerPrefsManager is not needed as its 'Static'



    //SCRIPT
    /* -----< DECLARATIONS - END >----- */



    /* -----< START FUNCTIONALITY >----- */
    //Start
    void Start() {

    }//Start() -end
    /* -----< START FUNCTIONALITY -END >----- */




    /* -----< SCORE FUNCTIONALITY >----- */
    // Add to score
    public void Score_AddTo(float score) {

        //A new score has occured
        //1.Tell ScoreKeeper  
        //2.Tell HUD new value

    }//Score_AddTo() -end


    // Subtract from score
    public void Score_SubtractFrom(float score) {

        //A new score has occured
        //1.Tell ScoreKeeper 
        //2.Tell HUD new value

    }//Score_SubtractFrom() -end




    // New record score
    public void Score_NewRecord(float Recordscore) {

        //A new record score has occured
        //1.Tell ScoreKeeper 
        //2.Tell HUD new value

    }//Score_NewRecord() -end
    /* -----< SCORE FUNCTIONALITY -END >----- */






    /* -----< SHIELD FUNCTIONALITY >----- */
    // Get value of shield
    public void Shield_GetValue(float shield) {

        //Shield value get request
        //1.Tell ScoreKeeper and get a return 
        //2.Pipe that return to HUD

    }//Shield_GetValue() -end




    // Set value of shield
    public void Shield_SetValue(float shield) {

        //Shield value set request
        //1.Tell ScoreKeeper 
        //2.Tell HUD new value

    }//Shield_SetValue() -end
    /* -----< SHIELD FUNCTIONALITY -END >----- */





    /* -----< HEALTH FUNCTIONALITY >----- */
    // Get value of health
    public void Health_GetValue(float health) {

        //health value get request
        //1.Tell ScoreKeeper and get a return 
        //2.Pipe that return to HUD

    }//Health_GetValue() -end




    // Set value of health
    public void Health_SetValue(float health) {

        //health value set request
        //1.Tell ScoreKeeper 
        //2.Tell HUD new value

    }//Health_SetValue() -end
    /* -----< HEALTH FUNCTIONALITY -END >----- */






    /* -----< INVASION LEVEL FUNCTIONALITY >----- */
    // Get value of invasionlevel
    public void InvasionLevel_GetValue(float invasionlevel) {

        //invasionlevel value get request
        //1.Tell ScoreKeeper and get a return 
        //2.Pipe that return to HUD

    }//InvasionLevel_GetValue() -end




    // Set value of invasionlevel
    public void InvasionLevel_SetValue(float invasionlevel) {

        //invasionlevel value set request
        //1.Tell ScoreKeeper 
        //2.Tell HUD new value

    }//InvasionLevel_SetValue() -end
    /* -----< INVASION LEVEL FUNCTIONALITY -END >----- */












    /*
        public static float SetScore() {
            //work out the score .....
            return score;
        }//SetScore() -end

    */








}//THE END
