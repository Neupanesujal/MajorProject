using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
   
    public int pointValue = 1;
    bool collected = false;
    public float spinSpeed = 20;

    public int collect()
    {
       collected = true;
        return pointValue;
    } 

    void Update()
    {
       if(collected){ playerManager.instance.levelComplete(); /* play sound */ Destroy(gameObject);}
        transform.Rotate(0f,spinSpeed * Time.deltaTime ,0f, Space.Self);
    }


}
