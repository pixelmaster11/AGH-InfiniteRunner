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
    public class GamePlayState : IState
    {

        private const string name = "GamePlayState";

        public void Entry()
        {

        }

        public void Exit()
        {

        }

        public string GetStateName()
        {
            return name;
        }

        public void Update()
        {

        }
    }
}

