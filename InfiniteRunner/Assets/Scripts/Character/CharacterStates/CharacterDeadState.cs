using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem.CharacterComponents;

/// <summary>
/// 1. This state represents that the character is dead
/// </summary>
namespace FSM.Character
{
    public class CharacterDeadState : CharacterBaseState
    {
       
        private const string name = "CharacterDeadState";

        public override void Entry(BaseController Owner)
        {
            base.Entry(Owner);
            stateName = name;
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            base.Exit();
        }

    }
}

