using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

namespace FSM.Character
{
    public class CharacterJumpingState : CharacterBaseState
    {
        

        private const string name = "CharacterJumpingState";


      

        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);
            Owner.anim.Jump(true);
            Owner.Jump();
        }

        public override void Update(CharController Owner)
        {
            base.Update(Owner);

            if(!Owner.isGrounded)
            {
                ChangeToState(Owner, CharacterBaseState.FALLING_STATE);
            }

          



        }

        public override void EXit(CharController Owner)
        {
            
            base.EXit(Owner);
        }

    }
}

