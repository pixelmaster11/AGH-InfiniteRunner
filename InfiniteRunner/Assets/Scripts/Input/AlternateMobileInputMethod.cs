
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. This class implements the input method interface. 
/// 2. It is responsible for handling mobile input (alternative method)
/// </summary>
namespace InputSystem
{
    public class AlternateMobileInputMethod : IInputMethod
    {
        //Some deadzone after which to detect swipes
        private const float DEADZONE = 100;

        //Swipe positions
        private Vector2 swipeDelta, startTouch;

        //Swipes
        private int horizontal;
        private int vertical;
        private bool doubleTap;
  

        //Double tap timer and count
        private float timer;
        private float tapTimer = 0.25f;
        private int tapCount = 0;


        public void CollectInputs()
        {
            //If we have touches
            if(Input.touches.Length != 0)
            {
                //Touch began
                if(Input.touches[0].phase == TouchPhase.Began)
                {
                    //tap = true;
                    startTouch = Input.mousePosition;

                    tapCount++;

                    //Check if double tapped
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

                //Touch end
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    startTouch = swipeDelta = Vector2.zero;
                }
            }


            swipeDelta = Vector2.zero;

            //Check for swipe amount vector
            if(startTouch != Vector2.zero)
            {
                //Mobile
                if(Input.touches.Length != 0)
                {
                    swipeDelta = Input.touches[0].position - startTouch;
                }

               
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
                    horizontal = (int) Mathf.Sign(x);
                    
                }

                //Vertical Swipe
                else
                {
                    vertical = (int)Mathf.Sign(y);
                    
                }

                //Reset
                startTouch = swipeDelta = Vector2.zero;

            }
        }



        /// <summary>
        /// Left / right swipe
        /// </summary>
        /// <returns></returns>
        public int GetHorizontalSwipeInput()
        {
            return horizontal;
        }


        /// <summary>
        /// Up / down swipe
        /// </summary>
        /// <returns></returns>
        public int GetVerticalSwipeInput()
        {
            return vertical;
        }

        /// <summary>
        /// Double tapped?
        /// </summary>
        /// <returns></returns>
        public bool GetDoubleTapInput()
        {
            return doubleTap;
        }

        /// <summary>
        /// Reset flags
        /// </summary>
        public void ResetInputs()
        {
            horizontal = 0;
            vertical = 0;
            doubleTap = false;
        }
    }
}

