/***************************************************************************
Author: Paul Land
Creation Date: 25/01/2017
Game: Infinite Invasion
Unity Script: NumberCruncher.cs
Location: /_Scripts/DataManager
Parent: DataManager | ScoreKeeper prefab object used in the game levels
Description: Controls maths functions of scoring system. Updates 
             PlayerPrefsManager to manage data in persistant storage.
             
Methods:    PUBLIC
            -------
            Score_Get(), Score_Add(), Score_Subtract(), Score_Display(), 
            HighScore_Get(), HighScore_Set(), HighScore_Display(), 
            Shield_GetValue(), Shield_SetValue(), Shield_GetStatus(), Shield_SetStatus(),
            Health_GetValue(), Health_SetValue(),
            InvasionLevel_GetValue(), InvasionLevel_SetValue(),
            
            PRIVATE
            -------
            Score(), 


NOTES: Part of the DataManager object


PROJECT PLAN FOR NumberCruncher
===================================
Overview - Migrate all scoring maths functions from other scripts to here.

      Job List
      --------
DONE  1. Write default methods for ...

      
      2. Creat Score_AddTo() functionality
      3.
      4.

*****************************************************************************/


using UnityEngine;
using UnityEngine.UI;

public class NumberCruncher : MonoBehaviour {



    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    //HUD
    public HUD hud;                             // Acess to object HUD
    //Player Health                                         
    public float playerHealth;                  // Default Player Health
    //Player Shield
    public float playerShield;                  // Default Player Shield
    //ENEMIES


    //SCRIPT
    //Player Health                                         
    public float playerHealthMax;               // Save Health Starting value for USB
    //Player Shield
    public float playerShieldMax;               // Save Shield Starting value for USB
    public bool shieldStatus;                   // Flag for Shield Up/Down
    //Score system
    public float score;                         //Current score
    public float highScore;                     //Current highscore
    public float newHighScore;                  // 


    //SCRIPT
    public GameObject[] roster;                 //Array to hold a list of the found enemies with 'enemy' tag
    


    /* -----< DECLARATIONS -END >----- */




    /* -----< AWAKE FUNCTIONALITY >----- */

    void Awake() {

        // Dont destroy the NumberCruncher when a new scene loads
        GameObject.DontDestroyOnLoad(gameObject);
       
        //INITIALISATION
        //SHIELD
        //Set the shield status to UP
        shieldStatus = true;
        //Set some initial default values for stuff
        playerShield = 1000;   //Tweekable in script
        playerHealth = 3000;   //Tweekable in script
        //Save initial values for the UltimateStatusBar(s) Max value
        playerShieldMax = playerShield;
        playerHealthMax = playerHealth;

        //SCORE
        Score_Reset();
        //HighScore_Set(10);  // Use this to reset the high score at design time

        highScore = HighScore_Get();        // Get the high score
        HighScore_Display(highScore);       // Update the On-Screen score


        //INITIALISATION -END

       

        


    }//Awake() -end
     /* -----< AWAKE FUNCTIONALITY -END >----- */




    /* -----< UPDATE FUNCTIONALITY >----- */
    private void Update() {
        Score_Display();   //Note: High Score 'display' is in scores logic
                           /* -----< UPDATE FUNCTIONALITY -END >----- */

        //Keep a list of enemy count
        
        EnemyRosta_Get();
    }





    /* -----< SHIELD FUNCTIONALITY >----- */

    // Get the shield value
    public float PlayerShield_GetValue() {

        return playerShield;    //Return value of the Player Shield

    }//PlayerShield_GetValue() -end




    // Set the shield value
    public void PlayerShield_SetValue(float shield) {
        //A Set the sheild value requested
        //1.set value
        //2.Return it to DM

    }//PlayerShield_SetValue() -end




