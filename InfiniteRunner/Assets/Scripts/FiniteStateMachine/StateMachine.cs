using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace FSM
{
    public class StateMachine
    {

        //Currently running state
        private IState currentState;

        //Previously run state
        private IState prevState;


        /// <summary>
        /// Change the current state to a new state
        /// </summary>
        /// <param name="newState">The new state to change to </param>
        public void ChangeState(IState newState)
        {
            //check null for initialization
            if(currentState != null)
            {
                //Exit the current state
                currentState.Exit();
                _LogState("Exit");
            }

            //Make current state as previous
            prevState = currentState;

            //Make the new state as current
            currentState = newState;

            //Enter the new state
            currentState.Entry();

            _LogState("Entry");
        }

       
        /// <summary>
        /// Conitnuously update the the state i.e keep the current state contionuously running
        /// </summary>
        public void StateUpdate()
        {
            if(currentState != null)
            {
                //Keep running the current state
                currentState.Update();
                _LogState("Update: " + Time.time);
            }
        }


        /// <summary>
        /// Similar to State Update except this will run every not per frame but after xx seconds
        /// </summary>
        /// <param name="timeDelay"> Time after every second this function will run </param>
        public void StateUpdateDelayed(float timeDelay)
        {
            float timer = 0;

            if (currentState != null)
            {
                //Run after every xx seconds
                if(timer >= timeDelay)
                {
                    currentState.Update();
                    timer = 0;
                }

                else
                {
                    timer += Time.deltaTime;
                }
              
            }
        }


        /// <summary>
        /// Get the name of the currently running state
        /// </summary>
        /// <returns></returns>
        public string GetCurrentStateName()
        {
            if(currentState != null)
            {
                return currentState.GetStateName();
            }

            return string.Empty;
           
        }


        /// <summary>
        /// Get the name of previously run state
        /// </summary>
        /// <returns></returns>
        public string GetPreviousStateName()
        {
            if (prevState != null)
            {
                return prevState.GetStateName();
            }

            return string.Empty;

        }


        /// <summary>
        /// Get currently running state
        /// </summary>
        /// <returns></returns>
        public IState GetCurrentState()
        {
            return currentState;
        }

        /// <summary>
        /// Get previously run state
        /// </summary>
        /// <returns></returns>
        public IState GetPreviousState()
        {
            return prevState;
        }


        [Conditional("DEBUG_STATEMACHINE")]
        private void _LogState(string message)
        {
            UnityEngine.Debug.Log("[Current State: " + GetCurrentStateName() + "] " + message);
        }

    }
}

