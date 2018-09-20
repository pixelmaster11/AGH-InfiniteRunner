using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/CharacterType")]
public class Character : ScriptableObject
{
    
    public Enums.CharacterType characterType;

    public CharacterMovementStats movementStats;
}
