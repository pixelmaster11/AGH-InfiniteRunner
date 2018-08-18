
using UnityEngine;
using CharacterBehaviours;

/// <summary>
/// 1. This class is responsible for all character movement and physics and sets specific character data on initialize
/// 2. It keeps track of what lane the character currently is, applies all behaviours like jump, slide ,etc
/// 3. Checks for ground, applies gravity, fast fall, etc
/// 4. All the functionality in here is controlled by character FSM. 
/// </summary>

[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour, IRunBehaviour, IJumpBehaviour, IFallBehaviour, ISlideBehaviour
{

    #region Character Movement Variables

    //Keep track of which lane the character is in
    private int currentLane = 1;
    private int targetLane = 1;

    //Constant run speed
    private float constMoveSpeed = 5;

    //How quickly character moves to another lane
    private float laneChangeSpeed;

    //Lane offset to change depending upon assets
    private float laneOffset;

    //How much to turn while changing lanes
    private float turnSpeed;

    //How high the character jumps
    private float jumpForce;

    //Y velocity
    private float verticalVelocity;

    //How much force applied to pull down character
    private float gravity;

    #endregion

    #region Character Movement Bools


    public bool IsJumping;


    public bool IsGrounded;
    public bool IsSliding;
    public bool IsRunning;
    public bool IsFalling;

    #endregion

    #region Character Serialized Variables

    //What should be detected as ground
    [SerializeField]
    LayerMask groundLayer;

    //How long should ray be cast to detect ground
    [SerializeField]
    float rayDistance;

    //Character controller attached 
    [SerializeField]
    private CharacterController controller; 
    
    //Animator reference
    public CharAnimator anim;



    #endregion


 

    #region Script Specific Methods

    /// <summary>
    /// 1. Sets the movement data of character
    /// 2. This can be used in a way when multiple characters have different movement data.
    ///    For example - Cat can have high const run speed or pengu can have high lane change speed
    /// </summary>
    /// <param name="data"></param>
    public void SetMovementData(CharMovementData data)
    {
        laneChangeSpeed = data.laneChangeSpeed;
        constMoveSpeed = data.constMoveSpeed;
        gravity = data.gravity;
        laneOffset = data.laneOffset;
        turnSpeed = data.turnSpeed;
        jumpForce = data.jumpForce;


    }



    /// <summary>
    /// Rotate character when switching lanes
    /// </summary>
    private void CalculateRotation()
    {

        //Get unit velocity vector from controller
        Vector3 rotateDir = controller.velocity;

        
        if (rotateDir != Vector3.zero)
        {
            //Ground heading
            rotateDir.y = 0;

            //Lerp for smooth rotation
            transform.forward = Vector3.Lerp(transform.forward, rotateDir, turnSpeed);
        }

    }


    /// <summary>
    /// Check whether player is grounded or not
    /// </summary>
    public void Grounded()
    {

        //Cast ray from bottom of char controller with some little offset
        Ray ray = new Ray(new Vector3(controller.bounds.center.x,
            (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);

        #if UNITY_EDITOR

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.cyan, 1.0f);

        #endif

        //If ray hits ground
        if (Physics.Raycast(ray, rayDistance, groundLayer))
        {
            //Character is ground, not jumping or falling
            IsGrounded = true;
           

        }

        //No ground hit
        else
        {
            //Character is still in air
            IsGrounded = false;
        }

    }

    /// <summary>
    /// Keep character snapped to the ground when grounded
    /// </summary>
    public void SnapToGround()
    {
        IsJumping = false;
        IsFalling = false;
        verticalVelocity = -0.1f;
       
    }

    #endregion

    #region RunBehaviour Methods

    /// <summary>
    /// Constantly move the player in forward direction
    /// </summary>
    public void ConstantMove()
    {

        //Calculate target position to move 
        //We want to move constantly in forward direction i.e in the Z-Axis
        Vector3 targetPosition = transform.position.z * Vector3.forward;

        //If left lane is the target lane
        if (targetLane == 0)
        {
            //Move left with some offset
            targetPosition += Vector3.left * laneOffset;
        }

        //If right lane is target lane
        else if (targetLane == 2)
        {
            //Move right with some offset
            targetPosition += Vector3.right * laneOffset;
        }

        //Calculate final movement 
        Vector3 finalMovement = Vector3.zero;

        //Calculate how much to move left or right based on lane change speed
        //We normalize the direction in X-Axis to get unit value for X-Axis and then we can control lane change speed
        finalMovement.x = (targetPosition - transform.position).normalized.x * laneChangeSpeed;

        //Y movement will be the character's current Y-velocity
        finalMovement.y = verticalVelocity;

        //Z movement will be Constant movement in Z-Axis
        finalMovement.z = constMoveSpeed;

        //Move the character 
        controller.Move(finalMovement * Time.deltaTime);

        //Rotate as per lane
        CalculateRotation();

    }



    /// <summary>
    /// Move character to another lane
    /// </summary>
    /// <param name="isRight"> Whether the target lane to move to is Right lane?</param>
    public void ChangeLane(bool isRight)
    {

        //If going right add lane, if not then subtract lane
        targetLane += isRight ? 1 : -1;

        //Clamp so we only move in 3 lanes
        targetLane = Mathf.Clamp(targetLane, 0, 2);

        currentLane = targetLane;

    }

    #endregion

    #region JumpBehaviour Methods

    /// <summary>
    /// Jump the Character by changing its Y-velocity
    /// </summary>
    public void Jump()
    {
        IsJumping = true;
        verticalVelocity = jumpForce;
    }


   

    #endregion

    #region FallBehaviour Methods

    /// <summary>
    /// Apply gravity when character is in air to make the character fall down
    /// </summary>
    public void ApplyGravity()
    {       
        verticalVelocity -= gravity * Time.deltaTime;
    }


    /// <summary>
    /// Fast falling ability to make character fall down quickly if swiped down when in air
    /// </summary>
    public void FastFall()
    {
              
        verticalVelocity = -jumpForce;
    }


    /// <summary>
    /// Check whether the charcter has reached maximum height of jump and now has started to fall down
    /// </summary>
    /// <returns>Returs if character is falling down or not</returns>
    public bool CheckForFall()
    {
        if (verticalVelocity < -0.2f)
        {
            IsFalling = true;
            return true;
        }

        return false;
    }

    #endregion

    #region SlideBehaviour Methods

    /// <summary>
    /// Make character slide.
    /// The reality is character only slides down in animation clips, here we only set its collider size accordingly
    /// </summary>
    public void Slide()
    {
        IsSliding = true;
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
    }

   
    /// <summary>
    /// Stop character from sliding
    /// Again we only set back the original collider size
    /// </summary>
    public void StopSlide()
    {
        IsSliding = false;
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }

    #endregion

}
