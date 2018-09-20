
using InputSystem;
using CharacterSystem.CharacterComponents;

/// <summary>
/// 1. This state represents the character is in jumping state.
/// 2. This state can be reached from running state or sliding state
/// 3. Do things related to when character jumps from this state
/// </summary>
/// 
namespace FSM.Character
{
    public class CharacterJumpingState : CharacterBaseState
    {
        
        //Jump state name
        private const string name = "CharacterJumpingState";

      
        /// <summary>
        /// 1. Entered jump state, so jump
        /// 2. Play jump animaion
        /// 3. Stop running animation by making grounded anim false, as player now in air
        /// </summary>
        /// <param name="Owner"></param>
        public override void Entry(BaseController Owner)
        {
            stateName = name;
            base.Entry(Owner);

            controller.Jump();
            controller.characterAnimator.Jump(true);
            controller.characterAnimator.Grounded(false);

        }



        public override void Update()
        {
            base.Update();

            //In air
            if (!controller.IsGrounded)
            {
                //apply gravity to make character fall 
                controller.ApplyGravity();

                //FastFall when swipe down
                if (CharacterInput.SwipeDownInput())
                {
                    controller.FastFall();

                    //Change from jump state to slide state on fast fall
                    controller.ChangeState(Enums.CharacterStateType.Sliding);


                }

                //If character reaches max height and starts falling
                //TODO: LATER CHANGE TO HOW FAR JUMP INSTEAD BASED ON TRACK SPEED
                else if (controller.CheckForFall())
                {
                    //Change from jump state to fall state
                    controller.ChangeState(Enums.CharacterStateType.Falling);
                }

            }

          

         

        }

        /// <summary>
        /// Stop jump animation here// Will auto start fall animation from AC
        /// </summary>
        public override void Exit()
        {
            
            controller.characterAnimator.Jump(false);
            
            base.Exit();
        }

    }
}

