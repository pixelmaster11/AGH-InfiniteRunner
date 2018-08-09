using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

namespace FSM.Character
{
    public abstract class CharacterBaseState : ICharacterState<CharController>
    {

        public static readonly ICharacterState<CharController> SLIDING_STATE = new CharacterSlidingState();
        public static readonly ICharacterState<CharController> RUNNING_STATE = new CharacterRunningState();
        public static readonly ICharacterState<CharController> JUMPING_STATE = new CharacterJumpingState();
        public static readonly ICharacterState<CharController> FALLING_STATE = new CharacterFallingState();
        public static readonly ICharacterState<CharController> DEAD_STATE = new CharacterDeadState();
        public static readonly ICharacterState<CharController> INIT_STATE = new CharacterInitState();


        public static ICharacterState<CharController> currentState;
        public static ICharacterState<CharController> previousState;

        public string stateName;

        public virtual void Entry(CharController Owner)
        {
            _LogState("Entry");
        }

        public virtual void Update(CharController Owner)
        {
            //Move Constantly

            Owner.CalculateFinalMovement();
            Owner.Grounded();

            if (CharacterInput.MoveRightInput())
            {
                Owner.ChangeLane(true);
            }

            else if (CharacterInput.MoveLeftInput())
            {
                Owner.ChangeLane(false);
            }



        }

        public virtual void EXit(CharController Owner)
        {
            _LogState("Exit");
        }


        public virtual void ChangeToState(CharController Owner, ICharacterState<CharController> newState)
        {
            if(currentState != null)
            {
                currentState.EXit(Owner);
               
            }

            previousState = currentState;
            currentState = newState;
            currentState.Entry(Owner);
            
        }



        public string GetStateName()
        {
            return stateName;
        }


        #region DEBUG

        [System.Diagnostics.Conditional("DEBUG_STATEMACHINE")]
        private void _LogState(string message)
        {
            UnityEngine.Debug.Log("[ Current State: "  + GetStateName() + " ] " + message);
        }

        #endregion

    }

}
