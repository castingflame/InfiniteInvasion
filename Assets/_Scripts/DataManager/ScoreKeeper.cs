using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;                //make this a static
    public static int newHighScore = 0;
    public static float currentHighScore;       

    private Text myText;
    

    void Start() {

        currentHighScore = PlayerPrefsManager.GetHighScore();           //Get the current High Score from the PPM
        ResetScore();
    }


    //Score
    public void Score(int points) {

        // score += points;                                        //Adds points to current score
        // myText.text = score.ToString();                         //Displays the score to the screen

        if (score > currentHighScore) {                         //New High Score?
            newHighScore = score;                               //Update High Score



            PlayerPrefsManager.SetHighScore(newHighScore);      
           
            // Debug.Log("New HiScore =" + newHighScore);
        } 

    }

    
    
    //Resets the Score
    public static void ResetScore() {  
        score = 0;                          //Resets the score variable     
    }


}
