using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This interface provides a way to define different input methods.
/// This enables easy extending of different inputs such as keyboard, controller, touch, gestures
/// </summary>
/// 

namespace InputSystem
{
    public interface IInputMethod
    {
        void CollectInputs();
        void ResetInputs();
        int GetMovementInput();
        bool GetJumpInput();
        bool GetSlideInput();

    }
}

