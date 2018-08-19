using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem.CharacterComponents;

namespace FSM.Character
{
    public class CharacterDeadState : CharacterBaseState
    {
       
        private const string name = "CharacterDeadState";




        public override void Entry(CharController Owner)
        {
            stateName = name;
        }

        public override void Update(CharController Owner)
        {
            base.Update(Owner);
        }

        public override void EXit(CharController Owner)
        {

        }

    }
}

