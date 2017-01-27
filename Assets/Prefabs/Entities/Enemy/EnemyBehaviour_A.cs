/*******************************************************************************************************
Author: Paul Land
Creation Date: 01/08/2016
Game: Infinite Invasion
Unity Script: EnemyBehaviour_A.cs
Location: /....
Parent: Enemy object (various)
Description: Responsible for enemy beahviour

Methods:    ....


NOTES: Part of the enemy object


PROJECT PLAN FOR HUD
====================
Overview - 
      
        Job List
        --------
        1. Remove local changes and pipe them to NumberCruncher


*******************************************************************************************************/

using UnityEngine;
//using System.Collections;

public class EnemyBehaviour_A : MonoBehaviour {

    /* -----< DECLARATIONS >----- */
    //INSPECTOR
    //Script Connect
    public NumberCruncher nc;                   // Acess to object (need to do a find as the object this 
                                                // script is attached to is only instantiated at runtime.

    //Objects
    public GameObject projectile;
    //Health
    public float enemyhealth = 150f;
    //Projectile
    public float projectileSpeed = 10;
    public float shotsPerSecond = 0.5f;
    //Score
    public int scoreValue = 150;
    //Audio
    public AudioClip fireSound;
    public AudioClip deathSound;

    //LOCAL 
    
    /* -----< DECLARATIONS - END >----- */




    private void Start() {

        //Dynamically get objects and assign a handle
         nc = GameObject.FindObjectOfType<NumberCruncher>();       //NumberCruncher

    }

    

    void Update() {

        //Fire
        float probablity = Time.deltaTime * shotsPerSecond;   //Probablity of fire...
        if (Random.value < probablity) {
            Fire();
        }



    }

    void Fire() {
        GameObject missile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);  //Play fire SFX
    }


    //Has this enemy been hit?
    void OnTriggerEnter2D(Collider2D collider) {

        Projectile missile = collider.gameObject.GetComponent<Projectile>();

        if (missile) {                              //Enemy hit by a Projectile?

            enemyhealth -= missile.GetDamage();     //Get the missile damage value and subtract it from the enemy health
            missile.Hit();                          //Distroy the players missile object

            if (enemyhealth <= 0) {                 //Enemy ready to die?
                Die();
            }
         } //if (missile) -end
      } //OnTriggerEnter2D  -end






    //ENEMY DEAD!
    void Die() {

       
        nc.Score_Add(scoreValue);  //Hit enemy. Pass 'scoreValue' to the ScoreKeeper
        AudioSource.PlayClipAtPoint(deathSound, transform.position);  //Play death SFX
        Destroy(gameObject);            //Destroy our enemy game object
        
    } //void Die -end




} //THE END



