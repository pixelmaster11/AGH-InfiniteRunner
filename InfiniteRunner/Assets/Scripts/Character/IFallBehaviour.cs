using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFallBehaviour
{
    void ApplyGravity();

    void FastFall();

    bool CheckForFall();
	
}
