using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for handling player inputs and selecting the input method
/// All input only related stuff should be handled by this class
/// </summary>

namespace CharacterInputs
{

    public class CharacterInput : MonoBehaviour
    {

     #region Variables

            //Which input method
            public enum InputMethods
            {
                Keyboard,
                Mobile
            };
            public InputMethods inputMethod;

            //Assign appropriate selected input method
            private IInputMethod selectedInputMethod;

    #endregion

     #region Script specific methods

            /// <summary>
            /// Assign the selected input method to use in game
            /// Keyboard / Mobile 
            /// </summary>
            private void SetInputMethod()
            {
                switch (inputMethod)
                {
                    case InputMethods.Keyboard:
                        selectedInputMethod = new KeyboardInputMethod();
                        break;

                    case InputMethods.Mobile:
                        selectedInputMethod = new MobileInputMethod();
                        break;
                }
            }

    #endregion

     #region Monobehaviour Methods

            private void Start()
            {
                SetInputMethod();
            }

            //Gather inputs each frame
            private void Update()
            {
                selectedInputMethod.CollectInputs();

                int dir = selectedInputMethod.GetMovementInput();
                bool jump = selectedInputMethod.GetJumpInput();
                bool slide = selectedInputMethod.GetSlideInput();

                _LogInput(dir, jump, slide);
                selectedInputMethod.ResetInputs();
            }

    #endregion

     #region DEBUG

            [System.Diagnostics.Conditional("DEBUG_PLAYER")]
            private void _LogInput(int dir, bool jump, bool slide, string message = "")
            {
                UnityEngine.Debug.Log("[D] " + dir + " [J] " + jump + " [S] " + slide + message);
            }

    #endregion



    }


}
