using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;


/// <summary>
/// 1. This class is responsible for handling player inputs and selecting the input method
/// 2. All input only related stuff should be handled by this class
/// 3. This class uses strategy pattern to implement cross-platform input
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
            
            if(selectedInputMethod.GetHorizontalSwipeInput() == 1)
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
            if (selectedInputMethod.GetHorizontalSwipeInput() == -1)
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
            if(selectedInputMethod.GetVerticalSwipeInput() == 1)
            {
                return true;
            }

            return false;
         }

        
        /// <summary>
        /// Swipe down
        /// </summary>
        /// <returns>Swipe down?</returns>
        public static bool SwipeDownInput()
         {
            if (selectedInputMethod.GetVerticalSwipeInput() == -1)
            {
                return true;
            }

            return false;
        }


        public static bool DoubleTapInput()
        {
            return selectedInputMethod.GetDoubleTapInput();
        }

    #endregion


     #region DEBUG

            [System.Diagnostics.Conditional("DEBUG_INPUT")]
            private static void _LogInput(int h, int v, string message = "")
            {
                Utils.DebugUtils.Log("[L-R] " + h + " [U-D] " + v + message);
            }

    #endregion



    }


}
