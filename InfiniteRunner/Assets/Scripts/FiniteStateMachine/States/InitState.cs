using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This state represents the initial loading state. When the game starts this will be the first state by default.
/// All iniitalization required for the game should be handled from this state
/// </summary>
namespace FSM
{
    public class InitState : State
    {

        private const string stateName = "InitState";

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

