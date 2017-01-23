using UnityEngine;

public class BasicPlayerController : MonoBehaviour {
    
    //DECLARATIONS
    //Player
    private float speed = 5.0f;
    public float padding = 0.5f;
    public float playerHealth = 500;            //Set in the Inspector
    public float playerShield = 1000;           //Default strength of the Players Shield
    public bool playerShieldStatus = true;      //Player shield up or down?

    //Controls
    public float horizontal;                    //Touch test
    float xmin;                                 //Touch Screen xmin;
    float xmax;                                 //Touch Screen xmax;


    /* 

     //Shield
     public float orbitDistance = 0.5f;
     public float orbitDegreesPerSec = 360.0f;
     public Vector3 relativeDistance = Vector3.zero;
     private Transform shield;

     */


    //Shield

    public Transform shield;            //Drop the 'Shield' on inspector
    public Transform shieldPivot;       //Drop the 'ShieldPivot' on inspector
    public float orbitDegreesPerSec = 360.0f;



    //DECLARATIONS -end



    void Start() {

        

        //CAMERA 
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
        //CAMERA -end


        }//Start() -end


    void Update() {

        //Player movement 
        if (Input.GetKey(KeyCode.LeftArrow)) {       //Move Left?
            transform.position += Vector3.left * speed * Time.deltaTime;
            }

        else if (Input.GetKey(KeyCode.RightArrow)) {   //Move RIght?
            transform.position += Vector3.right * speed * Time.deltaTime;
            }


        //restrict the player to the gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        //Player movement -end

        
        Orbit();
        
        }
    

    void Orbit() {


        // Keep us at the last known relative position
        //shield.transform.position = transform.position + (shield.transform.position - transform.position).normalized * orbitDistance;

        //Note: Vector3.back rotates round the z axis of player clockwise
        //shield.transform.RotateAround(transform.position, Vector3.back, orbitDegreesPerSec * Time.deltaTime);


        // Reset relative position after rotate
        //relativeDistance = shield.transform.position - transform.position;


        shieldPivot.localRotation = Quaternion.AngleAxis(orbitDegreesPerSec * Time.time, Vector3.back);


        }//Orbit -end

    }
