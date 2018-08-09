using InputSystem;

/// <summary>
/// 1. This is the base class for each character state.
///    A base class is needed as we need our character to move continuously inspite of being in any state. 
///    
/// 2. All states are provided as static readonly properties as each state will only have one instance and only read another state 
///    to change to a different state
///    
/// 3. Provides current and previous state access so any script can check the status of character FSM
/// </summary>


namespace FSM.Character
{
    public abstract class CharacterBaseState : ICharacterState<CharController>
    {

        //All Character states
        public static readonly ICharacterState<CharController> SLIDING_STATE = new CharacterSlidingState();
        public static readonly ICharacterState<CharController> RUNNING_STATE = new CharacterRunningState();
        public static readonly ICharacterState<CharController> JUMPING_STATE = new CharacterJumpingState();
        public static readonly ICharacterState<CharController> FALLING_STATE = new CharacterFallingState();
        public static readonly ICharacterState<CharController> DEAD_STATE = new CharacterDeadState();
        public static readonly ICharacterState<CharController> INIT_STATE = new CharacterInitState();

        //Currently and previously run state
        public static ICharacterState<CharController> currentState;
        public static ICharacterState<CharController> previousState;

        //Currently running state name
        public string stateName;


        /// <summary>
        /// Entry point hook to the state 
        /// </summary>
        /// <param name="Owner">Who owns this FSM</param>
        public virtual void Entry(CharController Owner)
        {
            _LogState("Entry");
        }


        /// <summary>
        /// 1. Update hook which ticks every frame
        /// 2. We need our player to move continuously inspite of any state, so move here
        /// 3. We can change lanes from any state currently. This is a design decision
        /// </summary>
        /// <param name="Owner">Who owns the state</param>
        public virtual void Update(CharController Owner)
        {
            CharacterInput.ResetInputs();
            CharacterInput.CollectInputs();

            //Move the character constantly in z-axis (World - forward axis)
            Owner.ConstantMove();

            //Check whether character is grounded
            Owner.Grounded();

            //Check for right input
            if (CharacterInput.MoveRightInput())
            {
                Owner.ChangeLane(true);
            }

            //Check for left input
            else if (CharacterInput.MoveLeftInput())
            {
                Owner.ChangeLane(false);
            }

        }


        /// <summary>
        /// Exit point hook of the state
        /// </summary>
        /// <param name="Owner">Who owns this FSM</param>
        public virtual void EXit(CharController Owner)
        {
            _LogState("Exit");
        }


        /// <summary>
        /// Change from current state to another state
        /// </summary>
        /// <param name="Owner">Who owns this FSM</param>
        /// <param name="newState">State we wish to change to from current state </param>
        public virtual void ChangeToState(CharController Owner, ICharacterState<CharController> newState)
        {
            //Check for null on init
            if(currentState != null)
            {
                //Exit current state
                currentState.EXit(Owner);              
            }

            //Previous state will now be current state
            previousState = currentState;

            //Current state will now change to new state
            currentState = newState;

            //Enter new state
            currentState.Entry(Owner);
            
        }


        /// <summary>
        /// Return currently running state name
        /// </summary>
        /// <returns></returns>
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
