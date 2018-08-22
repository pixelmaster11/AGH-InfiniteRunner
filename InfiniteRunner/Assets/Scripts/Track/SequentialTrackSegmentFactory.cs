using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace TrackSystem
{
    public class SequentialTrackSegmentFactory : TrackSegmentFactory
    {
        const int SEQUENTIAL_SEGMENT_COUNT = 3;

        int prevSegID = Random.Range(0, 3);
        int segCount = 0;

        public override TrackSegment DeliverProduct(ref TrackSegment[] products)
        {

            if (segCount >= SEQUENTIAL_SEGMENT_COUNT)
            {
                segCount = 0;
                prevSegID = Random.Range(0, 3);
            }

            TrackSegment segment = null;
            List<TrackSegment> possibleSegments = trackSegmentPool.FindAll(x => !x.gameObject.activeSelf && x.segID == prevSegID);

            if (possibleSegments.Count > 0)
            {
                int rand = Random.Range(0, possibleSegments.Count);
                segment = possibleSegments[rand];
            }


            if (segment == null)
            {
                for(int i = 0; i < products.Length; i++)
                {           
                    if(products[i].segID == prevSegID)
                    {
                        segment = Object.Instantiate(products[i]);
                        break;
                    }
                }

                       
                trackSegmentPool.Add(segment);
            }


            prevSegID = segment.segID;
            segCount++;


            return segment;
        }

        public override void ManufactureProduct(int productQuantity, ref TrackSegment[] products, Transform productParent)
        {
           

            for(int j = 0; j < products.Length; j++)
            {
                for (int i = 0; i < productQuantity / products.Length; i++)
                {

                    TrackSegment newSegment = (TrackSegment)Object.Instantiate(products[j]);
                    newSegment.gameObject.SetActive(false);
                    newSegment.transform.SetParent(productParent);
                    trackSegmentPool.Add(newSegment);
                }
            }
           
        }
    }

}
