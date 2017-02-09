using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldStatusBar : MonoBehaviour {


    /* -----< DECLARATIONS >----- */
  
    //SCRIPT
    //Number Cruncher
    public NumberCruncher nc;



    
    void Start () {
        

        // Get the value of the Player Shield from Number Cruncher
        nc = GameObject.FindObjectOfType<NumberCruncher>();
        float shield = nc.playerShield;
        float max = nc.playerShieldMax;
        UltimateStatusBar.UpdateStatus("PlayerShield", shield, max);


        }

   


    }//THE END
