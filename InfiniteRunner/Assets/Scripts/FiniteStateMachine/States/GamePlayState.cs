using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This state represents the actual game playing state. The player is playing the game in this state
/// This state is after the menu state
/// All in-game or game play related stuff should be handled from this state
/// </summary>

namespace FSM
{
    public class GamePlayState : State
    {

        private const string stateName = "GamePlayState";

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