    // Get status of shield
    public bool Shield_GetStatus() {

        //work out the shield status
        bool shieldstatus = true;    //TEMP SET SOMETHING TO TEST THE LOGIC
      
        return shieldstatus;  // return the shield status

    }//Shield_GetStatus() -end
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
    public void Health_SetValue(float health) {
        //A Set the health value requested
        //1.set value
        //2.Return it to DM

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
    public void InvasionLevel_SetValue(float invasionlevelvalue) {
        //A Set the invasion level value requested
        //1.set value
        //2.Return it to DM
        
    }//InvasionLevel_SetValue() -end


    /* -----< INVASION LEVEL FUNCTIONALITY -END >----- */




    /* -----< PLAYER FUNCTIONALITY >----- */
    // Playey hit!
    public void Player_Hit(float hit) {

        if (shieldStatus == true) {     // Shield is UP?

            playerShield -= hit;        //subtract hit value from shield
            // Debug.Log("NC:Shield Status: " + shieldStatus + " value:" + playerShield);
            hud.PlayerShield_Display(playerShield, playerShieldMax);
            if (playerShield <= 0) {    //Shield Down?
                shieldStatus = false;
            }
        }

        if (shieldStatus == false) {    // Shield is DOWN?

            playerHealth -= hit;        //subtract hit value from health
            // Debug.Log("NC:Health value:" + playerHealth);
            hud.PlayerHealth_Display(playerHealth, playerHealthMax);

            if (playerHealth <= 0) {        //Player Dead?
                Destroy(GameObject.Find("Player")); //TODO: Needs massive explosion!
                LevelManager levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                levMan.LoadLevel("Lose");      
            }
        }

    }//Player_Hit() -end
     /* -----< PLAYER FUNCTIONALITY -END >----- */




    /* -----< SCORE FUNCTIONALITY >----- */

    public void Score_Display() {
        hud.Score_Display(score);
    }

    

    //Resets the Score
    public void Score_Reset() {
        score = 0;                          //Resets the score variable     
    }//Score_Reset() -end

    
    
    // Get the score
    public float Score_Get() {
       return score;             //Return the score.
    }


    // Add to score
    public void Score_Add(float newscore) {
        //A new score has occured

        score += newscore;                  //Compute score
        if (score >= highScore) {           //New High Score?
            highScore = score;              //Update the High Score value 
            HighScore_Display(highScore);   //Display the New High Score
            
        }

        hud.Score_Display(score);       //Sent it to HUD





    }//Score_Add() -end


    // Subtract from score
    public float Score_SubtractFrom(float newscore) {

        //A new score has occured
        //1.Compute score
        //2.Return it to DM

        return newscore;
    }//Score_SubtractFrom() -end


    /* -----< SCORE FUNCTIONALITY -END >----- */







    /* -----< HIGH SCORE FUNCTIONALITY >----- */
    // Get the high score
    public float HighScore_Get() {
        float hs = PlayerPrefsManager.HighScore_Get();    //Get the current high score from the PPM     
        // Debug.Log("hs=" + hs);
        return hs;                                 //Return the score.
       
    }



    public void HighScore_Set(float newhighscore) {
        PlayerPrefsManager.HighScore_Set(newhighscore);        //Write the high score to the PPM

    }





    /* -----< ROSTER FUNCTIONALITY >----- */
    public void EnemyRosta_Set()
    {
           

    }

    


    public void EnemyRosta_Get()
    {       
        roster = GameObject.FindGameObjectsWithTag("Enemy");
        int i = 0;                                  //counter in increment        

            foreach (Object Enemy in roster)
            {          //Loop through objects to get a count of how many
                i++;                                    //Increment loop
                Debug.Log("EMEMIES = " + (i));
                if (i == 0)
                {
                    Debug.Log("NO EMEMIES LEFT IN SCENE");    
                }
        } 
    }
    /* -----< ROSTER FUNCTIONALITY -END >----- */






    public void HighScore_Display(float newhighscore) {
        hud.HighScore_Display(newhighscore);
        //Debug.Log("New High Score" + newhighscore);
    }
    /* -----< HIGH SCORE FUNCTIONALITY -END >----- */






    /* -----< ON DESTROY FUNCTIONALITY -END >----- */
    // NOTE: After the lose scene has it's data it must do a 
    // Destroy(nc.gameObject) imediately otherwise the highscore
    // will not be updated via the section below.

    void OnDestroy() {
        //High Score      
        //Debug.Log("highScore:" + highScore);
        //Debug.Log("HighScore_Get():" + HighScore_Get());
        if (highScore > HighScore_Get()) {   //If our high score is > than in PPM
            
            HighScore_Set(highScore);        //Write the high score to PPM
            //Debug.Log("HighScore_Set then Get:" + HighScore_Get());
        }
        /* -----< ON DESTROY FUNCTIONALITY -END >----- */


    }


    

}//THE END
