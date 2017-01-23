/* Author: Paul Land */
/* Script: PlayerController.cs */
/* Usage: Attached to Player(object) in Level screen */
/* Version: 0.0.3 */

using UnityEngine;


public class PlayerController : MonoBehaviour {


    /* -----< DECLARATIONS >----- */

    //INSPECTOR
    //Player
    public float padding = 0.5f;                // Player object padding to surroundings
    public float playerHealth;                  // Set in the Inspector
    public float playerShield;                  // Default strength of the Players Shield
    //Projectile
    public float projectileSpeed;               // Set in the Inspector
    public float firingRate;                    // Set in the Inspector
    public float projectileDamage;              // Var holds the collided projectiles damage value
    //Sound
    public AudioClip hitSound;                  // drag and drop the clip into the inspector    
    public AudioClip fireSound;                 // drag and drop the clip into the inspector
    //Handles
    public LevelManager levelManager;
    //Shield
    public Transform shield;                    //Drop the 'Shield' on inspector
    public Transform shieldPivot;               //Drop the 'ShieldPivot' on inspector
    public float orbitDegreesPerSec = 360.0f;
    //Joystick
    public UltimateJoystick myJoystick;
    //Buton
    public UltimateButton myFire;

    //LOCAL
    //Player
    private float speed = 5.0f;
    private float playerShieldMax;              // Save value for USB
    private float playerHealthdMax;             // Save valur for USB
    public bool playerShieldStatus = true;      // Player shield up or down?
    //Projectile
    public GameObject projectile;
    //Controls
    float xmin;                                 // Touch Screen xmin;
    float xmax;                                 // Touch Screen xmax; 
   
    /* -----< DECLARATIONS - END >----- */ 



    void Start() {

        //Save Max Shield value for the Ultimate UI status bar
        playerShieldMax = playerShield;
        playerHealthdMax = playerHealth;

       //INITIALISE ON-SCREEN
        UltimateStatusBar.UpdateStatus("PlayerShield", playerShield, playerShieldMax); //Update Screen with our default Player Shield value
        UltimateStatusBar.UpdateStatus("PlayerHealth", playerHealth, playerHealthdMax);//Update Screen with our default Player Health value
       
        //TODO: Add comments to whole section
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
        
        }//Start() -end

 
    


    void Update () {


#if UNITY_STANDALONE     // Build for all Windows, Mac or Linux Standalone platforms (Input controls)

        // TODO: Hide Player Controls (UJ UB) 
        
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





#else    // Build for all touch platforms (Input controls)

        //TODO: Add comments to whole section
        //Get the size of the player controls from the PPM

        myJoystick.joystickSize = PlayerPrefsManager.GetControlsSize();
        myFire.buttonSize = PlayerPrefsManager.GetControlsSize();

        
        myFire.UpdatePositioning();         // Ask UB to update our changes
        myJoystick.UpdatePositioning();     //ask  UJ to update our changes




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
            //Debug.Log("Fire Pressed");            
            }
            if (UltimateButton.GetButtonUp("PlayerFire")) {
            //Debug.Log("Fire Released");
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
            collidedProjectile.Hit();       //Finished talking to projectile. Tell projectile it hit us so it will destroy itself.
            Shield();                       //Check shield
     
            if (playerShieldStatus == false) {      //Shields down. Player takes damage.
                Health();   //Check health              
                }
            }

    }//OnTriggerEnter2D -end

    /* -----< PLAYER HIT!!!!! -END >----- */



    void Shield() {
        
        if (playerShieldStatus == true) {       //Still some shield left so negate the 'hit' value
            playerShield -= projectileDamage;   //Take the damage value from shield total

            if (playerShield < 0) {             //Shields 0?
                playerShieldStatus = false;     //Set Shield 'down' status.
                playerShield = 0;               //Set to 0 to make the On-Screen not negative
                }

            UltimateStatusBar.UpdateStatus("PlayerShield", playerShield, playerShieldMax);
            }
        }//Shield() -end



    void Health() {

        playerHealth -= projectileDamage;   //Take damage to players health.

        if (playerHealth <= 0) {
            Die();                          //If health run out die!
            }

        UltimateStatusBar.UpdateStatus("PlayerHealth", playerHealth, playerHealthdMax);//Update Screen with our default Player Health value
        }//Health() -end



    void Die() {
        //TODO: Add comments to whole section
        Destroy(gameObject);      
        LevelManager levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>(); //Get the LevelManager.sc attached to the LevelManager game object 
        levMan.LoadLevel("Lose");
        }//Die() -end



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


