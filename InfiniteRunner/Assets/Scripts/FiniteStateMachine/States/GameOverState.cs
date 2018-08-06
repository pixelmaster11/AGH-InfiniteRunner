using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This state represents when the game is finished that is successfull completion of a mission or player dead. 
/// All stuff that showed be executed or displayed when the game is over should be handled from this state
/// </summary>

namespace FSM
{
    public class GameOverState : IState
    {

        private const string name = "GameOverState";

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

