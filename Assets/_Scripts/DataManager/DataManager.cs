/*******************************************************************************************************
Author: Paul Land
Creation Date: 25/01/2017
Game: Infinite Invasion
Unity Script: DataManager.cs
Location: /_Scripts/DataManager
Parent: DataManager prefab object used in the game levels
Description: Controls order of execution of scripts & methods that
            display, update and save the Score, Record Score,
            Levels, Shield, Health, ...
            
            DOES do - Asks other scripts to do stuff. This may involved passing data between scripts 
            Does NOT do - Work out stuff or make decisions.. apart from passing decision to pass values or not
            

Methods:    Score_AddTo(), Score_SubtractFrom(), Score_NewRecord(), 
            Shield_Initialise(), Shield_GetValue(), Shield_SetValue(), Shield_GetStatus(), Shield_SetStatus(),
            Health_Initialise(), Health_GetValue(), Health_SetValue(),
            InvasionLevel_GetValue(), InvasionLevel_SetValue(),
            PlayerHit(),


NOTES: Part of the DataManager object


PROJECT PLAN FOR DataManager
============================
Overview - Migrate all data (scoring, shield, health, etc) management functions from other 
scripts to here. Manage all data processing(NumberCruncher) and display(HUD) tasks from here in DM.

      Job List
      --------
DONE  1.Create Declarations | Inspector - with links to the other scripts
      required HUD.cs, ScoreKeeper.cs, PlayerPrefsManager.cs
DONE  2.Public inspector link on NumberCruncher.cs for PlayerPrefsManager_obj object is not needed as PPM is 'Static'
DONE  3.DM - Create some basic Methods that outline the functionality for DM
      4.DM - Gather initial data from ScoreKeeper.cs and pipe it to HUD to display it on the screen
      
      END - Remove all the score functionality in Player.cs and move it to DataManager.cs

*******************************************************************************************************/

using UnityEngine;

public class DataManager : MonoBehaviour {

    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    //Script Connections
    public HUD hud;                             // Acess to object
    public NumberCruncher numbercruncher;       // Acess to object
    public PlayerController playercontroller;   // Acess to object
    //NOTE:PlayerPrefsManager is not needed as is is a 'Static' class(Method)
    
   
                                                
    /* -----< DECLARATIONS - END >----- */





    /* -----< START FUNCTIONALITY >----- */
    //Start
    void Start() {

   

        //INITIALISATION -END

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





    

    // Get status of shield
    public bool Shield_GetStatus() {  //normally called by Player.cs

        bool nc_return = numbercruncher.Shield_GetStatus();

        if (nc_return == false) {
            //Tell HUD the shield is down
            }
       //else if logic for shield up?
        
        return numbercruncher.Shield_GetStatus();  //return value


    }//Shield_SetValue() -end



    

    /* -----< SHIELD FUNCTIONALITY -END >----- */





    // Get value of health
    public void Health_GetValue(float health) {

        //health value get request
        //1.Tell ScoreKeeper and get a return 
        //2.Pipe that return to HUD

    }//Health_GetValue() -end





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




    /* -----< PLAYER FUNCTIONALITY >----- */
    // Playey hit!
    public void Player_Hit(float hit) {


        //1.Tell NU and .....
        numbercruncher.Player_Hit(hit);                         //Tell NumberCruncher


        //...


    }//Player_Hit() -end
    /* -----< PLAYER FUNCTIONALITY -END >----- */









}//THE END
