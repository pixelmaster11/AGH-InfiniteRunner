using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrackSystem
{
    public class TrackSegmentFactory : MonoBehaviour
    {

        public TrackSegment testPrefab;
        public List<TrackSegment> trackSegmentPool;

        public TrackSegment GetTrackSegment()
        {
            TrackSegment segment = trackSegmentPool.Find(x => !x.gameObject.activeSelf);

            if (segment == null)
            {
                segment = Instantiate(testPrefab);
                trackSegmentPool.Add(segment);
            }


            return segment;

        }

    }
}

