using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
   public static playerShoot instance;

   void Awake()
   {
        if(instance == null)    instance = this;
   }

    public Camera playerCamera;
    public Transform rightPalm,leftPalm;
    public GameObject attackProjectile, defendProjectile;
    Vector3 destination;
    Ray ray;
    RaycastHit hit;
    
    
    public void shootL()
    {
        shoot(leftPalm,attackProjectile);
    }

    public void shootR()
    {
        shoot(rightPalm,defendProjectile);
    }

    void shoot(Transform firePoint, GameObject projecile)
    {
         ray = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));

        
    
        if(Physics.Raycast(ray, out hit))
        destination = hit.point;
        else destination = ray.GetPoint(1000);

        InstantiateProjectile(firePoint,projecile);
    }

    
    public float projectileSpeed;
    

    void InstantiateProjectile(Transform firePoint, GameObject projecile)
    {
        var projecObject = Instantiate(projecile,firePoint.position, Quaternion.identity) as GameObject;
        projecObject.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        Destroy(projecObject,10);
    
    }

    

}
/* public class Example : MonoBehaviour
{
    // Frame update example: Draws a 10 meter long green line from the position for 1 frame.
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
    }
}

*/