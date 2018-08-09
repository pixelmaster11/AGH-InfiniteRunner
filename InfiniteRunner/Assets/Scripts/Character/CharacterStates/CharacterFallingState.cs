using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Character
{
    public class CharacterFallingState : CharacterBaseState
    {


        private const string name = "CharacterFallingState";




        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);
        }

        public override void Update(CharController Owner)
        {
            base.Update(Owner);

            if (!Owner.isGrounded)
            {
                Owner.Fall();
            }

            else
            {
                ChangeToState(Owner, CharacterBaseState.RUNNING_STATE);
            }



        }

        public override void EXit(CharController Owner)
        {
            Owner.anim.Jump(false);
            base.EXit(Owner);
        }


    }
}

