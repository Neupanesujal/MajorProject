using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerHealth : MonoBehaviour
{
    public static playerHealth instance;
    public GameObject RespwanPoint;
    
    void Awake()
    {
        instance = this;
    }
    
    int currentHealth ,  maxHealth = 100;
    bool isAlive; 
    
    void Start()
    {
        Reset();
    }
    void Reset()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    public void takeDamage(int damage)
    {
        if(isAlive)      
        {
            currentHealth -= damage;   
            UImanager.instance.SetHealth(currentHealth); 
            Vibrate();
            vibratePC();
        }
        
        if(currentHealth <= 0 && isAlive) {isAlive = false; playerDied();}
        if(!isAlive)
        {
            deactivate();
        } 

    }    

    void vibratePC()
    {
        InputManager.instance.serialController.SendSerialMessage("F");
        Invoke("deactivate",2f);

    }

        void deactivate()
        {
            InputManager.instance.serialController.SendSerialMessage("S");

        }

    void Vibrate()
    {
        #if UNITY_ANDROID 
                Handheld.Vibrate();
        #endif
    }



    public void playerDied()
    {
        playerManager.instance.playerDied();
       #if UNITY_ANDROID 
            Ads.instance.ShowAds();
        #endif 
    }

    public void setHealth(int HP)
    {
        currentHealth = HP;
        transform.position = RespwanPoint.transform.position;
    }

}
