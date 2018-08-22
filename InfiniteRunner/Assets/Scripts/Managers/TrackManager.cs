using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrackSystem;

public class TrackManager : MonoBehaviour
{

    public float timer = 0;

    public float spawnDistance = 30;

    [SerializeField]
    TrackSegmentSpawner segmentSpawner;

    [SerializeField]
    CharManager charManager;


    private Transform charCameraTransform;


    private void Start()
    {
        charCameraTransform = charManager.charCamera.transform;
    }

    private void Update()
    {
        if(segmentSpawner.currentZDistance - charCameraTransform.position.z < spawnDistance)
        {
            // Spawn
            SpawnSegment();
            
        }

       

    }


    private void SpawnSegment()
    {          
      segmentSpawner.SpawnTrackSegment();
      segmentSpawner.DeSpawn();
      bool recenter = segmentSpawner.Recenter();
      
        if(recenter)
        {
            charManager.RecenterCharacter(segmentSpawner.currentZDistance);
        }
              
    }
}
	
