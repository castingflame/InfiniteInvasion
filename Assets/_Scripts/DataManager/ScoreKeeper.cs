/***************************************************************************
Author: Paul Land
Creation Date:
Game: Infinite Invasion
Unity Script: ScoreKeeper.cs
Location: /_Scripts/DataManager
Parent: DataManager | ScoreKeeper prefab object used in the game levels
Description: Controls maths functions of scoring system. Updates 
             PlayerPrefsManager to manage data in persistant storage.
             
Methods:    Score_AddTo(), Score_SubtractFrom(), Score_NewRecord(), 
            Shield_GetValue(), Shield_SetValue(),
            Health_GetValue(), Health_SetValue(),
            InvasionLevel_GetValue(), InvasionLevel_SetValue(),

NOTES: Part of the DataManager object


(new) PROJECT PLAN FOR ScoreKeeper
===================================
Overview - Migrate all scoring maths functions from other scripts to here.

      Job List
      --------
DONE     Write default methods for ...
         Score_AddTo(), Score_SubtractFrom(), Score_NewRecord(), 
         Shield_GetValue(), Shield_SetValue(),
         Health_GetValue(), Health_SetValue(),
         InvasionLevel_GetValue(), InvasionLevel_SetValue(),
      
      2. Creat Score_AddTo() functionality
      3.
      4.

*****************************************************************************/


using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    
    public static int newHighScore = 0;         //DEPRECIATED - Remove?
    public static float currentHighScore;       //DEPRECIATED - Remove?

    private Text myText;


    /* -----< DECLARATIONS >----- */
    //INSPECTOR



    //SCRIPT
    //Score
    public static int score = 0;                //Static because ????????????????????




    //Record Score
    //Health
    //Shield
    //Invasion Level

    /* -----< DECLARATIONS - END >----- */






    /* -----< START FUNCTIONALITY >----- */

    void Start() {
        currentHighScore = PlayerPrefsManager.GetHighScore();           //Get the current High Score from the PPM
        ResetScore();
    }//Start() -end

    /* -----< START FUNCTIONALITY -END >----- */




    //Score
    public void Score(int points) {

        // score += points;                                        //Adds points to current score
        // myText.text = score.ToString();                         //Displays the score to the screen

        if (score > currentHighScore) {                         //New High Score?
            newHighScore = score;                               //Update High Score
            PlayerPrefsManager.SetHighScore(newHighScore);      
            // Debug.Log("New HiScore =" + newHighScore);
        } 
    }//Score() -end

    
    
    public static float GetScore(){
        //work out the score .....
        return score;
    }//GetScore() -end



    public static float SetScore() {
        //work out the score .....
        return score;
    }//SetScore() -end




    //Resets the Score
    public static void ResetScore() {  
        score = 0;                          //Resets the score variable     
    }//ResetScore() -end




    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    //++++  N E W  F U N C T I O N A L I T Y   B E L O W   H E R E   +++++




    /* -----< SCORE FUNCTIONALITY >----- */

    // Add to score
    public float Score_AddTo(float newscore) {
        //A new score has occured
        //1.Compute score
        //2.Return it to DM

        return newscore; 
    }//Score_AddTo() -end


    // Subtract from score
    public float Score_SubtractFrom(float newscore) {

        //A new score has occured
        //1.Compute score
        //2.Return it to DM

        return newscore; 
    }//Score_SubtractFrom() -end


    // New record score
    public float Score_NewRecord(float recordscore) {

        //A new record score has occured
        //1.Compute score
        //2.Return it to DM

        return recordscore; 
    }//Score_NewRecord() -end

    /* -----< SCORE FUNCTIONALITY -END >----- */









    /* -----< SHIELD FUNCTIONALITY >----- */

    // Get the shield value
    public float Shield_GetValue(float shieldvalue) {
        //A sheild value requested
        //1.get value
        //2.Return it to DM

        return shieldvalue;
    }//Shield_GetValue() -end


    // Set the shield value
    public float Shield_SetValue(float shieldvalue) {
        //A Set the sheild value requested
        //1.set value
        //2.Return it to DM

        return shieldvalue;
    }//Shield_SetValue() -end

    /* -----< SHIELD FUNCTIONALITY -END >----- */





    /* -----< HEALTH FUNCTIONALITY >----- */
   
    // Get the health value
    public float Health_GetValue(float healthvalue) {
        //A health value requested
        //1.get value
        //2.Return it to DM

        return healthvalue;
    }//Health_GetValue() -end


    // Set the shield value
    public float Health_SetValue(float healthvalue) {
        //A Set the health value requested
        //1.set value
        //2.Return it to DM

        return healthvalue;
    }//Health_SetValue() -end

    /* -----< HEALTH FUNCTIONALITY -END >----- */






    /* -----< INVASION LEVEL FUNCTIONALITY >----- */

    // Get the InvasionLevel value
    public float InvasionLevel_GetValue(float invasionlevelvalue) {
        //A invasion level value requested
        //1.get value
        //2.Return it to DM

        return invasionlevelvalue;
    }//InvasionLevel_GetValue() -end


    // Set the InvasionLevel value
    public float InvasionLevel_SetValue(float invasionlevelvalue) {
        //A Set the invasion level value requested
        //1.set value
        //2.Return it to DM

        return invasionlevelvalue;

    }//InvasionLevel_SetValue() -end


    /* -----< INVASION LEVEL FUNCTIONALITY -END >----- */


    






}//THE END
