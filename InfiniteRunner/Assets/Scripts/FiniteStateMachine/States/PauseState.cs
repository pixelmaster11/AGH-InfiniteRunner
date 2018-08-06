using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This state represents when the game is paused. Usually only happens during the game play. Thus this state will be after game play stae.
/// All stuff related that should happen when the game is paused should be handled from this state.
/// </summary>

namespace FSM
{
    public class PauseState : IState
    {

        private const string name = "PauseState";

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

