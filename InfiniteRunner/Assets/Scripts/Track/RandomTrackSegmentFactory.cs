using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles random production of track segments.
/// 1. This class manufactures track segments randomly from various track segment types
/// 2. This class also returns a random track segment from the pool
/// 3. This is uselful for a game like subway surfer where the playing region is same throughout only changes are 
///    in the segments. You have a train track environment / region throughout. 
/// </summary>
namespace TrackSystem
{
    public class RandomTrackSegmentFactory : TrackSegmentFactory
    {
        //Cache lists to avoid GC
        List<TrackSegment> possibleSegments = new List<TrackSegment>();
        List<int> possibleRandomSegments = new List<int>();

        /// <summary>
        /// Manufacture tracksegments randomly from the provided prefabs
        /// </summary>
        /// <param name="productQuantity">Amount to manufacture</param>
        /// <param name="products">Prefabs / types of tracksegments that can be manufactured</param>
        /// <param name="productParent">Transform under which to keep the pooled segments</param>
        public override void ManufactureProduct(int productQuantity, ref TrackSegment[] products, Transform productParent)
        {
            //Random seed
            Random.InitState((int)System.DateTime.Now.Ticks);

            //Segemnts to manufacture(pool)
            for (int i = 0; i < productQuantity; i++)
            {
                //Get random segment to pool
                TrackSegment newSegment = (TrackSegment)Object.Instantiate(products[Random.Range(0, products.Length)]);

                //Set parent, disable and add it to pool
                newSegment.gameObject.SetActive(false);
                newSegment.transform.SetParent(productParent);
                trackSegmentPool.Add(newSegment);
            }
        }


        /// <summary>
        /// Returns a randomly chosen track segment
        /// </summary>
        /// <param name="products"></param>
        /// <returns>A random track segment from the pool</returns>
        public override TrackSegment DeliverProduct(ref TrackSegment[] products, int maxSegLevel)
        {
            //Random seed
            Random.InitState((int)System.DateTime.Now.Ticks);

            TrackSegment segment = null;

            //Find all possible segments that can be returned in range of difficulty level
            possibleSegments = trackSegmentPool.FindAll(x => !x.gameObject.activeSelf && x.segLevel <= maxSegLevel);

            //If there are any possible segments
            if (possibleSegments.Count > 0)
            {
                //Choose randomly
                int rand = Random.Range(0, possibleSegments.Count);
                segment = possibleSegments[rand];
            }

            //Create new random segment, if 0 possible segments
            if (segment == null)
            {
                //Check if new segment fits within the possible difficulty level, then add it to possible spawns
                for (int i = 0; i < products.Length; i++)
                {
                    if (products[i].segLevel <= maxSegLevel)
                    {
                        possibleRandomSegments.Add(i);
                    }
                }

                //Create one random segment from possible spawns
                if (possibleRandomSegments.Count > 0)
                {
                    int rand = Random.Range(0, possibleRandomSegments.Count);
                    segment = Object.Instantiate(products[possibleRandomSegments[rand]]);

                    trackSegmentPool.Add(segment);
                }

                else
                {
                    Utils.DebugUtils.LogError("Please make sure track segments prefabs provided to create pool includes atleast 1 prefab of all difficulty levels");
                }


            }

            possibleSegments.Clear();
            possibleRandomSegments.Clear();

            //Return random segment
            return segment;
        }

      
    }

}
