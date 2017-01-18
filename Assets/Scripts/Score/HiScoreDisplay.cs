using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HiScoreDisplay : MonoBehaviour {

    //Declarations
    //Inspector
    public GameObject resetScript_obj;      //ResetScript_obj

    //Declarations -end




    void Start() {

        Text highscoreField = GetComponent<Text>();

        //Display the High Score for the first time
        highscoreField.text = PlayerPrefsManager.GetHighScore().ToString();


        }


    void Update() {  //Looks at the reset flag on the resetScript_obj

        Text highscoreField = GetComponent<Text>();

        //Get the HighScoreReset.sc from the resetScript_obj
        HiScoreReset script = resetScript_obj.GetComponent<HiScoreReset>();


        if (script.hasReset == false) {
            //Debug.Log("HiScoreDisplay - hasRest flag = false" );
            }

        else if (script.hasReset == true) {
            //Debug.Log("HiScoreDisplay - hasRest flag = true" );

            highscoreField.text = PlayerPrefsManager.GetHighScore().ToString();

            }

       


        }

    }//THE END


