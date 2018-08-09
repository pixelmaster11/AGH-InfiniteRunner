using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

