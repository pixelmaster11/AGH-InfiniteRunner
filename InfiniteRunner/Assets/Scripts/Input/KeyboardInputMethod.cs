
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class represents the input to be taken from keyboard
/// </summary>

namespace InputSystem
{
    public class KeyboardInputMethod : IInputMethod
    {

        private  int dir;
        private  bool shouldJump;
        private  bool shouldSlide;

        #region Interface methods   

        /// <summary>
        /// Reset all inputs
        /// </summary>

        public void ResetInputs()
        {
            dir = 0;
            shouldJump = false;
            shouldSlide = false;
        }


        /// <summary>
        /// Collect inputs from different keys
        /// </summary>
        public void CollectInputs()
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                 dir = -1;
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                dir = 1;
            }

            else
            {
                dir = 0;
            }


            if (Input.GetKey(KeyCode.UpArrow))
            {

                shouldJump = true;
            }

            else
            {
                shouldJump = false;
            }


            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                shouldSlide = true;
            }

            else
            {
                shouldSlide = false;
            }

        }


        /// <summary>
        /// Return swipe direction i.e left or right
        /// </summary>
        /// <returns></returns>
        public int GetMovementInput()
        {
           

            return dir;
        }


        /// <summary>
        /// Return whether swiped up
        /// </summary>
        /// <returns></returns>
        public bool GetSwipeUpInput()
        {
           

            return shouldJump;

        }


        /// <summary>
        /// Return whether swiped down
        /// </summary>
        /// <returns></returns>
        public bool GetSwipeDownInput()
        {
           

            return shouldSlide;
        }

        #endregion

    }
}


