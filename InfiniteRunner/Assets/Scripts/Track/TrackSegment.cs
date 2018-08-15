using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrackSystem
{
    public class TrackSegment : MonoBehaviour
    {

        public int segID;
        public Transform segmentEntry;
        public Transform segmentExit;
        public float segmentLength;


#if UNITY_EDITOR
        void OnDrawGizmos()
        {

            Vector3 entryPos = new Vector3(segmentEntry.transform.position.x, 2, segmentEntry.transform.position.z);
            Vector3 exitPos = new Vector3(segmentExit.transform.position.x, 2, segmentExit.transform.position.z);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(entryPos, exitPos);


            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(entryPos, 2f);
            Gizmos.DrawSphere(exitPos, 2f);


        }
#endif
    }
}

	

