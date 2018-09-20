
using System.Collections.Generic;
using Enums;
using CharacterSystem.CharacterComponents;


/// <summary>
/// This class represents a finite state machine of cat character
/// </summary>
namespace FSM.Character
{

    public class CatFSM : CharacterFSM
    {
        #region State Variables

        //Cache controller to pass on to states
        BaseController controller;

        //Cat States
        private readonly ICharacterState INIT_STATE = new CharacterInitState();
        private readonly ICharacterState RUN_STATE = new CharacterRunningState();
        private readonly ICharacterState FALL_STATE = new CharacterFallingState();
        private readonly ICharacterState JUMP_STATE = new CharacterJumpingState();
        private readonly ICharacterState SLIDE_STATE = new CharacterSlidingState();
        //private readonly ICharacterState HIT_STATE = new HitState();
        private readonly ICharacterState DEAD_STATE = new CharacterDeadState();

        #endregion

        #region Initializers
        //Cache controller 
        public CatFSM(BaseController ownerController)
        {
            controller = ownerController;
        }


        /// <summary>
        /// Map all Cat states for faster retrieval
        /// </summary>
        public override void FillPossibleStates()
        {
            //State type mapping
            possibleStates = new Dictionary<CharacterStateType, ICharacterState>();
            possibleStates.Add(CharacterStateType.Init, INIT_STATE);
            possibleStates.Add(CharacterStateType.Running, RUN_STATE);
            possibleStates.Add(CharacterStateType.Falling, FALL_STATE);
            possibleStates.Add(CharacterStateType.Jumping, JUMP_STATE);
            possibleStates.Add(CharacterStateType.Sliding, SLIDE_STATE);
            possibleStates.Add(CharacterStateType.Dead, DEAD_STATE);

        }

        #endregion

        #region FSM Methods

        /// <summary>
        /// Initializes Cat FSM with ab inital state
        /// </summary>
        /// <param name="initalStateType">Begin from this state</param>
        public override void InitializeFSM(CharacterStateType initalStateType)
        {
            //Map all states
            FillPossibleStates();

            //Assign initial state
            if (possibleStates.TryGetValue(initalStateType, out currentState))
            {

                currentStateType = initalStateType;

                LogState("Initialized");

                currentState.Entry(controller);

                LogState("Entry");
            }

            else
            {
                LogStateError(initalStateType + "=> Not a valid state");
            }
        }


        /// <summary>
        /// Change state from current state to new state based on state type
        /// </summary>
        /// <param name="newStateType">State to which we want to transition to</param>
        public override void ChangeState(CharacterStateType newStateType)
        {
            ICharacterState newState;

            //Get the new state from state type dictionary mapping
            if (possibleStates.TryGetValue(newStateType, out newState))
            {
                //Current state now becomes previous state
                previousState = currentState;
                previousStateType = currentStateType;

                //Exit current state
                if (currentState != null)
                {             
                    currentState.Exit();
                    LogState("Exit");
                }

                //Transition to new state
                currentState = newState;
                currentStateType = newStateType;

                //Enter new state
                currentState.Entry(controller);
                LogState("Entry");
            }

            else
            {
                LogStateError(newStateType + "=> Not a valid state");
            }


        }


 

        /// <summary>
        /// Update the current state
        /// </summary>
        public override void UpdateState()
        {
            currentState.Update();
        }

        #endregion

        #region DEBUG

        [System.Diagnostics.Conditional("DEBUG_STATEMACHINE")]
        private void LogStateError(string message)
        {
            Utils.DebugUtils.LogError(message);
        }

        [System.Diagnostics.Conditional("DEBUG_STATEMACHINE")]
        private void LogState(string message)
        {
            Utils.DebugUtils.LogState("[Character: " + controller.gameObject.name + " ] " + " [ Current State: " + currentStateType + " ] " + message);
        }



        #endregion


    }

}