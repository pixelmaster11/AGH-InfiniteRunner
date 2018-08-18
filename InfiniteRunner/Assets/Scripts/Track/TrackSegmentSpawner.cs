using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrackSystem
{

    public class TrackSegmentSpawner : MonoBehaviour
    {

        private Vector3 currentExitPoint;
        private Vector3 entryPoint;
        private Vector3 pos;

        [SerializeField]
        private int trackSegmentQuantity;

        public List<TrackSegment> activeSegments;

        [SerializeField]
        private Transform trackSegmentRoot;
        
        [SerializeField]
        private TrackSegmentFactory segmentFactory;

       


        private void Start()
        {
            segmentFactory.ManufactureTrackSegments(trackSegmentQuantity);
        }


        public void SpawnTrackSegment()
        {

            TrackSegment newSegment = segmentFactory.DeliverTrackSegment();

            if (activeSegments.Count > 0)
            {
                currentExitPoint = activeSegments[activeSegments.Count - 1].segmentExit.transform.position;
            }

            else
            {
                currentExitPoint = Vector3.zero;
            }

            entryPoint = newSegment.segmentEntry.transform.position;

            pos = currentExitPoint + (newSegment.transform.position - entryPoint);
            newSegment.transform.position = pos;

            newSegment.gameObject.SetActive(true);
            newSegment.transform.SetParent(trackSegmentRoot);


            activeSegments.Add(newSegment);

            ObstacleSpawner.SpawnObstacles(newSegment);
        }



        public void DeSpawnTrackSegment(int deSpawnIndex)
        {
            TrackSegment despawnSegment = activeSegments[deSpawnIndex];
            despawnSegment.gameObject.SetActive(false);
            despawnSegment.transform.SetParent(segmentFactory.transform);

            activeSegments.RemoveAt(deSpawnIndex);
        }

    }

}
