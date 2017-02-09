using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthStatusBar : MonoBehaviour {
    
    /* -----< DECLARATIONS >----- */

    //SCRIPT
    //Number Cruncher
    public NumberCruncher nc;




    void Start() {


        // Get the value of the Player Shield from Number Cruncher
        nc = GameObject.FindObjectOfType<NumberCruncher>();
        float health = nc.playerHealth;
        float max = nc.playerHealthMax;
        UltimateStatusBar.UpdateStatus("PlayerHealth", health, max);


        }
    }
