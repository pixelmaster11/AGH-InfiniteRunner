using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Character
{
    public class CharacterInitState : CharacterBaseState
    {
      
        private const string name = "CharacterInitState";


        private float waitTime = 3;
        private float timer = 0;

        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);
        }

        public override void Update(CharController Owner)
        {
            if(timer < waitTime)
            {
                timer += Time.deltaTime;
            }

            else
            {
                
                ChangeToState(Owner, CharacterBaseState.RUNNING_STATE);
            }
           
        }

        public override void EXit(CharController Owner)
        {
            timer = 0;
            base.EXit(Owner);
        }
    }
}

