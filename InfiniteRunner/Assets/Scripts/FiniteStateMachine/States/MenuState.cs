using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This state represents the game currently being in the MainMenu. 
/// This state is right after the completion of initialization state and before the game play state
/// All UI navigation such as settings, play etc should be handled from this state
/// </summary>
namespace FSM
{
    public class MenuState : IState
    {
        private const string name = "MenuState";

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

