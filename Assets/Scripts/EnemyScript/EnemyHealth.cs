using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    bool dead = false;
    EnemyHealth instance;
    public GameObject coin,OnHit,OnDeath;
    void Awake()
    {
    instance = this;
    }
 
    int health = 100, maxHealth = 100;
    public  void Health(int damage) // damage mechanism add later)
    {
      
        if(!dead)
        {
            health -=damage;
            GameObject obj = Instantiate(OnHit);
            obj.transform.position = transform.position;
            Destroy(obj,2f);
            if(health <= 0 ) 
            {
               dead = true;

               if(coin != null) 
               {
                    GameObject Coin =  Instantiate(coin);
                    Coin.transform.position = transform.position;

               }
               // add some particle system
                UImanager.instance.EnemyCount();
                GameObject obj1 = Instantiate(OnHit);
                obj1.transform.position = transform.position;
                Destroy(obj1,3f);
                Destroy(gameObject);
            }
        }
        if(health >= maxHealth) health = maxHealth;

    }
}
