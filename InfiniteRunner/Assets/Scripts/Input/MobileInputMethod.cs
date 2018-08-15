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

        private bool slide = false;
        private bool jump = false;
        private int dir;

        private bool isSwiping = false;
        private Vector2 startingTouchPos;


        #region Interface methods

        /// <summary>
        /// Resets all inputs
        /// </summary>
        public void ResetInputs()
        {
            dir = 0;
            jump = false;
            slide = false;
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
                            if (diff.y < 0)
                            {
                                slide = true;
                                jump = false;

                            }
                            else
                            {
                                jump = true;
                                slide = false;

                            }

                            dir = 0;
                        }
                        else
                        {
                            jump = false;
                            slide = false;

                            if (diff.x < 0)
                            {
                                dir = -1;
                            }
                            else
                            {
                                dir = 1;
                            }
                        }

                        isSwiping = false;
                    }
                }

                // Input check is AFTER the swip test, that way if TouchPhase.Ended happen a single frame after the Began Phase
                // a swipe can still be registered (otherwise, m_IsSwiping will be set to false and the test wouldn't happen for that began-Ended pair)
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startingTouchPos = Input.GetTouch(0).position;
                    isSwiping = true;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    isSwiping = false;
                }
            }
        }


        /// <summary>
        /// Returns the direction of movement
        /// </summary>
        /// <returns></returns>
        public int GetMovementInput()
        {
            return dir;
        }


        /// <summary>
        /// Returns whether jump input or not
        /// </summary>
        /// <returns></returns>
        public bool GetJumpInput()
        {

            return jump;

        }


        /// <summary>
        /// Returns whether slide input or not
        /// </summary>
        /// <returns></returns>
        public bool GetSlideInput()
        {
            return slide;
        }

        #endregion
    }
}

