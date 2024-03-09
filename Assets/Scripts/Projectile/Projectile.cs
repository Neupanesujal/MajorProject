using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public GameObject impactVfx;
    void OnCollisionEnter(Collision collision )
    {   
        bool collided = false;
        //print(collision.gameObject.tag);
        if(gameObject.tag !="Enemy" && collision.gameObject.tag =="Enemy")
        {
             collision.gameObject.GetComponent<EnemyHealth>().Health(damage);
        }

        if(gameObject.tag !="Enemy" && collision.gameObject.tag =="agent")
        {
             collision.gameObject.GetComponent<EnemyHealth>().Health(damage);
        }
        

        if(!collided && collision.gameObject.tag !="Bullet" && collision.gameObject.tag != "Player")
        {
            collided = true;
            var impact = Instantiate(impactVfx,collision.contacts[0].point,Quaternion.identity) as GameObject;
            Destroy(impact,2);
            Destroy(gameObject);
        }


    }
}
