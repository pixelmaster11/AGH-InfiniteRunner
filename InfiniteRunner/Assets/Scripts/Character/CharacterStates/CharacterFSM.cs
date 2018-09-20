
using System.Collections.Generic;
using Enums;


/// <summary>
/// This is the base class for all types of character finite state machines
/// </summary>
/// 

namespace FSM.Character
{
    public abstract class CharacterFSM
    {

        //Store all possible states that an character can have
        protected Dictionary<CharacterStateType, ICharacterState> possibleStates;

        //The current state of character
        protected ICharacterState currentState;

        //The previous state of character
        protected ICharacterState previousState;

        //Current and previous state types
        protected CharacterStateType currentStateType;
        protected CharacterStateType previousStateType;

        //Each character should fill / map their corresponding states
        public abstract void FillPossibleStates();

        //Initalize the statemachine with an initial state
        public abstract void InitializeFSM(CharacterStateType initalState);

        //Change from current state to new state using State types
        public abstract void ChangeState(CharacterStateType newStateType);

        //Update the current state continuously
        public abstract void UpdateState();

    }

}

