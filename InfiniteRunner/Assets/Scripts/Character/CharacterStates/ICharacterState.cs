
/// <summary>
/// 1. A generic interface for character FSM.
/// 
/// 2. Any character states can be created by simply implementing this interface.
/// 
/// 3. An owner can be specified so as to who will own / control this FSM. 
///    This is useful if we want maybe in future A.I characters that can use this same FSM.
///    
/// 4. Entry point, Exit point, update points are hooked to each state. 
/// 
/// 5. This FSM can change from current state to new state by using ChangeToState functionality 
/// from its controller. 

/// </summary>

using CharacterSystem.CharacterComponents;

namespace FSM.Character
{
    public interface ICharacterState
    {
        void Entry(BaseController controller);
        void Update();
        void Exit();
               
        
    }
}

