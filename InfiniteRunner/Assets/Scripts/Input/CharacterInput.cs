using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;


/// <summary>
/// This class is responsible for handling player inputs and selecting the input method
/// All input only related stuff should be handled by this class
/// </summary>

namespace InputSystem
{
   

    public static class CharacterInput 
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

                    case InputMethod.Mouse:
                        selectedInputMethod = new MouseInputMethod();
                        break;
                }   
            }
        
        
        /// <summary>
        /// Reset inputs to reset all flags
        /// </summary>
        public static void ResetInputs()
        {
            selectedInputMethod.ResetInputs();
        }


        /// <summary>
        /// Start collecting input ervery frame
        /// </summary>
        public static void CollectInputs()
        {
            selectedInputMethod.CollectInputs();
        }


        /// <summary>
        /// Is Swipe right
        /// </summary>
        /// <returns>Swipe right?</returns>
        public static bool MoveRightInput()
         {
            
            if(selectedInputMethod.GetMovementInput() == 1)
            {
                return true;
            }

            return false;
         }


        
        /// <summary>
        /// Is Swipe left
        /// </summary>
        /// <returns>Swipe Left?</returns>
        public static bool MoveLeftInput()
        {
            if (selectedInputMethod.GetMovementInput() == -1)
            {
                return true;
            }

            return false;
        }


        //public static int GetMovementInput()
        // {
        //    return selectedInputMethod.GetMovementInput();

        // }


        
        /// <summary>
        /// Swipe Up
        /// </summary>
        /// <returns>Swipe up?</returns>
        public static bool GetJumpInput()
         {
            return selectedInputMethod.GetJumpInput();
         }

        
        /// <summary>
        /// Swipe down
        /// </summary>
        /// <returns>Swipe down?</returns>
        public static bool GetSlideInput()
         {
            return selectedInputMethod.GetSlideInput();
         }

    #endregion


     #region DEBUG

            [System.Diagnostics.Conditional("DEBUG_PLAYER")]
            private static void _LogInput(int dir, bool jump, bool slide, string message = "")
            {
                UnityEngine.Debug.Log("[D] " + dir + " [J] " + jump + " [S] " + slide + message);
            }

    #endregion



    }


}
