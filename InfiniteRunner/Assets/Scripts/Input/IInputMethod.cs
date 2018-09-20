using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. This interface provides a way to define different input methods.
/// 2. This enables easy extending of different inputs such as keyboard, controller, touch, gestures
/// 3. Uses strategy pattern to implement
/// </summary>
/// 

namespace InputSystem
{
    public interface IInputMethod
    {
        void CollectInputs();
        void ResetInputs();

        int GetHorizontalSwipeInput();
        int GetVerticalSwipeInput();
        bool GetDoubleTapInput();

    }
}

