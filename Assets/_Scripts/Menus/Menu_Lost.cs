/***************************************************************************
Author: Paul Land
Creation Date: 27/01/2017
Game: Infinite Invasion
Unity Script: Menu_Lost.cs
Location: /_Scripts/Menus
Parent: Lost scene | LostCanvas 
Description: Controls various displaying info for the 'Lost' scene
               
NOTES: Part of the Lost scene

*****************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class Menu_Lost : MonoBehaviour {

    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    public NumberCruncher nc;
    public Text score;
    public Text highscore;


    private GameObject cruncher;
    //Local
    //Script Connections
    //Script Connect
   

    /* -----< DECLARATIONS -END >----- */




    // Use this for initialization
    void Start () {      

        nc = FindObjectOfType<NumberCruncher>();        //Find the running NumberCruncher

        if (GameObject.Find("NumberCruncher") != null)  // If NumberCruncher DOES exist
        {
            nc = FindObjectOfType<NumberCruncher>();

            highscore.text = nc.highScore.ToString();
            score.text = nc.score.ToString();

            Destroy(nc.gameObject);     // Finished getting data from Number Cruncher. 
                                        // Destroy it otherwise there will be another 
                                        // when the game level starts.
        }

        else if (GameObject.Find("NumberCruncher") == null) // If NumberCruncher does NOT exists
        {
            Debug.LogError("Could not find a NumberCruncher to open");
        }

    }//Start() -end




}//THE END



