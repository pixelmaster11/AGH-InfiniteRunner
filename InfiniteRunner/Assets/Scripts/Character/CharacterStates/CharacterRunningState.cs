using InputSystem;
using CharacterSystem.CharacterComponents;

/// <summary>
/// 1. This state represents the character is in running state or grounded state
/// 2. You can jump / slide / change lanes (by default) fromt this state
/// </summary>

namespace FSM.Character
{
    public class CharacterRunningState : CharacterBaseState
    {
        //This state name
        private const string name = "CharacterRunningState";    

        public override void Entry(BaseController Owner)
        {
            stateName = name;
            base.Entry(Owner);

            //Grounded, to play run animation
            Owner.characterAnimator.Grounded(true);
        }

        public override void Update()
        {
            base.Update();    

            //Check for ground
            if(controller.IsGrounded)
            {
                //Snap back to ground while grounded 
                //controller.SnapToGround();

                //Jump
                if(CharacterInput.SwipeUpInput())
                {
                    //Change from this state to jump state
                    controller.ChangeState(Enums.CharacterStateType.Jumping);
                   
                }

                //Slide
                else if (CharacterInput.SwipeDownInput())
                {
                    //Change from this state to slide state
                    controller.ChangeState(Enums.CharacterStateType.Sliding);

                }
            }

            //If falling from top of something while running
            else
            {
                //Change from this state to fall state
                controller.ChangeState(Enums.CharacterStateType.Falling);
            }



        }

        public override void Exit()
        {
           
            base.Exit();
        }



    }
}

