using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class handles sequential spawning of tracks segments
/// 1. It will manufacture segments in a sequence (3 --> Jungle type, next 3 --> mountain, next 3 --> sea)
/// 2. This will also return a segment in the proper sequence. (If current region / segment is of type jungle)
///    It will return the next segment of type jungle as well.
/// 3. This type if particularly useful for games like agent dash, where we have different regions that spawn 
///    in a sequence. The sequence is random but, segments will be spawned continuously of 1 type till a limit
/// </summary>
namespace TrackSystem
{
    public class SequentialTrackSegmentFactory : TrackSegmentFactory
    {
        //How many segments of same type should be spawned in a sequence
        const int SEQUENTIAL_SEGMENT_COUNT = 3;

        //Previous segment spawned ID
        //TODO: rang should be from min to max segment level
        int prevSegID = Random.Range(0, 3);
        int segCount = 0;

        //Possible segments that can be spawned
        //Cache to avoid GC
        List<TrackSegment> possibleSegments = new List<TrackSegment>();


        /// <summary>
        /// Return the segment in a sequence of same seg Id as previous segment
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public override TrackSegment DeliverProduct(ref TrackSegment[] products, int maxSegLevel)
        {

            //Random seed
            Random.InitState((int)System.DateTime.Now.Ticks);

            //Check whether segments spawned in sequence have reached its limit
            if (segCount >= SEQUENTIAL_SEGMENT_COUNT)
            {
                //If yes then spawn new sequential segments
                segCount = 0;
                prevSegID = Random.Range(0, 3);
            }


            //Get possible segments of same segId as previous segment and in range of max difficulty level
            TrackSegment segment = null;
            /*List<TrackSegment>*/
            possibleSegments = trackSegmentPool.FindAll(x => !x.gameObject.activeSelf
                                && x.segID == prevSegID && x.segLevel <= maxSegLevel);

            //If there are any possible segments, then choose 1 from it
            if (possibleSegments.Count > 0)
            {
                int rand = Random.Range(0, possibleSegments.Count);
                segment = possibleSegments[rand];
            }

            //If there are 0 possible segments, then create one
            if (segment == null)
            {
                //Search the prefabs / segment types 
                for (int i = 0; i < products.Length; i++)
                {
                    //If a prefab matches the segId to spawn and is in range of max difficulty level
                    if (products[i].segID == prevSegID && products[i].segLevel <= maxSegLevel)
                    {
                        //Instantiate it
                        segment = Object.Instantiate(products[i]);
                        break;
                    }
                }

                //Add in the pool
                trackSegmentPool.Add(segment);
            }

            //Increment segment counter
            prevSegID = segment.segID;
            segCount++;

            //Clear possible segment list to avoid garbage collection
            possibleSegments.Clear();

            return segment;
        }


        /// <summary>
        /// Manufacture segments sequentially and evenly
        /// If there are 3 segment types and quantity is 21, then 7 segments of each type will be manufactured
        /// </summary>
        /// <param name="productQuantity"></param>
        /// <param name="products"></param>
        /// <param name="productParent"></param>
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
