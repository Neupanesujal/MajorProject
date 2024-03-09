using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public static MouseController instance;
    
    void Awake()
   {
       instance = this;
   }
   
    public float sensitivityX = 5f,sensitivityY = 5f;
    float mouseX,mouseY;
    public Transform playerCamera;
    float xClamp =  70f;
    float xRotation = 0f;
    
    void Update()
    {
        transform.Rotate(Vector3.up * mouseX );
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, - xClamp,xClamp);
        Vector3 CamRotation = transform.eulerAngles;
        CamRotation.x = xRotation;
        playerCamera.eulerAngles = CamRotation;
    } 

    public void Reciver(Vector2 deltaMouse)
    {
        mouseX = deltaMouse.x * sensitivityX;
        mouseY = deltaMouse.y *sensitivityY;
    }

    public void SetX(float x)
    {
        sensitivityX = x; 
    }

    public void SetY(float y)
    {
        sensitivityY =y ; 
    }


}
