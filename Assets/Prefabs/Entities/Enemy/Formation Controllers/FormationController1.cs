/***************************************************************************
Author: Paul Land
Creation Date: 01/12/2016
Game: Infinite Invasion
Unity Script: FormationController1.cs
Location: /_Scripts/Prefabs/Entities/Enemy/Formation Controllers
Parent: Formation canvas (e.g. EnemyFormation1) parent of the Enemies.
             
Methods:    PUBLIC
            -------
                      
            PRIVATE
            -------
   
PROJECT PLAN 
============
Overview -

      Job List
      --------
      1.

*****************************************************************************/



using UnityEngine;
//using System.Collections;

public class FormationController1 : MonoBehaviour
{

    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    //Enemy
    public GameObject enemyPrefab;          //Drop the Enemy Prefab here in the Unity Inspector   
    public float formWidth;                 //Enemy formation width - NOTE! set in inspector
    public float formHeight;                //Enemy formation height - NOTE! set in inspector
    public bool movingRight = true;         //Enemy movement direction flag
    public float speed = 1f;                //Enemy movement speed
    public float spawnDelay = 0.5f;         //Enemy spawn creation delay
    public bool spawned = false;            //All spawned flag      


    //SCRIPT
    private float xmax;                     //Left boundry of enemy formation
    private float xmin;                     //Right boundry of enemy formation

    public NumberCruncher nc;
    private Enemy enemy;
    
    
    
    /* -----< DECLARATIONS -END >----- */





    void Start() {
        
        //Find Objects
        nc = GameObject.FindObjectOfType<NumberCruncher>();
        enemy = GameObject.FindObjectOfType<Enemy>();


        //nc.Enemies_Spawned(false);



        //Edge of screen stuff
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightBoundry.x;
        xmin = leftBoundry.x;
        //Edge of screen stuff - end

        SpawnUntilFull();  //Populate with enemies

        spawned = true;
        enemy.Formation_Spawned(GetInstanceID(), spawned);  //Report 'Spawned'  to enemy manager

        }//Start() -end




    //Draw a gizmo on the screen so that the enemy positions can me dragged around at design time
    public void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(formWidth, formHeight));
    }//Draw a gizmo -end

    //void Start() -end


    void Update(){

        //Enemy movement section
        if (movingRight) {
            transform.position += new Vector3(speed * Time.deltaTime, 0);
        } else {
            transform.position += new Vector3(-speed * Time.deltaTime, 0);
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * formWidth);  
        float leftEdgeOfFormation = transform.position.x - (0.5f * formWidth);   

        if (leftEdgeOfFormation < xmin){  //
            movingRight = true;
        } else if (rightEdgeOfFormation > xmax){
            movingRight = false;
        }
        //Enemy movement section -end


        if (AllMembersDead()) {    //Are all the enemies dead?
                                  
           
            //Debug.Log("Aliens Dead!");
        }

    } //void Update -end




    bool AllMembersDead() {     //Are the enemies all dead?

        foreach (Transform childPositionGameObject in transform) {
            if (childPositionGameObject.childCount > 0) {
                return false;   //still enemies left
            }
        }
        return true;            //no enemies left
    } //AllMembersDead() -end




    void SpawnUntilFull() {
        Transform freePosition = NextFreePosition();  //Get the next available free enemy child position
        if (freePosition) {
            GameObject enemyShip = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemyShip.transform.parent = freePosition;

            enemy.Enemy_Count();   //Get a count of enemies

            }
        if (NextFreePosition()) {

  

            Invoke("SpawnUntilFull", spawnDelay);    //Invokes the function again after period of time held in spawnDelay
            }       
        }//SpawnUntilFull() -end





    Transform NextFreePosition() {   //Single enemy spawner function
        foreach (Transform EnemychildPosition in transform) {
            if (EnemychildPosition.childCount == 0) {   //child enemy 'position' no enemy in it?
                return EnemychildPosition;  
            }
        }
        return null;
    }//NextFreePosition() -end

    


}//The End

	