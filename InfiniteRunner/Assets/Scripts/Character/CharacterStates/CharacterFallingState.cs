using InputSystem;

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

        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);
        }


        public override void Update(CharController Owner)
        {
            base.Update(Owner);

            //Falling from air
            if (!Owner.IsGrounded)
            {
                //Apply gravity to fall
                Owner.ApplyGravity();

                //FastFall
                if (CharacterInput.GetSlideInput())
                {
                    Owner.FastFall();
                }
            }

            //Reached ground
            else
            {
                //Change from falling state to running state
                ChangeToState(Owner, CharacterBaseState.RUNNING_STATE);
            }



        }

        /// <summary>
        /// 1. Falling state exit
        /// 2. Currently only jump animation, so stop that animation
        /// 3. If we have different falling animation; play that at entry and stop that here
        /// </summary>
        /// <param name="Owner"></param>
        public override void EXit(CharController Owner)
        {
            Owner.anim.Jump(false);
            base.EXit(Owner);
        }


    }
}

