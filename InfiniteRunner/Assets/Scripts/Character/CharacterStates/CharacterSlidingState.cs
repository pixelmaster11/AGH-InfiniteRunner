using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Character
{
    public class CharacterSlidingState : CharacterBaseState
    {
       
        private const string name = "CharacterSlidingState";



        public override void Entry(CharController Owner)
        {
            stateName = name;
            base.Entry(Owner);
            Owner.anim.Slide(true);
            Owner.StartCoroutine(SlideDuration(Owner));
        }

        public override void Update(CharController Owner)
        {
            base.Update(Owner);
        }

        public override void EXit(CharController Owner)
        {
            Owner.anim.Slide(false);
            base.EXit(Owner);
        }

       
        private IEnumerator SlideDuration(CharController controller)
        {
            controller.Slide();

            var wait = new WaitForSeconds(1);
            yield return wait;

            controller.StopSlide();

            ChangeToState(controller, CharacterBaseState.RUNNING_STATE);

            controller.StopCoroutine(SlideDuration(controller));
        }


    }

}
