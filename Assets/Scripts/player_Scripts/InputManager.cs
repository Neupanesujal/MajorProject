using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    //class to be called for reciving inputs
    public static InputManager instance;
    PlayerInput input;
    Vector2 movement,movement1,deltaMouse;
    bool jump;

    public SerialController serialController;
    
    void Awake()
    {   
        instance = this;
        input = new PlayerInput();
        newInput();
    }

        void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();

    }
    int fire;
    void newInput()
    {
        input.controller.movement.performed += Move => { movement = Move.ReadValue<Vector2>();};
        input.controller.movement1.performed += Move1 => { movement1 = Move1.ReadValue<Vector2>();};
        input.controller.jump.performed += Jump => { playerMovement.instance.jump = true;  };
        
        
        input.controller.mouseX.performed += mouseX =>{deltaMouse.x = mouseX.ReadValue<float>();};
        input.controller.mouseY.performed += mouseY =>{deltaMouse.y = mouseY.ReadValue<float>();};
        
        
        input.controller.fireL.performed += fireL => playerShoot.instance.shootL();
        input.controller.fireL1.performed += fireL1 => fire = 1;
        input.controller.fireL1.canceled += fireL1 => fire = 0;
        input.controller.fireR.performed += fire =>playerShoot.instance.shootR();

        input.controller.interact.performed += collect => playerMovement.instance.collect();
        //  UI management 
        input.controller.escape.performed += escape => UImanager.instance.Settings();
    }
        void Update()
        {
          playerMovement.instance.Movement(movement);
          if(movement.y != 0)
          {
            UImanager.instance.SetCalorie();
          }

          MouseController.instance.Reciver(deltaMouse);
        }


        public Vector2 AgentMove()
        {
            return movement1;
        }

        public int justFired()
        {
            return fire;
        }



    void OnEnable()
    {
        input.controller.Enable();
    }

    void OnDisable()
    {
        input.controller.Disable();
    }


}
