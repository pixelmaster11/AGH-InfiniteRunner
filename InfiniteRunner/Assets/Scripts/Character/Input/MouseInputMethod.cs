using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    public class MouseInputMethod : IInputMethod
    {
        //Some value to check swipe with
        private const float DEADZONE = 100;
        private Vector2 swipeDelta, startTouch;

        private int dir;
        private bool jump;
        private bool slide;

        /// <summary>
        /// Collect inputs from mouse swipes
        /// </summary>
        public void CollectInputs()
        {
        
            //Editor
            #if UNITY_EDITOR

            //Left click pressed
            if (Input.GetMouseButtonDown(0))
            {
                //Begin swiping
                startTouch = Input.mousePosition;
            }

            //Left clicked released
            else if (Input.GetMouseButtonUp(0))
            {
                //Finish swiping
                startTouch = swipeDelta = Vector2.zero;
            }

           
        
            swipeDelta = Vector2.zero;

            //If already pressed and swiped
            if (startTouch != Vector2.zero)
            {             
               //Then released to end swipe
                if (Input.GetMouseButton(0))
                {
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
                }

                
            }



            //check swipe beyond deadzone
            if (swipeDelta.magnitude > DEADZONE)
            {
                //Confirmed Swipe
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                //Horizontal Swipe
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    dir = (int)Mathf.Sign(x);
                    jump = slide = false;
                }

                //Vertical Swipe
                else
                {
                    jump = y > 0 ? true : false;
                    slide = y < 0 ? true : false;
                    dir = 0;
                }

                //Reset
                startTouch = swipeDelta = Vector2.zero;

            }

            #endif
        }



        /// <summary>
        /// Returns whether jump input
        /// </summary>
        /// <returns></returns>
        public bool GetJumpInput()
        {
            return jump;
        }

        /// <summary>
        /// Return whether move left or right
        /// </summary>
        /// <returns></returns>
        public int GetMovementInput()
        {
            return dir;
        }

        /// <summary>
        /// Returns whether slide input
        /// </summary>
        /// <returns></returns>
        public bool GetSlideInput()
        {
            return slide;
        }

        /// <summary>
        /// Reset all flags
        /// </summary>
        public void ResetInputs()
        {
            
            dir = 0;
            jump = false;
            slide = false;
        }
    }
}


	
