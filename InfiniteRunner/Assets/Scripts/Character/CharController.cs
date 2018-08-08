using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Character;
using FSM;
using InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{

   
   
    bool isJumping;  
    bool isSliding;
    bool isGrounded = true; 
    bool isFalling;
    bool isRunning = true;

    internal int currentLane = 1;  
    internal int targetLane = 1;
    float constMoveSpeed = 5;
    float laneChangeSpeed;
    float laneOffset;
    float turnSpeed;
    float jumpForce; 
    internal float verticalVelocity;   
    float gravity;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    float rayDistance;

    /// <summary>
    /// 0. Init
    /// 1. Run
    /// 2. Jump
    /// 3. Slide
    /// 4. Dead
    /// </summary>
    [SerializeField]
    State[] characterStates;
    
    StateMachine characterFSM = new StateMachine();

    [SerializeField]
    private CharacterController controller;
  

 

    private void Awake()
    {
        
        ChangeCharacterState(characterStates[0]);
    }

    private void Start()
    {
       
        ChangeCharacterState(characterStates[1]);
       
    }


    public void SetMovementData(CharMovementData data)
    {
        laneChangeSpeed = data.laneChangeSpeed;
        constMoveSpeed = data.constMoveSpeed;
        gravity = data.gravity;
        laneOffset = data.laneOffset;
        turnSpeed = data.turnSpeed;
        jumpForce = data.jumpForce;
        

    }


    private void CalculateFinalMovement()
    {

        //Calculate target position to move
        Vector3 targetPosition = transform.position.z * Vector3.forward;

        if (targetLane == 0)
        {
            targetPosition += Vector3.left * laneOffset;
        }

        else if (targetLane == 2)
        {
            targetPosition += Vector3.right * laneOffset;
        }

        //Calculate final movement 

        Vector3 finalMovement = Vector3.zero;
        finalMovement.x = (targetPosition - transform.position).normalized.x * laneChangeSpeed;



        finalMovement.y = verticalVelocity;
        finalMovement.z = constMoveSpeed;


        controller.Move(finalMovement * Time.deltaTime);


        CalculateRotation();

    }


    private void CalculateRotation()
    {

        //Rotate
        Vector3 rotateDir = controller.velocity;

        if (rotateDir != Vector3.zero)
        {
            rotateDir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, rotateDir, turnSpeed);
        }

    }

    private void Update()
    {
        characterFSM.StateUpdate();

        CalculateFinalMovement();

        if (CharacterInput.MoveRightInput())
        {
           
            ChangeLane(true);
        }

        if(CharacterInput.MoveLeftInput())
        {
            ChangeLane(false);
        }

       

        if(Grounded())
        {
            SnapToGround();

            if(CharacterInput.GetJumpInput())
            {
                Jump();
            }

            else if(CharacterInput.GetSlideInput())
            {
                Slide();
            }
        }

        else
        {
            Fall();
        }



      




    }

    
    private bool Grounded()
    {
       

        Ray ray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);


        Debug.DrawRay(ray.origin, ray.direction * rayDistance , Color.cyan, 1.0f);

        if(Physics.Raycast(ray, rayDistance, groundLayer))
        {
            isGrounded = true;
            isJumping = false;
            isFalling = false;
            return true;
        }

        else
        {
            isGrounded = false;
            return false;
        }

     
       

       
    }

    private void SnapToGround()
    {
        characterFSM.ChangeState(characterStates[1]);
        verticalVelocity = -0.1f;
    }

    private void ChangeCharacterState(State newState)
    {
        characterFSM.ChangeState(newState);
    }




    private void ChangeLane(bool isRight)
    {
       

        //If going right add lane, if not then subtract lane
        targetLane += isRight ? 1 : -1;

        //Clamp so we only move in 3 lanes
        targetLane = Mathf.Clamp(targetLane, 0, 2);


        currentLane = targetLane;
       
    }

  
   

    private void Jump()
    {

        isJumping = true;
       
        verticalVelocity = jumpForce;

        ChangeCharacterState(characterStates[2]);
    }


    private void Slide()
    {
        isSliding = true;
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
        ChangeCharacterState(characterStates[3]);
    }

    private void StopSlide()
    {
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
        isSliding = false;
    }

    private void FastFall()
    {
        verticalVelocity = -jumpForce;
    }

    private void Fall()
    {
        isFalling = true;
        verticalVelocity -= gravity * Time.deltaTime;

        //FastFall
        if (CharacterInput.GetSlideInput())
        {
            FastFall();
        }
     

    }

}
