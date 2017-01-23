using UnityEngine;
using UnityEngine.UI;



public class Settings : MonoBehaviour {

    //DECLARATIONS

    private Slider joystickSlider;                      //Var for On-screen Joystick size slider
    private Text joystickSizeField;                     //Var for On-screen Joystick Size field
   // private ETCJoystick inputJoystick;                  //Var for ETC Joystick


    private string currentJoystickScale_string;         //Var to hold joystick scale before it gets written to PPM
    private int currentJoystickScale_int;               //Var to hold parsed currentJoystickScale string to int

    //private float currentJoystickSize;                  //Var to hold joystick size before it get written to PPM


    public const int JOYSTICK_SCALE_1 = 100;
    public const int JOYSTICK_SCALE_2 = 150;
    public const int JOYSTICK_SCALE_3 = 200;
    public const int JOYSTICK_SCALE_4 = 250;
    public const int JOYSTICK_SCALE_5 = 300;

    public const int BUTTON1_SCALE_1 = 100;
    public const int BUTTON1_SCALE_2 = 150;
    public const int BUTTON1_SCALE_3 = 200;
    public const int BUTTON1_SCALE_4 = 250;
    public const int BUTTON1_SCALE_5 = 300;





    //DECLARATIONS -end;



    private void Awake() {
        
        //HIDE OBJECTS
        //ETCInput.SetControlVisible("Joystick", false);  //Hide The Joystick until the scale is discovered
        
        
        
        //HIDE OBJECTS -end


        }// Awake() -end



    private void Start() {


        //GET OBJECTS - Dynamically find objects at runtime
        joystickSlider = GameObject.Find("Joystick_sldr").GetComponent<Slider>();       //Joystick size - slider
        joystickSizeField = GameObject.Find("JoystickScale").GetComponent<Text>();      //Joystick size - filed
        //inputJoystick = GameObject.Find("Joystick").GetComponent<ETCJoystick>();        //Joystick - ETC Joystick
        //GET OBJECTS -end

        

        //LISTENERS - Listen to the buttons, sliders, etc ...
        joystickSlider.onValueChanged.AddListener(delegate { ScaleTheJoystick(); });    //Joystick size slider
        //LISTENERS -end


        //ON-SCREEN
        //Joystick
        ShowTheJoystick();
      
        //Button
        
        //ON-SCREEN -end


        } //Start() -end





    void ShowTheJoystick() {

        currentJoystickScale_string = PlayerPrefsManager.GetJoystickScale().ToString();     //Get Joystick Scale from PPM

        //TODO: if ... error check if sze is not within expeted range
        
        int.TryParse(currentJoystickScale_string, out currentJoystickScale_int);            //Convert the string to int


        //Virgin game?  -catch if Joystick Scale = 0
        if (currentJoystickScale_int == 0) {          //Initial run of game will not have a JoystickScale value saved in the PPs
            PlayerPrefsManager.SetJoystickScale(1);   //Set the Initial size of the Joystick to JOYSTICK_SCALE_1 
            }

        //Joystick Scale 1?
        else if (currentJoystickScale_int == 1) {   
            joystickSizeField.text = "1";             //Update On-Screen Joystick
            joystickSlider.value = 1;                 //Update the On-Screen slider
            //TODO: SCALE THE JOYSTICK CONTROL HERE
            }
        //Joystick Scale 2?
        else if (currentJoystickScale_int == 2) {    
            joystickSizeField.text = "2";             //Update On-Screen Joystick
            joystickSlider.value = 2;                 //Update the On-Screen slider
            //TODO: SCALE THE JOYSTICK CONTROL HERE
            }

        //Joystick Scale 3?
        else if (currentJoystickScale_int == 3) {     
            joystickSizeField.text = "3";             //Update On-Screen Joystick
            joystickSlider.value = 3;                 //Update the On-Screen slider
            //TODO: SCALE THE JOYSTICK CONTROL HERE
            }

        //Joystick Scale 4?
        else if (currentJoystickScale_int == 4) {    
            joystickSizeField.text = "4";             //Update On-Screen Joystick
            joystickSlider.value = 4;                 //Update the On-Screen slider
            //TODO: SCALE THE JOYSTICK CONTROL HERE
            }

        //Joystick Scale 5?
        else if (currentJoystickScale_int == 5) {     
            joystickSizeField.text = "5";             //Update On-Screen Joystick
            joystickSlider.value = 5;                 //Update the On-Screen slider
            //TODO: SCALE THE JOYSTICK CONTROL HERE
            }

        //TODO: IF THE JOYSTICK IS NOT 0 - 5....

                    
            //ETCInput.SetControlVisible("Joystick", true);  //Joystick scale discovered so show it 

        }


    void ScaleTheJoystick() {

        //ETCInput.SetControlVisible("Joystick", false);


        //1. make the screen Joystick Size reflect JOYSTICK_SCALE_1
        //2. make the Joystick.size == currentJoystickScale


        }


    

    void OnDestroy() {

        //Remove Listeners           
        joystickSlider.onValueChanged.RemoveListener(delegate { ScaleTheJoystick(); });           //Joystick size slider

        //Remove Listeners -end        
    }


}
