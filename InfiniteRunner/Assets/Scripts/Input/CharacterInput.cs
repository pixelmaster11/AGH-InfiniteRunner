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
            public static void SetInputMethod(InputType inputMethod)
            {
                 
                switch (inputMethod)
                {
                    case InputType.Keyboard:
                        selectedInputMethod = new KeyboardInputMethod();
                        break;

                    case InputType.Mobile:
                        selectedInputMethod = new MobileInputMethod();
                        break;

                    case InputType.AlternateMobile:
                        selectedInputMethod = new AlternateMobileInputMethod();
                        break;

                    case InputType.Mouse:
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
        public static bool SwipeRightInput()
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
        public static bool SwipeLeftInput()
        {
            if (selectedInputMethod.GetMovementInput() == -1)
            {
                return true;
            }

            return false;
        }


       


        
        /// <summary>
        /// Swipe Up
        /// </summary>
        /// <returns>Swipe up?</returns>
        public static bool SwipeUpInput()
         {
            return selectedInputMethod.GetSwipeUpInput();
         }

        
        /// <summary>
        /// Swipe down
        /// </summary>
        /// <returns>Swipe down?</returns>
        public static bool SwipeDownInput()
         {
            return selectedInputMethod.GetSwipeDownInput();
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
