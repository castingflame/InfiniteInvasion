using UnityEngine;
using UnityEngine.UI;



public class Settings : MonoBehaviour {


    //DECLARATIONS

    public ResizeControls resizecontrols;


    private Slider ControlsSlider;                      //Var for On-screen Controls size slider
    private Text ControlsSizeField;                     //Var for On-screen Controls Size field
   // private ETCControls inputControls;                //Var for ETC Controls


    private string currentControlsScale_string;         //Var to hold Controls scale before it gets written to PPM
    private int currentControlsScale_int;               //Var to hold parsed currentControlsScale string to int

    //private float currentControlsSize;                  //Var to hold Controls size before it get written to PPM
    public const float CONTROLS_SCALE_1 = 1f;
    public const float CONTROLS_SCALE_2 = 1.2f;
    public const float CONTROLS_SCALE_3 = 1.4f;
    public const float CONTROLS_SCALE_4 = 1.6f;
    public const float CONTROLS_SCALE_5 = 1.8f;

    //DECLARATIONS -end;



 


    private void Start() {


        //GET OBJECTS - Dynamically find objects at runtime
        ControlsSlider = GameObject.Find("Controls_sldr").GetComponent<Slider>();       //Controls size - slider
        ControlsSizeField = GameObject.Find("ControlsScale").GetComponent<Text>();      //Controls size - filed
        //inputControls = GameObject.Find("Controls").GetComponent<ETCControls>();        //Controls - ETC Controls
        //GET OBJECTS -end

        

        //LISTENERS - Listen to the buttons, sliders, etc ...
        ControlsSlider.onValueChanged.AddListener(delegate { ScaleTheControls(); });    //Controls size slider
        //LISTENERS -end


        //ON-SCREEN
        //Controls
        ShowTheControls();
      
        //Button
        
        //ON-SCREEN -end


        } //Start() -end





    void ShowTheControls() {    // This method pnly gets run at start to load Controls size from PPM values

        currentControlsScale_string = PlayerPrefsManager.ControlsScale_Get().ToString();     //Get Controls Scale from PPM

        //TODO: if ... error check if sze is not within expeted range
        
        int.TryParse(currentControlsScale_string, out currentControlsScale_int);            //Convert the string to int


        //Virgin game?  -catch if Controls Scale = 0 (for some weird reason!)
        if (currentControlsScale_int == 0) {          // Initial run of game will not have a ControlsScale value saved in the PPs
            PlayerPrefsManager.ControlsScale_Set(1);   // save the new scale in PPM
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_1);  // save the new size in PPM  
            Debug.LogError("ShowTheControls() | currentControlsScale_int = 0 -- Problem with the logic!!!");
        }

        //Controls Scale 1?
        else if (currentControlsScale_int == 1) {   
            ControlsSizeField.text = "1";             //Update On-Screen Controls
            ControlsSlider.value = 1;                 //Update the On-Screen slider
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_1);  // save the new size in PPM              
        }
        //Controls Scale 2?
        else if (currentControlsScale_int == 2) {    
            ControlsSizeField.text = "2";             //Update On-Screen Controls
            ControlsSlider.value = 2;                 //Update the On-Screen slider
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_2);  // save the new size in PPM 
        }

        //Controls Scale 3?
        else if (currentControlsScale_int == 3) {     
            ControlsSizeField.text = "3";             //Update On-Screen Controls
            ControlsSlider.value = 3;                 //Update the On-Screen slider
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_3);  // save the new size in PPM 
        }

        //Controls Scale 4?
        else if (currentControlsScale_int == 4) {    
            ControlsSizeField.text = "4";             //Update On-Screen Controls
            ControlsSlider.value = 4;                 //Update the On-Screen slider
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_1);  // save the new size in PPM 
        }

        //Controls Scale 5?
        else if (currentControlsScale_int == 5) {     
            ControlsSizeField.text = "5";             //Update On-Screen Controls
            ControlsSlider.value = 5;                 //Update the On-Screen slider
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_5);  // save the new size in PPM 
        }

        //TODO: ERROR TRAP  --- IF THE Controls IS NOT 0 - 5

                    
            //ETCInput.SetControlVisible("Controls", true);  //Controls scale discovered so show it 

        }



    void ScaleTheControls() {

        //ETCInput.SetControlVisible("Controls", false);
        //1. make the screen Controls Size reflect CONTROLS_SCALE_1
        //2. make the Controls.size == currentControlsScale

        if (ControlsSlider.value == 1) {
            resizecontrols.Resize(CONTROLS_SCALE_1);  // Update On-Screen Controls
            ControlsSizeField.text = "1";             // Update On-Screen Controls Size text
            PlayerPrefsManager.ControlsScale_Set(1);   // save the new scale in PPM  
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_1);  // save the new size in PPM 

        } else if (ControlsSlider.value == 2) {
            resizecontrols.Resize(CONTROLS_SCALE_2);
            ControlsSizeField.text = "2";             // Update On-Screen Controls Size text
            PlayerPrefsManager.ControlsScale_Set(2);   // save the new scale in PPM 
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_2);  // save the new size in PPM 

        } else if (ControlsSlider.value == 3) {
            resizecontrols.Resize(CONTROLS_SCALE_3);
            ControlsSizeField.text = "3";             // Update On-Screen Controls Size text
            PlayerPrefsManager.ControlsScale_Set(3);   // save the new scale in PPM 
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_3);  // save the new size in PPM 

        } else if (ControlsSlider.value == 4) {
            resizecontrols.Resize(CONTROLS_SCALE_4);
            ControlsSizeField.text = "4";             // Update On-Screen Controls Size text
            PlayerPrefsManager.ControlsScale_Set(4);   // save the new scale in PPM 
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_4);  // save the new size in PPM 

        } else if (ControlsSlider.value == 5) {
            resizecontrols.Resize(CONTROLS_SCALE_5);
            ControlsSizeField.text = "5";             // Update On-Screen Controls Size text
            PlayerPrefsManager.ControlsScale_Set(5);   // save the new scale in PPM  
            PlayerPrefsManager.ControlsSize_Set(CONTROLS_SCALE_5);  // save the new size in PPM 
        }
    }//ScaleTheControls() -end




    void OnDestroy() {
        //Remove Listeners           
        ControlsSlider.onValueChanged.RemoveListener(delegate { ScaleTheControls(); });           //Controls size slider
       //Remove Listeners -end        
        }


}//THE END