using InputSystem;
using CharacterSystem.CharacterComponents;
using System.Collections.Generic;

/// <summary>
/// 1. This is the base class for each character state.
/// 2. A base class provides a good way for our character to move continuously from any state.
/// </summary>


namespace FSM.Character
{
    public abstract class CharacterBaseState : ICharacterState
    {     

        //Currently running state name
        public string stateName;

        //Cache the controller
        protected BaseController controller;


        /// <summary>
        /// Entry point hook to the state 
        /// </summary>
        /// <param name="Owner">Who owns this FSM</param>
        public virtual void Entry(BaseController Owner)
        {
            controller = Owner;
           
        }


        /// <summary>
        /// 1. Update hook which ticks every frame
        /// 2. We need our player to move continuously from any state, so move here
        /// 3. We can change lanes from any state currently. This is a design decision
        /// </summary>
        /// <param name="Owner">Who owns the state</param>
        public virtual void Update()
        {
            //Get inputs from all states
            CharacterInput.ResetInputs();
            CharacterInput.CollectInputs();

            //Move the character constantly in z-axis (World - forward axis)
            controller.ConstantMove();

            //Check whether character is grounded
            controller.GroundCheck();

            //Check for right input
            if (CharacterInput.SwipeRightInput())
            {
                controller.ChangeLane(true);
            }

            //Check for left input
            else if (CharacterInput.SwipeLeftInput())
            {
                controller.ChangeLane(false);
            }


        }


        /// <summary>
        /// Exit point hook of the state
        /// </summary>
        /// <param name="Owner">Who owns this FSM</param>
        public virtual void Exit()
        {
           
        }


   


        /// <summary>
        /// Return currently running state name
        /// </summary>
        /// <returns></returns>
        public string GetStateName()
        {
            return stateName;
        }



    }

}
