using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;                //make this a static
    public static int newHighScore = 0;
    public static float currentHighScore;       

    private Text myText;
    

    void Start() {

        currentHighScore = PlayerPrefsManager.GetHighScore();           //Get the current High Score from the PPM
        //Debug.Log("START: currentHighScore =" + currentHighScore);
        myText = GetComponent<Text>();
        myText.text = "0";                                              //Reset the On Screen Score
        ResetScore();
    }


    //Score
    public void Score(int points) {   
        score += points;                                        //Adds points to current score
        myText.text = score.ToString();                         //Displays the score to the screen

        if (score > currentHighScore) {                         //New High Score?
            newHighScore = score;                               //Update High Score


            //TODO: NOTE!!! this is probably really slowing the game down
            //updating the Prefs over and over like this as its slow storage.
            PlayerPrefsManager.SetHighScore(newHighScore);      
           
            // Debug.Log("New HiScore =" + newHighScore);
        } 

    }

    
    
    //Resets the Score
    public static void ResetScore() {  
        score = 0;                          //Resets the score variable     
    }


}
