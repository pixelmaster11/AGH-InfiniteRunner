using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This state represents the initial loading state. When the game starts this will be the first state by default.
/// All iniitalization required for the game should be handled from this state
/// </summary>
namespace FSM
{
    public class InitState : IState
    {

        private const string name = "InitState";

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

