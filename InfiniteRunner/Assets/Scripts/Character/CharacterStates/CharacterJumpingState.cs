using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Character
{
    public class CharacterJumpingState : State
    {
        [SerializeField]
        private CharController controller;

        private const string stateName = "CharacterJumpingState";

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

