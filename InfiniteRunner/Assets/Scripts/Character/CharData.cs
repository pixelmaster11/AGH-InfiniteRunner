using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharData
{

    public string characterName;
    public float unlockCoinCost;
    public int unlockPaidCost;


}

[System.Serializable]
public class CharMovementData
{

    public float timeBeforeRunStart;
    public float constMoveSpeed;
    public float laneChangeSpeed;
    public float laneOffset;
    public float turnSpeed;
    public float gravity;
    public float jumpForce;
}
