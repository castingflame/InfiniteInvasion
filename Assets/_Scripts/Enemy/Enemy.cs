using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;   //added to use RegEx
using UnityEngine;

public class Enemy : MonoBehaviour {


    #region DECLARATIONS
    /* -----< DECLARATIONS >----- */
    //INSPECTOR

    //SCRIPT
    //Number Cruncher
    public NumberCruncher nc;                   //Access to object (do a find in Start)
    //public int enemyTotal;  <<<DEPRECIATED?
    //Formations  <<<DEPRECIATED?
    HashSet<int> children2 = new HashSet<int>();   //Temp list for Formation objects
    Dictionary<int, bool> formationStatus = new Dictionary<int, bool>(); //List of reported 'Spawned' formations
    //public int fomationCount;

    //Invaders
    HashSet<int> children = new HashSet<int>();    //Temp list for Invader objects 

    //public int childCount;  <<<DEPRECIATED?

    //All Spawned
    public bool allSpawned = false;     //flag for all enemies spawned






    /* -----< DECLARATIONS -END >----- */

    #endregion DECLARATIONS



    #region START
    void Start() {
        //Find Objects
        nc = GameObject.FindObjectOfType<NumberCruncher>();

        //INITIALISE
        //Formantions
        Fomation_Count();


        }//Start () -end
    #endregion




    #region UPDATE
    // Update is called once per frame
    void Update() {

        //All Spawned?
        if (formationStatus.Count == children2.Count && allSpawned == false) {
            //Debug.LogWarning("ALL Formations Spwaned! (" + formationStatus.Count + ")" ); 
            nc.Enemy_Spawned(true); //Tell nc All Spawned
            allSpawned = true;  //set flag to stop checking the All Spawned state.
            }




        }//Update() -end
    #endregion




    #region FORMATION COUNT
    public void Fomation_Count() {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true)) {    //include inactive
            //Search for Invaders
            if (Regex.IsMatch(child.ToString(), "Formation") == true) { //Look for children                                                                     
                children2.Add(child.GetInstanceID());  //Add new child(s) to the HashSet
               }
            }//foreach - end
        }// Enemy_Count() -end

    #endregion





    #region ENEMY COUNT
    public void Enemy_Count() {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true)) {    //include inactive
            //Search for Invaders
            if (Regex.IsMatch(child.ToString(), "Invader") == true) { //Look for children 
                children.Add(child.GetInstanceID());  //Add new child(s) to the HashSet            
                }
            }//foreach - end
        }// Enemy_Count() -end

    #endregion




    #region FORMATIONS
    //Formation Controllers scripts report here when they have spawned all their childen
    //Dictionary - formationStatus hold a list of formations
    public void Formation_Spawned(int id, bool status) {
        formationStatus.Add(id, status);      
        }
    #endregion





    }//THE END
