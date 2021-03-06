﻿/* Author: Paul Land */
/* Script: PlayerController.cs */
/* Usage: Attached to Player(object) in Level screen */
/* Version: 0.0.3 */

using UnityEngine;


public class PlayerController : MonoBehaviour {


    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    //Player
    public float padding = 0.5f;                // Player object padding to surroundings
     //Projectile
    public float projectileSpeed;               // Set in the Inspector
    public float firingRate;                    // Set in the Inspector
    public float projectileDamage;              // Var holds the collided projectiles damage value
    //Sound
    public AudioClip hitSound;                  // drag and drop the clip into the inspector    
    public AudioClip fireSound;                 // drag and drop the clip into the inspector
    //Shield
    public Transform shield;                    //Drop the 'Shield' on inspector
    public Transform shieldPivot;               //Drop the 'ShieldPivot' on inspector
    public float orbitDegreesPerSec = 360.0f;
  
    //LOCAL
    //Player
    private float speed = 5.0f;
    //Projectile
    public GameObject projectile;
    //Shield
    private bool playerShieldStatus;            // Player shield up or down?
    //Controls
    float xmin;                                 // Touch Screen xmin;
    float xmax;                                 // Touch Screen xmax; 
    //Number Cruncher
    public NumberCruncher nc;                   // Acess to object
    public UltimateJoystick myJoystick;           // Acess to object
    public UltimateButton myFire;               // Acess to object
    public LevelManager levelManager;


    /* -----< DECLARATIONS - END >----- */



    void Start() {

        //Find Objects
        nc = GameObject.FindObjectOfType<NumberCruncher>();
        myJoystick = GameObject.FindObjectOfType<UltimateJoystick>();
        myFire = GameObject.FindObjectOfType<UltimateButton>();     
        levelManager = GameObject.FindObjectOfType<LevelManager>();




        //TODO: Add comments to whole section
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;



        //Get the size of the player controls from the PPM 
        //Also applies changes made to the controls in the setting menu
        myJoystick.joystickSize = PlayerPrefsManager.ControlsSize_Get();
        myFire.buttonSize = PlayerPrefsManager.ControlsSize_Get();
        myFire.UpdatePositioning();         // ask UB to update our changes
        myJoystick.UpdatePositioning();     // ask UJ to update our changes

       

        }//Start() -end





    void Update () {

        



#if UNITY_STANDALONE     // Build for all Windows, Mac or Linux Standalone platforms (Input controls)

        // TODO: Add comments to whole section
        
        // Player movement 
        if (Input.GetKey(KeyCode.LeftArrow)) {       //Move Left?
            transform.position += Vector3.left * speed * Time.deltaTime;
            }
        else if (Input.GetKey(KeyCode.RightArrow)) {   //Move RIght?
            transform.position += Vector3.right * speed * Time.deltaTime;
            }

        // Restrict the player to the gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Player movement -end



        // Projectile
        if (Input.GetKeyDown(KeyCode.Space)) {

            InvokeRepeating("Fire", 0.0001f, firingRate);
            }
       
        // Projectile -end





#else   // Build for all touch platforms (Input controls)
            

        //Player movement 
        float shipMovement = UltimateJoystick.GetHorizontalAxis("PlayerJoystick");
        transform.position += Vector3.left * speed * Time.deltaTime * -shipMovement;  

        // Restrict the player to the gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        //Player movement -end
       

        //Projectile
        if (UltimateButton.GetButtonDown("PlayerFire")) {               
            InvokeRepeating("Fire", 0.000001f, firingRate);
            Debug.Log("Fire Pressed");            
            }
       
            if (UltimateButton.GetButtonUp("PlayerFire")) {
            Debug.Log("Fire Released");
            CancelInvoke("Fire");
        }//Projectile -end



#endif
        /* -----< GENERAL Update() >----- */

        Orbit(); // Show shield satellites


        /* -----< GENERAL Update -END() >----- */
        } // void Update -end



    /* -----< PLAYER HIT!!!!! >----- */
    void OnTriggerEnter2D(Collider2D collider) {

        Projectile  collidedProjectile = collider.gameObject.GetComponent<Projectile>(); //Get a handle on the projectile we collided with

        if (collidedProjectile) {                                       //Player collided with projectile
            AudioSource.PlayClipAtPoint(hitSound, transform.position);  //Play hit SFX
            projectileDamage = collidedProjectile.GetDamage();          //Get the damage value from collided projectile 
            collidedProjectile.Hit();                                   //Finished talking to projectile. Tell projectile it hit us so it will destroy itself.
            nc.Player_Hit(projectileDamage);                            //Pass the damage value to NumberCrumcher
        }
    }//OnTriggerEnter2D -end

    /* -----< PLAYER HIT!!!!! -END >----- */

 




    void Fire() {

        //TODO: Add comments to whole section

        Vector3 startPosition = transform.position + new Vector3(0, 1, 0);
        GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);

        AudioSource.PlayClipAtPoint(fireSound, transform.position);  //Play fire SFX

        // Fire Button
        if (Input.GetKeyDown(KeyCode.Space)) {
            CancelInvoke("Fire");
            }

        }//Fire() -end




    void Orbit() {
        shieldPivot.localRotation = Quaternion.AngleAxis(orbitDegreesPerSec * Time.time, Vector3.back);
        
        }//Orbit -end


    }//THE END


