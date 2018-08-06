﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class represents the input to be taken from keyboard
/// </summary>

namespace CharacterInputs
{
    public class KeyboardInputMethod : IInputMethod
    {

        private int dir;
        private bool shouldJump;
        private bool shouldSlide;

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
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                dir = -1;
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                dir = 1;
            }


            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                shouldJump = true;
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                shouldSlide = true;
            }


        }


        /// <summary>
        /// Return movement direction i.e move left or right
        /// </summary>
        /// <returns></returns>
        public int GetMovementInput()
        {

            return dir;
        }


        /// <summary>
        /// Return whether jump input or not
        /// </summary>
        /// <returns></returns>
        public bool GetJumpInput()
        {

            return shouldJump;

        }


        /// <summary>
        /// Return whether slide input or not
        /// </summary>
        /// <returns></returns>
        public bool GetSlideInput()
        {

            return shouldSlide;
        }

        #endregion

    }
}


