
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class handles the input from keyboard
/// </summary>

namespace InputSystem
{
    public class KeyboardInputMethod : IInputMethod
    {

        private int horizontal;
        private int vertical;

        private bool doubleTap;

        private float timer;
        private float tapTimer = 0.25f;
        private int tapCount = 0;

        #region Interface methods   

        /// <summary>
        /// Reset all inputs
        /// </summary>

        public void ResetInputs()
        {
            horizontal = 0;
            vertical = 0;

            doubleTap = false;
        }


        /// <summary>
        /// Collect inputs from different keys
        /// </summary>
        public void CollectInputs()
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                 horizontal = -1;
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                horizontal = 1;
            }

            else
            {
                horizontal = 0;
            }


            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                vertical = 1;
            }


            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                vertical = -1;
            }

            else
            {
                vertical = 0;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                tapCount++;

                if (tapCount >= 2 && Time.time - timer <= tapTimer)
                {
                    doubleTap = true;
                    timer = 0;
                    tapCount = 0;
                }

                else
                {
                    timer = Time.time;
                }

                doubleTap = true;
                
                
            }

        }


        /// <summary>
        /// Returns horizontal swipe direction i.e left / right
        /// </summary>
        /// <returns></returns>
        public int GetHorizontalSwipeInput()
        {
          
            return horizontal;
        }


        /// <summary>
        /// Return vertical swipe direction i.e  up / down
        /// </summary>
        /// <returns></returns>
        public int GetVerticalSwipeInput()
        {
           

            return vertical;

        }


        /// <summary>
        /// Return whether double tapped
        /// </summary>
        /// <returns></returns>
        public bool GetDoubleTapInput()
        {           
            return doubleTap;
        }

        #endregion

    }
}


