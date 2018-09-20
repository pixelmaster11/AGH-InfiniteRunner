using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrackSystem;

/// <summary>
/// 1. This class is responsible for managing Track system
/// 2. It will handle segment spawn / despawn / recenter event checks 
/// 3. Handle difficulty increment based on distance covered 
/// 4. Communicate with other required managers
/// </summary>
public class TrackManager : MonoBehaviour
{

    [Tooltip("Distance between last spawned segment and camera to spawn next segment")]
    [SerializeField]
    private float spawnDistance = 30;

    [Tooltip("Exponential Base to which difficulty will increase exponentially based on distance")]
    [SerializeField]
    private long difficultyBaseDistance = 10;

    [Tooltip("Power with which to increment distance exponentially")]
    [SerializeField]
    private int difficultyBasePower = 2;

    [Tooltip("How much exponential increment")]
    [SerializeField]
    private int powerIncrement = 1;

    [Tooltip("How much segment difficulty level should increase per checkpoint")]
    [SerializeField]
    private int levelIncrement = 1;

    [Tooltip("Segment Difficulty level cap")]
    [SerializeField]
    private int maxLevelCap;

    //References
    [SerializeField]
    TrackSegmentSpawner segmentSpawner;

    [SerializeField]
    private Transform charCameraTransform;


   
    private void Update()
    {
        ///Segment spawn
        if (segmentSpawner.currentZDistance - charCameraTransform.position.z < spawnDistance)
        {
            // Spawn
            SpawnSegment();

        }

        //Increment Level
        if (segmentSpawner.currentZDistance >= Mathf.Pow(difficultyBaseDistance, difficultyBasePower))
        {
            if (segmentSpawner.maxSegmentLevel < maxLevelCap)
            {
                segmentSpawner.maxSegmentLevel += levelIncrement;
            }

            difficultyBasePower += powerIncrement;
        }

    }

    /// <summary>
    /// Spawns a track segment from segment spawner
    /// </summary>
    private void SpawnSegment()
    {          
      //Spawn a segment
      segmentSpawner.SpawnTrackSegment();

      //Despawn a segment if requried
      segmentSpawner.DeSpawn();

      bool recenter = segmentSpawner.Recenter();
      
        //TODO: Recenter event
        if(recenter)
        {
            //charManager.RecenterCharacter(segmentSpawner.currentZDistance);
        }
              
    }
}
	
