using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for handling player inputs and selecting the input method
/// All input only related stuff should be handled by this class
/// </summary>

namespace InputSystem
{
    //Which input method
    public enum InputMethod
    {
        Keyboard,
        Mobile,
        AlternateMobile
    };

    public class CharacterInput 
    {

     #region Variables
                
      //Assign appropriate selected input method
      private static IInputMethod selectedInputMethod;                    

    #endregion

     #region Script specific methods

            /// <summary>
            /// Assign the selected input method to use in game
            /// Keyboard / Mobile 
            /// </summary>
            public static void SetInputMethod(InputMethod inputMethod)
            {
                 
                switch (inputMethod)
                {
                    case InputMethod.Keyboard:
                        selectedInputMethod = new KeyboardInputMethod();
                        break;

                    case InputMethod.Mobile:
                        selectedInputMethod = new MobileInputMethod();
                        break;

                    case InputMethod.AlternateMobile:
                        selectedInputMethod = new AlternateMobileInputMethod();
                        break;
                }
            }
        
        

        public static void ResetInputs()
        {
            selectedInputMethod.ResetInputs();
        }

        public static void CollectInputs()
        {
            selectedInputMethod.CollectInputs();
        }


         public static bool MoveRightInput()
         {
            
            if(selectedInputMethod.GetMovementInput() == 1)
            {
                return true;
            }

            return false;
         }


        public static bool MoveLeftInput()
        {
            if (selectedInputMethod.GetMovementInput() == -1)
            {
                return true;
            }

            return false;
        }

        public static int GetMovementInput()
         {
            return selectedInputMethod.GetMovementInput();

         }


         public static bool GetJumpInput()
         {
            return selectedInputMethod.GetJumpInput();
         }

         public static bool GetSlideInput()
         {
            return selectedInputMethod.GetSlideInput();
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
