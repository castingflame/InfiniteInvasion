﻿using UnityEngine;

public class HiScoreReset : MonoBehaviour {

    //Declarations
    //Inspector
    public bool hasReset = false;       //Flag to say script has reset.

    //Declarations -end




    public void HiReset() {    //public function become available in inspector for button on-clik event
      
        PlayerPrefsManager.ResetHighScore();        //Gets PPM to reset the High Score
        //Debug.LogError("HiScoreReset - HiReset");

        hasReset = true;
        }


    }

