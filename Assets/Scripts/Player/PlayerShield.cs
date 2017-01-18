/* Author: Paul Land */
/* Script: PlayerShield.cs */
/* Usage: Attached to Player(object) in Level screen */
/* Version: 0.0.1 */

using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour {

    /* -----< DECLARATIONS >----- */

    //INSPECTOR
    //Shield
    public float orbitDistance = 1.0f;
    public float orbitDegreesPerSec = 360.0f;
    //Player
    public Transform player;


    //LOCAL
    //Shield
    public Vector3 relativeDistance = Vector3.zero;

    /* -----< DECLARATIONS - END >----- */





    private void Start() {

       //GET OBJECTS - Dynamically find objects at runtime
       player = GameObject.Find("Player").transform;      //Find and assign the players transform
       //GET OBJECTS -end
       

        if (player != null) {       //If there is a target (Player)
            relativeDistance = transform.position - player.position;
            }
        

        }//Start -end



    void Orbit() {
        if (player != null) {
            
            // Keep us at the last known relative position
           // transform.position = player.position + relativeDistance;    //Start point of rotation

            transform.position = player.position + (transform.position - player.position).normalized * orbitDistance;





            //Note: Vector3.back rotates round the z axis of player clockwise
            transform.RotateAround(player.position, Vector3.back, orbitDegreesPerSec * Time.deltaTime);  
            
            
            // Reset relative position after rotate
            relativeDistance = transform.position - player.position;
            }
        }//Orbit -end







    void LateUpdate() {

        Orbit();

        }






    }//THE END
