
using UnityEngine;
using CharacterSystem.CharacterComponents;

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

        //Cache owner controller
        public override void Entry(BaseController Owner)
        {
            stateName = name;
            base.Entry(Owner);
        }

        public override void Update()
        {
            //Wait to start running
            if(timer < waitTime)
            {
                timer += Time.deltaTime;
            }

            else
            {
                //Change from init to run state
                controller.ChangeState(Enums.CharacterStateType.Running);
            }
           
        }

        /// <summary>
        /// Reset timer 
        /// </summary>
        public override void Exit()
        {
            timer = 0;
            base.Exit();
        }
    }
}

