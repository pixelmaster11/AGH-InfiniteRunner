
using UnityEngine;
using InputSystem;
using FSM.Character;

/// <summary>
/// This class represents the base controller for all types of animals
/// </summary>
/// 

namespace CharacterSystem.CharacterComponents
{
    public abstract class BaseController : MonoBehaviour
    {

        //Has an animal fsm
        protected CharacterFSM characterFSM;


        //Has an animal animator
        public CharAnimator characterAnimator;

        //Has an animal camera
        public CharCamera characterCamera;

        //Has an animal collision system
        [SerializeField]
        protected CharacterCollision characterCollision;


        //Which animal this controller belongs to
        [SerializeField]
        protected Character character;

        //Has a character controller ref
        [SerializeField]
        protected CharacterController controller;


        

        //Enable / disable tweaking of movement data during runtime
        [SerializeField]
        protected bool debugRuntime;






        public bool IsGrounded = true;
        public bool IsJumping = false;
        public bool IsSliding = false;
        public bool IsFalling = false;
        public bool IsFastFalling = false;

        public abstract void ChangeState(Enums.CharacterStateType newStateType);
 

        
        public abstract void ConstantMove();
        public abstract void GroundCheck();
        public abstract void InputCheck();

        public abstract void ChangeLane(bool isRight);
        public abstract void Jump();
        public abstract void Slide();
        public abstract void StopSlide();


        public abstract void ApplyGravity();
        public abstract void FastFall();
        public abstract bool CheckForFall();





    }

}
