
using UnityEngine;
using InputSystem;
using CharacterSystem.CharacterComponents;

/// <summary>
/// 1. This state represents sliding state.
/// 2. Sliding state can be reached from running state
/// 3. When slide input during jump / fall stating, the character will only quickly fall down
/// 4. Do all stuff needed during sliding in this state
/// </summary>

namespace FSM.Character
{
    public class CharacterSlidingState : CharacterBaseState
    {
        //Sliding state
        private const string name = "CharacterSlidingState";

        //How long the slide
        //TOOD: LATER CHANGE THIS TO HOW FAR SLIDE INSTEAD OF HOW LONG BASED ON TRACK SPEED
        private float waitTime = 1;
        private float timer = 0;

        /// <summary>
        /// 1. Start slide and play animation
        /// 
        /// </summary>
        /// <param name="Owner"></param>
        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);

            //Set jump animation false if sliding from fast fall while jump
            //Owner.anim.Jump(false);

            //Start sliding
            Owner.anim.Slide(true);
            Owner.Slide();
        }


        public override void Update(CharController Owner)
        {
            base.Update(Owner);

            //Check for how long to slide
            //TOOD: LATER CHANGE TO HOW FAR SLIDE BASED ON TRACK SPEED AND DISTANCE COVERED
            if (timer < waitTime)
            {
                timer += Time.deltaTime;
            }

            //Change back to running state after slide over
            else
            {
                ChangeToState(Owner, CharacterBaseState.RUNNING_STATE);
            }

            //If jump input then jump from slide        
            if(CharacterInput.SwipeUpInput())
            {
                ChangeToState(Owner, CharacterBaseState.JUMPING_STATE);
            }


            //Apply gravity so player falls from top if sliding whenver he is not on ground
            if (!Owner.IsGrounded)
            {
                ChangeToState(Owner, CharacterBaseState.FALLING_STATE);
            }


        }

        /// <summary>
        /// Exit sliding and stop slide animation and reset timer
        /// </summary>
        /// <param name="Owner"></param>
        public override void EXit(CharController Owner)
        {
            base.EXit(Owner);

            timer = 0;
            Owner.StopSlide();
            Owner.anim.Slide(false);
          
        }

     
    }

}
