using UnityEngine;

public class ResizeControls : MonoBehaviour {



    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    //Joystick
    public UltimateJoystick myJoystick;
    //Buton
    public UltimateButton myFire;
    
    /* -----< DECLARATIONS -END >----- */




    public void Resize(float scale) {
      
       
        myFire.buttonSize = scale;
        myJoystick.joystickSize = scale;
        myFire.UpdatePositioning();         // Ask UB to update our changes
        myJoystick.UpdatePositioning();     //ask  UJ to update our changes

    }


}
