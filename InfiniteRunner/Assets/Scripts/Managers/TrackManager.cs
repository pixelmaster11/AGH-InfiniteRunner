using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrackSystem;

public class TrackManager : MonoBehaviour
{

    public float timer = 0;
    public Vector3 currentExitPoint;
    public Vector3 entryPoint;
    public Vector3 pos;

    public List<TrackSegment> activeSegments;

    [SerializeField]
    TrackSegmentFactory segmentFactory;

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
        TrackSegment newSegment = segmentFactory.GetTrackSegment();

        if(activeSegments.Count > 0)
        {
            currentExitPoint =  activeSegments[activeSegments.Count - 1].segmentExit.transform.position;
        }

        else
        {
            currentExitPoint = Vector3.zero;
        }
        
        entryPoint = newSegment.segmentEntry.transform.position;

        pos = currentExitPoint + (newSegment.transform.position - entryPoint);
        newSegment.transform.position = pos;

        newSegment.gameObject.SetActive(true);

        activeSegments.Add(newSegment);
       
    }
}
	
