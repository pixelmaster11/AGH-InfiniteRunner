﻿
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
        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);

            Owner.Jump();
            Owner.anim.Jump(true);
            Owner.anim.Grounded(false);
        }



        public override void Update(CharController Owner)
        {
            base.Update(Owner);

            //In air
            if (!Owner.IsGrounded)
            {
                //apply gravity to make character fall 
                Owner.ApplyGravity();

                //FastFall when swipe down
                if (CharacterInput.SwipeDownInput())
                {
                    Owner.FastFall();

                    //Change from jump state to slide state on fast fall
                    ChangeToState(Owner, CharacterBaseState.SLIDING_STATE);


                }

                //If character reaches max height and starts falling
                //TODO: LATER CHANGE TO HOW FAR JUMP INSTEAD BASED ON TRACK SPEED
                else if (Owner.CheckForFall())
                {
                    //Change from jump state to fall state
                    ChangeToState(Owner, CharacterBaseState.FALLING_STATE);
                }

            }

          

         

        }

        /// <summary>
        /// Stop jump animation here// Will auto start fall animation from AC
        /// </summary>
        /// <param name="Owner"></param>
        public override void EXit(CharController Owner)
        {
            
            Owner.anim.Jump(false);
            
            base.EXit(Owner);
        }

    }
}

