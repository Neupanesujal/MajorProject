using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{
    
    bool        walkPointSet = false,   alreadyAttacked = false;
    
    public float  gravity,h = 5f,  lookRadius = 30f,      distance=10f,    timeBetweenAttacks=3f,      walkPointRange = 20f ;
    
    public GameObject projectile;
    
    NavMeshAgent agent;    
    
    Vector3 walkPoint, player;
    public Transform firePoint;

    
    // Start is called before the first frame update
    void Start()
    {
        gravity = playerMovement.instance.gravity * 4f;        
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void gotPlayer(Vector3 _player)
    {
        player = _player;
        if(player != null ) distance = Vector3.Distance(player,transform.position);
        
        if(distance <= lookRadius)  Target();
        
        else Patroling();
        
    }   

    void Target()
    {
        agent.SetDestination(player);
        if(distance <= agent.stoppingDistance)  {   lookTarget();   }
       
    }

    void lookTarget()
    {
        Vector3 direction = (player - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime* 2);
        attackTarget();
    }
    
    //random walk
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)   agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //print("seaeching");
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
          
    }
    void attackTarget()
    {
        if (!alreadyAttacked)
        {
            Launch();
            ///Attack code here
            /*Rigidbody rb = Instantiate(projectile,( firePoint.position), Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);*/
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    
    void ResetAttack()
    {
        alreadyAttacked = false;
    }


    void Launch() 
    {
		Physics.gravity = Vector3.up * gravity;		
        Rigidbody ball =Instantiate(projectile,( firePoint.position), Quaternion.identity).GetComponent<Rigidbody>(); 
		Vector3 direction =  CalculateLaunchData().initialVelocity;
        // ball.AddForce(direction.forward * 12f,ForceMode.Impulse);
        //ball.AddForce(direction.up , ForceMode.Impulse);
        ball.velocity = CalculateLaunchData ().initialVelocity;
	}

	LaunchData CalculateLaunchData() 
    {
		float displacementY = player.y - firePoint.position.y;
		Vector3 displacementXZ = new Vector3 (player.x - firePoint.position.x, 0, player.z - firePoint.position.z);
		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;
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


