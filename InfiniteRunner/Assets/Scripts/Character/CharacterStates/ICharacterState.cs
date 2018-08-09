
/// <summary>
/// 1. A generic interface for character FSM.
/// 
/// 2. Any character states can be created by simply implementing this interface.
/// 
/// 3. A generic owner can be specified so as to who will own this FSM. 
///    This is useful if we want maybe in future A.I characters that can use this same FSM.
///    
/// 4. Entry point, Exit point, update points are hooked to each state. 
/// 
/// 5. This FSM can change from current state to new state by using ChangeToState functionality. 
///    This functionality properly suits infinite runner as we need states to determine when they start and end,
///    rather than having a separate state machine controller as we have for global game FSM.
/// </summary>

namespace FSM.Character
{
    public interface ICharacterState<T>
    {
        void Entry(T Owner);
        void Update(T Owner);
        void EXit(T Owner);
        void ChangeToState(T Owner, ICharacterState<T> newState);           
        
    }
}

