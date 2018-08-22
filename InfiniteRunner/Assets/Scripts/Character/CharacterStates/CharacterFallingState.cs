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
        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);

            //FastFall and slide
            if (CharacterInput.SwipeDownInput())
            {
                Owner.FastFall();

                //Change from falling state to running state
                ChangeToState(Owner, CharacterBaseState.SLIDING_STATE);
            }


            Owner.anim.Grounded(false);
        }


        /// <summary>
        /// 1. Check for fast fall input
        /// 2. Check for ground if falling from top of something
        /// </summary>
        /// <param name="Owner"></param>
        public override void Update(CharController Owner)
        {
            base.Update(Owner);     

            //Falling from air
            if (!Owner.IsGrounded)
            {
                //Apply gravity to fall
                Owner.ApplyGravity();

                //FastFall and slide
                if (CharacterInput.SwipeDownInput())
                {
                    Owner.FastFall();

                    //Change from falling state to running state
                    ChangeToState(Owner, CharacterBaseState.SLIDING_STATE);
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
        /// 
        /// 
        /// </summary>
        /// <param name="Owner"></param>
        public override void EXit(CharController Owner)
        {
            //Owner.anim.Jump(false);
            base.EXit(Owner);
        }


        #if Debug
        private void _Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
        #endif
    }


    

}

