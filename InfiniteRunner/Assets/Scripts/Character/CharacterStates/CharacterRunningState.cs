using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

namespace FSM.Character
{
    public class CharacterRunningState : CharacterBaseState
    {
        

        private const string name = "CharacterRunningState";


       

        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);
        }

        public override void Update(CharController Owner)
        {
            base.Update(Owner);    

            if(Owner.isGrounded)
            {
                Owner.SnapToGround();

                if(CharacterInput.GetJumpInput())
                {
                    ChangeToState(Owner, CharacterBaseState.JUMPING_STATE);
                }

                else if (CharacterInput.GetSlideInput())
                {
                    ChangeToState(Owner, CharacterBaseState.SLIDING_STATE);
                }
            }



        }

        public override void EXit(CharController Owner)
        {
            base.EXit(Owner);
        }



    }
}

