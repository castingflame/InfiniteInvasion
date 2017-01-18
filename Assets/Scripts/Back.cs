using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour {

    

    void Start() {

        int indexOfSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;      //Get the current Scene index (add 1 because they start from 0)
        Debug.Log("Loading level at id: " + indexOfSceneToLoad + "!");
        //SceneManager.LoadScene(indexOfSceneToLoad);



       
    }


}