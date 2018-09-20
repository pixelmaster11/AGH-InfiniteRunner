#define Debug


using InputSystem;
using CharacterSystem.CharacterComponents;

/// <summary>
/// 1. This state represents the character falling state. This state is immediately after jump state when the character starts to fall down
/// 2. Do all stuff necessary when falling in this state
/// </summary>
namespace FSM.Character
{
    public class CharacterFallingState : CharacterBaseState
    {

        //Falling state
        private const string name = "CharacterFallingState";

        /// <summary>
        /// 1. Check for fast fall input
        /// 2. Player now in air so make grounded anim false
        /// </summary>
        /// <param name="Owner"></param>
        public override void Entry(BaseController Owner)
        {
            stateName = name;
            base.Entry(Owner);

            //FastFall and slide
            if (CharacterInput.SwipeDownInput())
            {
                Owner.FastFall();

                //Change from falling state to running state
                controller.ChangeState(Enums.CharacterStateType.Sliding);
               
               
            }


            controller.characterAnimator.Grounded(false);
        }


        /// <summary>
        /// 1. Check for fast fall input
        /// 2. Check for ground if falling from top of something
        /// </summary>
        public override void Update()
        {
            base.Update();     

            //Falling from air
            if (!controller.IsGrounded)
            {
                //Apply gravity to fall
                controller.ApplyGravity();

                //FastFall and slide
                if (CharacterInput.SwipeDownInput())
                {
                    controller.FastFall();

                    //Change from falling state to running state
                    controller.ChangeState(Enums.CharacterStateType.Sliding);

                }

            }

            //Reached ground
            else 
            {
                //Change from falling state to running state
                controller.ChangeState(Enums.CharacterStateType.Running);

            }


        }

        /// <summary>
        /// 1. Falling state exit
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }


     
    }


    

}

