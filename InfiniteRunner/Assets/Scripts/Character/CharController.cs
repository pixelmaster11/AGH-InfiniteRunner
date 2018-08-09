using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Character;
using FSM;
using InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{

   
   
    public bool isJumping;  
    bool isSliding;

    
    public bool isGrounded = true; 
    bool isFalling;
    public bool isRunning = true;

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

  
    [SerializeField]
    private CharacterController controller;

   
    public CharAnimator anim;



   
    

    public void SetMovementData(CharMovementData data)
    {
        laneChangeSpeed = data.laneChangeSpeed;
        constMoveSpeed = data.constMoveSpeed;
        gravity = data.gravity;
        laneOffset = data.laneOffset;
        turnSpeed = data.turnSpeed;
        jumpForce = data.jumpForce;
        

    }


    public void CalculateFinalMovement()
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

    private void Start()
    {
        CharacterBaseState.currentState = CharacterBaseState.INIT_STATE;
        CharacterBaseState.currentState.Entry(this);
        
    }

    private void Update()
    {


        //CalculateFinalMovement();

        //if (CharacterInput.MoveRightInput())
        //{

        //    ChangeLane(true);
        //}

        //if(CharacterInput.MoveLeftInput())
        //{
        //    ChangeLane(false);
        //}



        //if(Grounded())
        //{
        //    SnapToGround();

        //    if(CharacterInput.GetJumpInput())
        //    {
        //        Jump();
        //    }

        //    else if(CharacterInput.GetSlideInput())
        //    {
        //        Slide();
        //    }
        //}

        //else
        //{
        //    Fall();
        //}


        CharacterBaseState.currentState.Update(this);
      




    }

    
    public bool Grounded()
    {
       

        Ray ray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);


        Debug.DrawRay(ray.origin, ray.direction * rayDistance , Color.cyan, 1.0f);

        if(Physics.Raycast(ray, rayDistance, groundLayer))
        {
            isGrounded = true;
            isJumping = false;
            //isFalling = false;
            //anim.Jump(false);
            return true;
        }

        else
        {
            isGrounded = false;
            return false;
        }

     
       

       
    }

    public void SnapToGround()
    {
        
        verticalVelocity = -0.1f;
    }

 



    public void ChangeLane(bool isRight)
    {
       

        //If going right add lane, if not then subtract lane
        targetLane += isRight ? 1 : -1;

        //Clamp so we only move in 3 lanes
        targetLane = Mathf.Clamp(targetLane, 0, 2);


        currentLane = targetLane;
       
    }

  
   

    public void Jump()
    {

        if(!isSliding)
        {
            isJumping = true;

            verticalVelocity = jumpForce;

           // anim.Jump(true);
        }
       
       
    }


    public void Slide()
    {
        isSliding = true;
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
        //anim.Slide(true);
        //StartCoroutine(IEStopSlide());
    }

    private IEnumerator IEStopSlide()
    {
        var wait = new WaitForSeconds(1);

        yield return wait;

        StopSlide();
    }

    public void StopSlide()
    {
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
        isSliding = false;
        //anim.Slide(false);
        //StopCoroutine(IEStopSlide());
    }

    private void FastFall()
    {
        verticalVelocity = -jumpForce;
    }

    public void Fall()
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
