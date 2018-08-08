
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    public class AlternateMobileInputMethod : IInputMethod
    {
        private const float DEADZONE = 100;
        private bool tap, swipeRight, swipeLeft, swipeUp, swipeDown;
        private Vector2 swipeDelta, startTouch;

        private int dir;
        private bool jump;
        private bool slide;

        public void CollectInputs()
        {

            //Editor
            #if UNITY_EDITOR

            if(Input.GetMouseButtonDown(0))
            {
                tap = true;
                startTouch = Input.mousePosition;
            }

            else if(Input.GetMouseButtonUp(0))
            {
                startTouch = swipeDelta = Vector2.zero;
            }
            #endif

            //Mobile
            if(Input.touches.Length != 0)
            {
                if(Input.touches[0].phase == TouchPhase.Began)
                {
                    tap = true;
                    startTouch = Input.mousePosition;
                }

                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    startTouch = swipeDelta = Vector2.zero;
                }
            }


            swipeDelta = Vector2.zero;

            if(startTouch != Vector2.zero)
            {
                //Mobile
                if(Input.touches.Length != 0)
                {
                    swipeDelta = Input.touches[0].position - startTouch;
                }

                #if UNITY_EDITOR

                if(Input.GetMouseButton(0))
                {
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
                }

                #endif
            }


            //check swipe beyond deadzone
            if(swipeDelta.magnitude > DEADZONE)
            {
                //Confirmed Swipe
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                //Horizontal Swipe
                if(Mathf.Abs(x) > Mathf.Abs(y))
                {
                    dir = (int) Mathf.Sign(x);
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
        }




        public bool GetJumpInput()
        {
            return jump;
        }

        public int GetMovementInput()
        {
            return dir;
        }

        public bool GetSlideInput()
        {
            return slide;
        }

        public void ResetInputs()
        {
            tap = swipeLeft = swipeRight = swipeDown = swipeUp = false;
            dir = 0;
            jump = false;
            slide = false;
        }
    }
}

