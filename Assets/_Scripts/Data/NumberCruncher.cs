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
using System.Collections.Generic;   //this is for 'lists' type of array

public class NumberCruncher : MonoBehaviour {


    #region DECLARATIONS
    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    //Player Health                                         
    public float playerHealth;                  // Default Player Health
    //Player Shield
    public float playerShield;                  // Default Player Shiel
    //Player Health                                         
    public float playerHealthMax;               // Save Health Starting value for USB
    //Player Shield
    public float playerShieldMax;               // Save Shield Starting value for USB
    public bool shieldStatus;                   // Flag for Shield Up/Down
    //Score system
    public float score;                         //Current score
    public float highScore;                     //Current highscore
    public float newHighScore;                  


    //SCRIPT
    //HUD
    public HUD hud;                             //Access to object (do a find in Start)
    //Level Manager
    public LevelManager levelManager;           //Access to object (do a find in Start)
    //ENEMY
    public Enemy enemy;
    //Roster 
    List<int> enemyRoster = new List<int>();    //List to hold enemy states when they are instantiated
    //All SpawnedFlag
    public bool enemySpawned = false;
    //Level Complete
    public float levelCompletePause = 5f;

    /* -----< DECLARATIONS -END >----- */
    #endregion DECLARATIONS





    #region AWAKE
    /* -----< AWAKE FUNCTIONALITY >----- */
    void Awake() {

        // Dont destroy the NumberCruncher when a new scene loads
        GameObject.DontDestroyOnLoad(gameObject);


        //Am I connected to Level Manager? If not get a connection.
        if (!levelManager) { levelManager = GameObject.FindObjectOfType<LevelManager>(); }
        //Am I connected to hud? If not get a connection.
        if (!hud) { hud = GameObject.FindObjectOfType<HUD>(); }



        }//Awake() -end
         /* -----< AWAKE FUNCTIONALITY -END >----- */
    #endregion AWAKE



    private void Start()
    {
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
        highScore = HighScore_Get();    // Get the high score

        //Enemies
        

    //INITIALISATION -END
    }



    #region UPDATE
    /* -----< UPDATE FUNCTIONALITY >----- */
    private void Update() {


        //Find Objects
        //Am I connected to Level Manager? If not get a connection.
        if (!levelManager) { levelManager = GameObject.FindObjectOfType<LevelManager>(); }
        //Am I connected to hud? If not get a connection.
        if (!hud) { hud = GameObject.FindObjectOfType<HUD>(); }

     
        HighScore_Display(highScore);       // Update the On-Screen score
        Score_Display();                    //!!!!!!! Note: High Score 'display' is in scores logic



        if (enemySpawned == true && enemyRoster.Count == 0) {

            //Clean-up before new Level 
            hud.InvasionLevel_Complete(true);
            //enemyRoster.Clear();    //Clear the list
            //hud.EnemyCount_Display(enemyRoster.Count); //update the hud
            enemySpawned = false;   //Reset the All Spawned flag
            Invoke("NextLevel", levelCompletePause);
            
            }



        }
    /* -----< UPDATE FUNCTIONALITY -END >----- */ 
    #endregion UPDATE






    #region SHIELD

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
    #endregion SHIELD





    #region HEATH
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
    #endregion HEALTH






    #region INVASION LEVEL
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
    #endregion INVASION






    #region PLAYER
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
    #endregion PLAYER






    #region SCORE
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
    #endregion SCORE






    #region HIGH SCORE 
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




    public void HighScore_Display(float newhighscore) {
        hud.HighScore_Display(newhighscore);
        //Debug.Log("New High Score" + newhighscore);
    }
    /* -----< HIGH SCORE FUNCTIONALITY -END >----- */
    #endregion HIGH SCORE






    #region ENEMY
    /* -----< enemyRoster FUNCTIONALITY >----- */

    public void EnemyRoster_Add(int enemy) {

        enemyRoster.Add(enemy);  //Adds an enemy (InstanceID) to the list
        hud.EnemyCount_Display(enemyRoster.Count); //update the hud
        }
             

    public void EnemyRosta_Remove(int enemy) {

        enemyRoster.Sort();     //A sort must be done before a Binary Search
        int enemyindex = enemyRoster.BinarySearch(enemy);
     
        enemyRoster.RemoveAt(enemyindex);   //Removes the enemy from the Roster
        hud.EnemyCount_Display(enemyRoster.Count); //update the hud

        }//EnemyRosta_Get -end



    /* -----< enemyRoster FUNCTIONALITY -END >----- */


    //Enemy Spawned? Used by Enemy.cs to set a ready status to fight.
    public void Enemy_Spawned(bool status) {
        enemySpawned = status;
        }

        #endregion ENEMY



    private void NextLevel(){
         levelManager.LoadNextScene();
        }







    #region ON DESTROY
    /* -----< ON DESTROY FUNCTIONALITY -END >----- */
    // NOTE: After the lose scene has it's data it must do a 
    // Destroy(nc.gameObject) imediately otherwise the highscore
    // will not be updated via the section below.

    void OnDestroy() {
        //High Score      
        if (highScore > HighScore_Get()) {   //If our high score is > than in PPM         
            HighScore_Set(highScore);        //Write the high score to PPM
        }
        /* -----< ON DESTROY FUNCTIONALITY -END >----- */
    }
    #endregion ON DESTROY


}//THE END
