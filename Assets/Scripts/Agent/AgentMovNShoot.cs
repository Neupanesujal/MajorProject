using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class AgentMovNShoot : Agent 
 {
    [SerializeField] private Transform target; //for randomly initializing target position
    // [SerializeField] public Transform firePoint;
    // [SerializeField] public GameObject projectile;
    public float moveSpeed= 0.5f; //movment speed of a player
    float rayLength = 50f;  //rayLength distance
    float[] rayAngles = { 0f, 30f,60f, 90f, 120f, 150f, 180f ,210f, 240f, 270f, 300f, 330f}; //angle at which agent fires
    //List<int> rayAngles = new List<int> {0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330};
    //RaycastHit[] rayHits = new RaycastHit[12];

    //List<float> rayAngles = new List<float> {0f, 0.5236f, 1.0472f, 1.5708f, 2.0944f, 2.6179f, 3.1416f, 3.6652f, 4.1888f, 4.7124f, 5.2360f, 5.7596f};

    float shotAvailable= 15f; //for reload
    int shootAction; //check if agent shoot, discrete action
    //private float previousDistanceToPlayer = 0f; // Added to track previous distance


    [SerializeField] private Material winMaterial;
    [SerializeField] private Material looseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    // public Transform firePoint;
    // public GameObject projectile;
    launchFire shootScript;
    float disctanceBetThem;
    public bool episodeBegin= false;

    private void Start()
    {
        shootScript = GetComponent<launchFire>();
    }

    private void Update() 
    {
        disctanceBetThem =Vector3.Distance(transform.position, target.position);
        RayPerception();
        shotAvailable--;
    }

    public override void OnEpisodeBegin()
    {
        // if(!episodeBegin)
        // {
        //     //transform.localPosition= new Vector3(Random.Range(-18f, 22f),1.7f, Random.Range(-20f, 12f));
        //     //target.localPosition= new Vector3(Random.Range(-18f, 22f),1.7f, Random.Range(-20f, 12f));
        //     episodeBegin= true;
        // }
        //target.localPosition= new Vector3(Random.Range(280f, 5f),1.7f, Random.Range(320f, 40f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {

        shootAction = actions.DiscreteActions[0];
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        //float shootAction= actions.ContinuousActions[2];
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;


        // float newDistanceToPlayer = Vector3.Distance(transform.localPosition, target.localPosition);
        // float distanceChange = previousDistanceToPlayer - newDistanceToPlayer;

        // if (distanceChange > 0f) // Reward for getting closer
        // { 
        //     AddReward(0.001f);
        // }

       // previousDistanceToPlayer = newDistanceToPlayer;
    }

    //bool isReleased = true, can  Press = true;
    Vector2 direction;
    Vector2 agentDir;

    
    public override void Heuristic(in ActionBuffers actionOut)
    {   
        //Debug.Log(direction);
        ActionSegment<float> continuousActions= actionOut.ContinuousActions;
        ActionSegment<int> discreteActions= actionOut.DiscreteActions;
        // discreteActions[0]= Input.GetKey(KeyCode.Space) ? 0 : 1;
        // discreteActions[0]= checkFire();
        discreteActions[0]= InputManager.instance.justFired();
        agentDir= InputManager.instance.AgentMove();
        // continuousActions[0] = direction.x;
        // continuousActions[1] = direction.y;
        continuousActions[0] = agentDir.x;
        continuousActions[1] = agentDir.y;
        // direction = playerMovement.instance.getDirection();
       
    }
    
    private void RayPerception()
    {
        for(int i=0; i < rayAngles.Length; i++)
        {
            Vector3 rayDirection = Quaternion.Euler(0f, rayAngles[i], 0f) * transform.forward;
            Debug.DrawRay(transform.position, rayDirection, Color.green, 100f);
            if (Physics.Raycast(transform.position, rayDirection,out RaycastHit hit, rayLength))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("player detected");
                    transform.Rotate(0f,30f*i,0f);
                    if(shootAction == 0 && disctanceBetThem< 20f)
                    {
                            Shoot();
                            shootAction=1;
                    }
                    
                }
            }
        }
    }

    public void Shoot()
    {
        if(shotAvailable > 0f)
        {
            return;
        }
        shotAvailable= 15f;
        Debug.Log("PlayerShooted");
        
        shootScript.Launch(target.position);


        //  if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayLength))
        //     {
        //      if (hit.collider.CompareTag("goal") || hit.collider.CompareTag("Player"))
        //       {

        //         Debug.Log("Goal got hit");

        //         // GameObject ball = Instantiate(projectile, firePoint.position, Quaternion.identity);
        //         // Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        //         // ballRigidbody.velocity = ball.transform.forward * 10f;

        //         floorMeshRenderer.material= winMaterial;
        //         SetReward(+1f);
        //         EndEpisode();

        //     }
        // }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player le goal xoyo");
            SetReward(+5f);
            floorMeshRenderer.material= winMaterial;
            episodeBegin= false;
            EndEpisode();
        }
        if(other.CompareTag("wall"))
        {
            Debug.Log("Player le wall xoyo");
            SetReward(-1f);
            floorMeshRenderer.material= looseMaterial;
            episodeBegin= false;
            EndEpisode();
        }
    }
}

