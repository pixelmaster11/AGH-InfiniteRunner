using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace TrackSystem
{
    public abstract class TrackSegmentFactory : IGenericFactory<TrackSegment>
    {

        protected List<TrackSegment> trackSegmentPool = new List<TrackSegment>();

        public abstract void ManufactureProduct(int productQuantity, ref TrackSegment[] products, Transform productParent);
      
        public abstract TrackSegment DeliverProduct(ref TrackSegment[] products);
   

        
    }
}

