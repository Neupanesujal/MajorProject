using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public  static playerManager instance;
    GameObject gameManager;
    
    void Awake()
    {
        instance = this;
    } 
    public void playerDied()
    {
        UImanager.instance.playerDied();
        manageBackGroundScripts();
    }

    public void levelComplete()
    {
        UImanager.instance.levelComplete();
    }

    void manageBackGroundScripts()
    {
        gameManager = InputManager.instance.gameObject;
        gameManager.SetActive(false);
        gameObject.GetComponent<playerMovement>().enabled = false;
        Time.timeScale = 0f;
    }

    public void revivePlayer(int hp)
    {
        playerHealth.instance.setHealth(hp);
        UImanager.instance.revivePlayer();
        gameObject.GetComponent<playerMovement>().enabled = true;
        gameManager.SetActive(true);
        Invoke("delayTime",2f);
                    
    }

    void delayTime()
    {
        Time.timeScale = 1f;
    }
    

    
}
