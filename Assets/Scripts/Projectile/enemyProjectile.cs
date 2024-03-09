using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{   
    //public GameObject gotHit;
    public GameObject Agent;
    AgentMovNShoot agent;

    void Start()
    {
        // agent = Agent.GetComponent<AgentMovNShoot>();
    }
    public int damageLevel = 25;
    void OnCollisionEnter(Collision collider)
    {
        //print(collider.gameObject.tag);
        if(collider.gameObject.tag == "Player" || collider.gameObject.tag == "goal")
        {   
            Debug.Log("goal got hit");
            // agent.SetReward(+1);
            // agent.EndEpisode();
            // agent.episodeBegin= false;
            
            collider.gameObject.GetComponent<playerHealth>().takeDamage(damageLevel);
            Destroy(gameObject);
            //gotHit.SetActive(true);
            
        }
        if(collider.gameObject.tag == "goal")
        {
            Debug.Log("k vako ");
        }
        if(collider.gameObject.tag !="Enemy" && collider.gameObject.tag !="enemyBullet" && collider.gameObject.tag !="Bullet")
        {
            Destroy(gameObject);
        }
    }
}
