
using UnityEngine;

/// <summary>
/// 1. This state represents the initialization state of character
/// 2. Do all initialization stuff here
/// </summary>
namespace FSM.Character
{
    public class CharacterInitState : CharacterBaseState
    {
      
        //Initialization state
        private const string name = "CharacterInitState";

        //How long to wait before begin run
        private float waitTime = 3;
        private float timer = 0;

        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);
        }

        public override void Update(CharController Owner)
        {
            //Wait to start running
            if(timer < waitTime)
            {
                timer += Time.deltaTime;
            }

            else
            {
                //Change from init to run state
                ChangeToState(Owner, CharacterBaseState.RUNNING_STATE);
            }
           
        }

        /// <summary>
        /// Reset timer 
        /// </summary>
        /// <param name="Owner"></param>
        public override void EXit(CharController Owner)
        {
            timer = 0;
            base.EXit(Owner);
        }
    }
}

