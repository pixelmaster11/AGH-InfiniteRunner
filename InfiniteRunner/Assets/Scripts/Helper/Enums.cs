using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A helper class which stores all enum types
/// </summary>
namespace Enums 
{

    //Which input method
    public enum InputType
    {
        Keyboard,
        Mobile,
        AlternateMobile,
        Mouse
    };

    //Obstacles
    public enum ObstacleType
    {
        Jump,
        Slide,
        LongRun,
        Ramp,
        Patrol,
        LongBlock
    };

    //How to spawn track segments
    public enum TrackSegmentSpawnType
    {
        RandomSegments,
        SequentialSegments
    };

    //Track segments
    public enum TrackSegmentType
    {
        City,
        Suburb,
        Garden,
        Tunnel
    };
  

    //Characters
    public enum CharacterType
    {
        Cat,
        Racoon
    };


    //Character States 
    public enum CharacterStateType
    {
        Init,
        Running,
        Jumping,
        Sliding,
        Falling,
        Dead,
        Hit
    };
}
