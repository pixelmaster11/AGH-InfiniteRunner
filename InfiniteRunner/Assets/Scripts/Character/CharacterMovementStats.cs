
using UnityEngine;

[CreateAssetMenu( menuName = "Characters/MovementStats")]
public class CharacterMovementStats : ScriptableObject
{

    [Tooltip("Iniital character speed")]
    public float minSpeed = 5;

    [Tooltip("Maximum character speed")]
    public float maxSpeed = 20;


    [Tooltip("How high the character jumps")]
    public float jumpForce = 11;

    [Tooltip("How much force applied to pull down character")]
    public float gravity = 21;

   
}
