using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchFire : MonoBehaviour
{
    public Transform firePoint;
    Vector3 player;
    public GameObject projectile;
    public float  gravity = -1,h = 5f,  lookRadius = 30f,      distance=10f,    timeBetweenAttacks=3f,      walkPointRange = 20f ;
    public void Launch(Vector3 _player) 
    {
        player = _player;
		Physics.gravity = Vector3.up * gravity;		
        GameObject _ball =Instantiate(projectile,( firePoint.position), Quaternion.identity);
        Rigidbody ball =   _ball.GetComponent<Rigidbody>(); 
		Vector3 direction =  CalculateLaunchData().initialVelocity;
        //ball.AddForce(direction.forward * 12f,ForceMode.Impulse);
        //ball.AddForce(direction.up , ForceMode.Impulse);
        if(ball != null) ball.velocity = CalculateLaunchData().initialVelocity;
		//Destroy(_ball,5f);
	}

	LaunchData CalculateLaunchData() 
    {
		// float displacementY = transform.position.y - firePoint.position.y;
		// Vector3 displacementXZ = new Vector3 (transform.position.x - firePoint.position.x, 0, transform.position.z - firePoint.position.z);
        
		float displacementY = player.y - firePoint.position.y;
        Vector3 displacementXZ = new Vector3 (player.x - firePoint.position.x, 0, player.z - firePoint.position.z);
		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;
        // Debug.Log("yo hooooo");
        // Debug.Log(h + "H ko val" );
        // Debug.Log("yo hooooo");
        // Debug.Log(time + "Time ko val");
        // Debug.Log("yo hooooo");

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	
	struct LaunchData {
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData (Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
    }
}
