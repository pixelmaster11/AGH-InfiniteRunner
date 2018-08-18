using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrackSystem;

public class TrackManager : MonoBehaviour
{

    public float timer = 0;
 
   

    [SerializeField]
    TrackSegmentSpawner segmentSpawner;

 
    private void Update()
    {
        if(timer >= 2)
        {
            timer = 0;

            SpawnSegment();
            //Spawn
        }

        else
        {
            timer += Time.deltaTime;
        }
    }


    private void SpawnSegment()
    {
        segmentSpawner.SpawnTrackSegment();
    }
}
	
