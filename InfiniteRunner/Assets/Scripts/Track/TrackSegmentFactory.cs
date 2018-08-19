using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrackSystem
{
    public class TrackSegmentFactory : IGenericFactory<TrackSegment>
    {

        private List<TrackSegment> trackSegmentPool = new List<TrackSegment>();

        public void ManufactureProduct(int productQuantity, ref TrackSegment[] products, Transform productParent)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);

            for (int i = 0; i < productQuantity; i++)
            {
                TrackSegment newSegment = (TrackSegment)Object.Instantiate(products[Random.Range(0, products.Length)]);
                newSegment.gameObject.SetActive(false);
                newSegment.transform.SetParent(productParent);
                trackSegmentPool.Add(newSegment);
            }
        }

        public TrackSegment DeliverProduct(ref TrackSegment[] products)
        {
            TrackSegment segment = null;
            List<TrackSegment> possibleSegments = trackSegmentPool.FindAll(x => !x.gameObject.activeSelf);

            if (possibleSegments.Count > 0)
            {
                int rand = Random.Range(0, possibleSegments.Count);
                segment = possibleSegments[rand];
            }


            if (segment == null)
            {
                segment = Object.Instantiate(products[Random.Range(0, products.Length)]);
                trackSegmentPool.Add(segment);
            }


            return segment;

        }
    }
}

