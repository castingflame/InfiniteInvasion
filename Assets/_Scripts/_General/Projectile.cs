using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float damage = 100f;  //damage done by projectile - NOTE! set in inspector

    public float GetDamage() {
        return damage;
    }


    public void Hit() {
        Destroy(gameObject);
    }


}

