using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterBehaviours
{
    public interface IFallBehaviour
    {
        void ApplyGravity();

        void FastFall();

        bool CheckForFall();

    }
}

