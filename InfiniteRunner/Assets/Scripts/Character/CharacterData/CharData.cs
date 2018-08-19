using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem.CharacterData
{
    [System.Serializable]
    public class CharData
    {

        public string characterName;
        public float unlockCoinCost;
        public int unlockPaidCost;
        public int characterID;

    }

    [System.Serializable]
    public class CharStats
    {
        public int lives;
    }


    [System.Serializable]
    public class CharSaveData
    {
        public long topRunScore;
        public bool isUnlocked;

    }

    [System.Serializable]
    public class CharMovementData
    {
        public float constMoveSpeed;
        public float laneChangeSpeed;
        public float laneOffset;
        public float turnSpeed;
        public float gravity;
        public float jumpForce;


    }

}


