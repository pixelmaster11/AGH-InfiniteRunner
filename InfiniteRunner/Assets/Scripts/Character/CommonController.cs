
using UnityEngine;
using InputSystem;
using Enums;
using FSM.Character;

namespace CharacterSystem.CharacterComponents
{
    public class CommonController : BaseController
    {

        //Keep track of which lane the character is in
        [Range(0,2)]
        public int currentLane = 1;
                
        [Range(0,2)]
        public int targetLane = 2;



        [SerializeField]
        protected CharacterStateType initialStateType;

        [SerializeField]
        protected InputType inputType;



        //Constant run speed
        private float constMoveSpeed;
  
        //How much to turn while changing lanes
        private float turnSpeed = 0.05f;

        [SerializeField]
        private float laneOffset = 6f;

        [SerializeField]
        private float laneChangeSpeed = 20;


        //What should be detected as ground
        [SerializeField]
        LayerMask groundLayer;

        //How long should ray be cast to detect ground
        [SerializeField]
        float rayDistance;


        //How high the character jumps
        private float jumpForce;

        //Y velocity
        private float verticalVelocity;

        //How much force applied to pull down character
        private float gravity;

        private Vector3 targetPosition;


      


        #region MonoBehaviour Methods

        private void Start()
        {
            SetMoveStats();
            SetAnimalFSM();

            //characterAnimator.SetHashes(animal.animalType);

        }

        private void Update()
        {
            characterFSM.UpdateState();

#if UNITY_EDITOR

            if (debugRuntime)
                SetMoveStats();
#endif

            CharacterInput.SetInputMethod(inputType);

            
        }

        #endregion


        private void SetMoveStats()
        {
            constMoveSpeed = character.movementStats.minSpeed;

            jumpForce = character.movementStats.jumpForce;
            gravity = character.movementStats.gravity;


        }


        private void SetAnimalFSM()
        {
            switch(character.characterType)
            {
                case CharacterType.Cat:
                    characterFSM = new CatFSM(this);
                    break;

            }

            characterFSM.InitializeFSM(initialStateType);
        }


        public override void ChangeState(CharacterStateType newStateType)
        {
            characterFSM.ChangeState(newStateType);
        }




        /// <summary>
        /// Constantly move the player in forward direction
        /// </summary>
        public override void ConstantMove()
        {

            //Calculate target position to move 
            //We want to move constantly in forward direction i.e in the Z-Axis
            targetPosition = transform.position.z * Vector3.forward;

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


            //finalMovement.x = verticalVelocity.x;



            //Y movement will be the character's current Y-velocity
            finalMovement.y = verticalVelocity;

            //Z movement will be Constant movement in Z-Axis
            finalMovement.z = constMoveSpeed;

            //Move the character 
            controller.Move(finalMovement * Time.deltaTime);

            CalculateRotation();

        }


        /// <summary>
        /// Move character to another lane
        /// </summary>
        /// <param name="isRight"> Whether the target lane to move to is Right lane?</param>
        public override void ChangeLane(bool isRight)
        {

            //If going right add lane, if not then subtract lane
            targetLane += isRight ? 1 : -1;

            //Clamp so we only move in 3 lanes
            targetLane = Mathf.Clamp(targetLane, 0, 2);

            currentLane = targetLane;

        }



        /// <summary>
        /// Check whether player is grounded or not
        /// </summary>
        public override void GroundCheck()
        {


            //Cast ray from bottom of char controller with some little offset
            Ray ray = new Ray(new Vector3(controller.bounds.center.x,
                (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);

#if UNITY_EDITOR

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.cyan, 1.0f);

#endif
            //TODO: Collision from collision class
            if (/*animalCollision.GroundCollision()*/ Physics.Raycast(ray, rayDistance, groundLayer))
            {
                IsGrounded = true;
                IsJumping = false;
                IsFalling = false;
                IsFastFalling = false;
                SnapToGround();
            }

            else
            {
                IsGrounded = false;
            }
        

        }


        public override void InputCheck()
        {
            CharacterInput.ResetInputs();
            CharacterInput.CollectInputs();
        }

        /// <summary>
        /// Keep character snapped to the ground when grounded
        /// </summary>
        public void SnapToGround()
        {

         
            verticalVelocity = -0.1f;

        }


       




        /// <summary>
        /// Jump the Character by changing its Y-velocity
        /// </summary>
        public override void Jump()
        {
          
           verticalVelocity = jumpForce;
           
        }


        /// <summary>
        /// Make character slide.
        /// The reality is character only slides down in animation clips, here we only set its collider size accordingly
        /// </summary>
        public override void Slide()
        {
            IsSliding = true;
            controller.height /= 2;
            controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
        }


        /// <summary>
        /// Stop character from sliding
        /// Again we only set back the original collider size
        /// </summary>
        public override void StopSlide()
        {
            IsSliding = false;
            controller.height *= 2;
            controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
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
        /// Apply gravity when character is in air to make the character fall down
        /// </summary>
        public override void ApplyGravity()
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }


        /// <summary>
        /// Fast falling ability to make character fall down quickly if swiped down when in air
        /// </summary>
        public override void FastFall()
        {
            IsFastFalling = true;
            verticalVelocity = -jumpForce;
        }


        /// <summary>
        /// Check whether the charcter has reached maximum height of jump and now has started to fall down
        /// </summary>
        /// <returns>Returs if character is falling down or not</returns>
        public override bool CheckForFall()
        {
            if (verticalVelocity < -0.2f)
            {
                IsFalling = true;

                return true;
            }


            return false;
        }



 
       
 
    }

}
