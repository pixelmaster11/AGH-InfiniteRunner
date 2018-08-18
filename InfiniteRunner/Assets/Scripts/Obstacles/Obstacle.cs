using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using TrackSystem;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField]
    protected ObstacleType type;

    [SerializeField]
    protected int graphicsID;
    
    public int obstacleLength;
 
    public int onLane;
  
    public TrackSegment onSegment;


    public abstract void OnSpawn();
    public abstract void OnDeSpawn();

    public abstract void OnImpact();
}
