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

        private int horizontal;
        private int vertical;

        private bool doubleTap;

        private float timer;
        private float tapTimer = 0.25f;
        private int tapCount = 0;

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
                    horizontal = (int)Mathf.Sign(x);
                   
                }

                //Vertical Swipe
                else
                {
                    vertical = (int)Mathf.Sign(y);
                }

                //Reset
                startTouch = swipeDelta = Vector2.zero;

            }

            #endif
        }



        /// <summary>
        /// Returns whether swiped left / right
        /// </summary>
        /// <returns></returns>
        public int GetHorizontalSwipeInput()
        {
            return horizontal;
        }

        /// <summary>
        /// Return whether swiped up / down
        /// </summary>
        /// <returns></returns>
        public int GetVerticalSwipeInput()
        {
            return vertical;
        }

        /// <summary>
        /// Returns whether double tapped
        /// </summary>
        /// <returns></returns>
        public bool GetDoubleTapInput()
        {
            return doubleTap;
        }

        /// <summary>
        /// Reset all flags
        /// </summary>
        public void ResetInputs()
        {
            
            horizontal = 0;
            vertical = 0;
            
            doubleTap = false;
        }
    }
}


	
