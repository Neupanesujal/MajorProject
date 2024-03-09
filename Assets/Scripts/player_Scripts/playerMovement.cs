using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class playerMovement : MonoBehaviour
{   
    public static playerMovement instance;
    CharacterController controller;
    public LayerMask groundMask,LavaMask; 
    public GameObject playerBase;
    bool isGrounded;    
    public bool jump;
    public float gravity = -9.8f , jumpHeight = 10f, speed = 2f;
    int coin;
    
    public float enemyRadius;
    Vector3 horizontal,vertical;
    
    void Awake()
    {
        instance = this;
        controller = GetComponent<CharacterController>();
    }
    
    void Start()
    { 
        vertical.y = Mathf.Sqrt( -2 * jumpHeight *gravity);
    }
    
    
    Vector2 movementDirection;
    
    public void Movement(Vector2 direction)
    {   
        movementDirection = direction;
//        Debug.Log(direction);
        horizontal = transform.right * direction.x + transform.forward * direction.y;
        commandController(horizontal);
    }

    public Vector2 getDirection()
    {   
        //Debug.Log("ya");
        return movementDirection;
    }

    public void Jump()
    {
        {
            commandController(vertical);
        }
        jump = false;
    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(playerBase.transform.position,.2f,groundMask);
        if(Physics.CheckSphere(playerBase.transform.position,.1f,LavaMask) || transform.position.y <= -20f) playerHealth.instance.playerDied();
        Gravity();
        GetMe();
        if(jump && isGrounded) Jump();
        else jump = false;    
    }

Vector3 _gravity;
    void Gravity()
    {       
        if(isGrounded)
            {
                _gravity.y = 0.1f;

            }

        
        _gravity.y += gravity * Time.deltaTime;
        commandController(_gravity);
    }
    void commandController(Vector3 location)
    {
        controller.Move(location *Time.deltaTime * speed );
    }

    public void collect()
    {
      
        Collider [] interacts = Physics.OverlapSphere(transform.position,2f);
        foreach (Collider interact in interacts)
        {
           //print(interact.gameObject.name);
           if(interact.gameObject.tag == "Coin")
           coin += interact.gameObject.GetComponent<collectable>().collect();
        }
    }

    void GetMe()
    {
        Collider [] Enemies = Physics.OverlapSphere(transform.position,enemyRadius);
        foreach (Collider Enemy in Enemies)
        {           
            if(Enemy.gameObject.tag == "Enemy")
            {   
                Enemy.gameObject.GetComponent<Movement>().gotPlayer(transform.position);
            }
        }

    }

}