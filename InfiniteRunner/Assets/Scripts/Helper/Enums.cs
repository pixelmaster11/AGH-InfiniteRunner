using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public enum ObstacleType
    {
        Jump,
        Slide,
        LongRun,
        Ramp,
        Patrol,
        LongBlock
    };

    public enum TrackSegmentSpawnType
    {
        RandomSegments,
        SequentialSegments
    };

    public enum TrackSegmentType
    {
        City,
        Suburb,
        Garden,
        Tunnel
    };
  
}
