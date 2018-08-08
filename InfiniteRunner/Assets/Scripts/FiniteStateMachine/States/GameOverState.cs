using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This state represents when the game is finished that is successfull completion of a mission or player dead. 
/// All stuff that showed be executed or displayed when the game is over should be handled from this state
/// </summary>

namespace FSM
{
    public class GameOverState : State
    {

        private const string stateName = "GameOverState";

        public override void Entry()
        {
            isStateActive = true;
        }

        public override void StateUpdate()
        {
            isStateActive = true;
        }

        public override void Exit()
        {
            isStateActive = false;
        }


        public override string GetStateName()
        {
            return stateName;
        }
    }
}

