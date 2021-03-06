﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem.CharacterComponents
{
    public class CharAnimator : MonoBehaviour
    {

        [SerializeField]
        private Animator characterAnim;


        private int jumpHash;
        private int randIdleHash;
        private int movingHash;
        private int jumpSpeedHash;
        private int laneSwitchHash;
        private int deadHash;
        private int hitHash;
        private int slideHash;
        private int randDeathHash;
        private int groundHash;

        private void SetHashes()
        {
            jumpHash = Animator.StringToHash("Jumping");
            randIdleHash = Animator.StringToHash("RandomIdle");
            movingHash = Animator.StringToHash("Moving");
            jumpSpeedHash = Animator.StringToHash("JumpSpeed");
            laneSwitchHash = Animator.StringToHash("LaneSwitch");
            deadHash = Animator.StringToHash("Dead");
            hitHash = Animator.StringToHash("Hit");
            slideHash = Animator.StringToHash("Sliding");
            randDeathHash = Animator.StringToHash("RandomDeath");
            groundHash = Animator.StringToHash("Grounded");
        }


        private void Start()
        {
            SetHashes();
        }


        public void Jump(bool start)
        {
            characterAnim.SetBool(jumpHash, start);
        }

        public void Slide(bool start)
        {
            
            characterAnim.SetBool(slideHash, start);
        }

        public void Grounded(bool enable)
        {
            characterAnim.SetBool(groundHash, enable);
        }
    }
}

