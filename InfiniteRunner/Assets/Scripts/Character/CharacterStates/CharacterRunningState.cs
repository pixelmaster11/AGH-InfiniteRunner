using InputSystem;

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

        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);
        }

        public override void Update(CharController Owner)
        {
            base.Update(Owner);    

            //Check for ground
            if(Owner.IsGrounded)
            {
                //Snap back to ground while grounded 
                Owner.SnapToGround();

                //Jump
                if(CharacterInput.GetJumpInput())
                {
                    //Change from this state to jump state
                    ChangeToState(Owner, CharacterBaseState.JUMPING_STATE);
                }

                //Slide
                else if (CharacterInput.GetSlideInput())
                {
                    //Change from this state to slide state
                    ChangeToState(Owner, CharacterBaseState.SLIDING_STATE);
                }
            }

            //If falling from top of something while running
            else
            {
                //Change from this state to fall state
                ChangeToState(Owner, CharacterBaseState.FALLING_STATE);
            }



        }

        public override void EXit(CharController Owner)
        {
            base.EXit(Owner);
        }



    }
}

