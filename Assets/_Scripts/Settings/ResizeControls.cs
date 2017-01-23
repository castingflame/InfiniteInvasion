using UnityEngine;

public class ResizeControls : MonoBehaviour {



    /* -----< DECLARATIONS >----- */

    //INSPECTOR
    //Joystick
    public UltimateJoystick myJoystick;
    //Buton
    public UltimateButton myFire;


    /* -----< DECLARATIONS -END >----- */




    public void Resize() {
      
       
        myFire.UpdateHighlightColor(Color.red);

        // 

        myFire.buttonSize = 2;
        myFire.UpdatePositioning();         // Get the UB to update our changes

    }


}
