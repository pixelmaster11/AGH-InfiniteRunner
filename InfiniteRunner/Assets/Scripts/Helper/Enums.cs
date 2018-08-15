﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums 
{

    //Which input method
    public enum InputMethod
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
        Ramp
    };
}