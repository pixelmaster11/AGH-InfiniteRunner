
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
        public override void Entry(BaseController Owner)
        {
            stateName = name;
            base.Entry(Owner);

            //Start sliding
            controller.characterAnimator.Slide(true);
            controller.Slide();
        }


        public override void Update()
        {
            base.Update();

            //Check for how long to slide
            //TOOD: LATER CHANGE TO HOW FAR SLIDE BASED ON TRACK SPEED AND DISTANCE COVERED
            if (timer < waitTime)
            {
                timer += Time.deltaTime;
            }

            //Change back to running state after slide over
            else
            {
                controller.ChangeState(Enums.CharacterStateType.Running);
              
            }

            //If jump input then jump from slide        
            if(CharacterInput.SwipeUpInput())
            {
                controller.ChangeState(Enums.CharacterStateType.Jumping);
            }


            //Apply gravity so player falls from top if sliding whenver he is not on ground
            //Check for fast falling if we want to continue to slide from fast fall
            if (!controller.IsGrounded && !controller.IsFastFalling)
            {
                controller.ChangeState(Enums.CharacterStateType.Falling);
            
            }


        }

        /// <summary>
        /// Exit sliding and stop slide animation and reset timer
        /// </summary>
        public override void Exit()
        {
            base.Exit();

            timer = 0;
            controller.StopSlide();
            controller.characterAnimator.Slide(false);
          
        }

     
    }

}
