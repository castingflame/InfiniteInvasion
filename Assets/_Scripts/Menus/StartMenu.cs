﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // Ask PPM to setup Persistant Storage
        //Debug.Log("StartMenu: Initialising PPM");
        PlayerPrefsManager.Initialise();
        
    }
	

}
