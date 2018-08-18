using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrackSystem
{
    public class TrackSegmentFactory : MonoBehaviour
    {

        public TrackSegment[] trackProducts;
        public List<TrackSegment> trackSegmentPool;


        public void ManufactureTrackSegments(int quantity)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);

            for (int i = 0; i < quantity; i++)
            {
                TrackSegment newSegment = (TrackSegment)Instantiate(trackProducts[Random.Range(0 , trackProducts.Length)]);
                newSegment.gameObject.SetActive(false);
                newSegment.transform.SetParent(this.transform);
                trackSegmentPool.Add(newSegment);
            }
        }

        public TrackSegment DeliverTrackSegment()
        {
           TrackSegment segment = null;
           List<TrackSegment> possibleSegments = trackSegmentPool.FindAll(x => !x.gameObject.activeSelf);

            if(possibleSegments.Count > 0)
            {
                int rand = Random.Range(0, possibleSegments.Count);
                segment = possibleSegments[rand];
            }
          

            if (segment == null)
            {
                segment = Instantiate(trackProducts[Random.Range(0 , trackProducts.Length)]);
                trackSegmentPool.Add(segment);
            }

          
            return segment;

        }

    }
}

