using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {

    //DECLARATIONS
    public static string PreviousSceneName;         //Previous Scene Name for Navigation purposes (static)

    //DECLARATIONS -end





    //LOADLEVEL
    public void LoadLevel(string name){

        SaveSceneName();                    //Store the Scene name for Navigation purposes
        SceneManager.LoadScene(name);       //Goto the new scene
	}




    //QUIT REQUEST
	public void QuitRequest(){

		Application.Quit ();
	}





    //SAVE THE PREVIOUS SCENE NAME
    public void SaveSceneName() {

        PreviousSceneName = SceneManager.GetActiveScene().name;     //Save the name of the current scene to a static
        //Debug.Log("Last Scene was: " + PreviousSceneName);

    }


    //LOAD PREVIOUS SCENE
    public void LoadPreviousScene() {

        SceneManager.LoadScene(PreviousSceneName);       //Goto the new scene

    }





}
