using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class represents a factory which manufactures track segments
/// </summary>
namespace TrackSystem
{
    public abstract class TrackSegmentFactory : IGenericFactory<TrackSegment>
    {
        //Pool of all track segmetns stored
        protected List<TrackSegment> trackSegmentPool = new List<TrackSegment>();

        //Manufacture tracksegments
        public abstract void ManufactureProduct(int productQuantity, ref TrackSegment[] products, Transform productParent);

        //Return a track segment
        public abstract TrackSegment DeliverProduct(ref TrackSegment[] products, int maxSegmentLevel);
   

        
    }
}

