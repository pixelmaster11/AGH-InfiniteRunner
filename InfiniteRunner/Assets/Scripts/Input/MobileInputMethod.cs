using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles input collected from mobile devices.
/// The input method used here is Swipe detection based on difference 
/// </summary>
/// 

namespace InputSystem
{
    public class MobileInputMethod : IInputMethod
    {


        private int horizontal;
        private int vertical;


        private bool doubleTap;

        private float timer;
        private float tapTimer = 0.25f;
        private int tapCount = 0;


        private bool isSwiping = false;
        private Vector2 startingTouchPos;


        #region Interface methods

        /// <summary>
        /// Resets all inputs
        /// </summary>
        public void ResetInputs()
        {
            horizontal = 0;
            vertical = 0;
            doubleTap = false;
        }

        /// <summary>
        /// Detects which swipes were performed by user
        /// </summary>
        public void CollectInputs()
        {
            // Use touch input on mobile
            if (Input.touchCount == 1)
            {
                if (isSwiping)
                {
                    Vector2 diff = Input.GetTouch(0).position - startingTouchPos;

                    // Put difference in Screen ratio, but using only width, so the ratio is the same on both
                    // axes (otherwise we would have to swipe more vertically...)
                    diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

                    if (diff.magnitude > 0.01f) //we set the swip distance to trigger movement to 1% of the screen width
                    {
                        if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                        {
                            vertical = (int) Mathf.Sign(diff.y);
             
                        }
                        else
                        {
                           horizontal = (int)Mathf.Sign(diff.x);
                        }

                        isSwiping = false;
                    }
                }

                // Input check is AFTER the swipe test, that way if TouchPhase.Ended happen a single frame after the Began Phase
                // a swipe can still be registered (otherwise, isSwiping will be set to false and the test wouldn't happen for that began-Ended pair)
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startingTouchPos = Input.GetTouch(0).position;
                    isSwiping = true;

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

                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    isSwiping = false;
                }
            }
        }


        /// <summary>
        /// Returns the direction of swipe left or right
        /// </summary>
        /// <returns></returns>
        public int GetHorizontalSwipeInput()
        {
            return horizontal;
        }


        /// <summary>
        /// Returns whether swiped up / down
        /// </summary>
        /// <returns></returns>
        public int GetVerticalSwipeInput()
        {

            return vertical;
            

        }


        /// <summary>
        /// Returns whether double tapped or not
        /// </summary>
        /// <returns></returns>
        public bool GetDoubleTapInput()
        {
            return doubleTap;
        }

        #endregion
    }
}

