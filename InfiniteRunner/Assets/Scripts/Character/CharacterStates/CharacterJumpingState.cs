
using InputSystem;

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
        /// </summary>
        /// <param name="Owner"></param>
        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);

            Owner.Jump();
            Owner.anim.Jump(true);
        }


        public override void Update(CharController Owner)
        {
            base.Update(Owner);
            
            //In air
            if(!Owner.IsGrounded)
            {
                //apply gravity to make character fall 
                Owner.ApplyGravity();

                //FastFall when swipe down
                if (CharacterInput.GetSlideInput())
                {
                    Owner.FastFall();
                }

                //If character reaches max height and starts falling
                //TODO: LATER CHANGE TO HOW FAR JUMP INSTEAD BASED ON TRACK SPEED
                if (Owner.CheckForFall())
                {
                    //Change from jump state to fall state
                    ChangeToState(Owner, CharacterBaseState.FALLING_STATE);
                }


            }         

        }

        /// <summary>
        /// If we had separate jump/ fall animation; stop jump animation here
        /// </summary>
        /// <param name="Owner"></param>
        public override void EXit(CharController Owner)
        {
            
            base.EXit(Owner);
        }

    }
}

