#define Debug

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

        public Obstacle[] possibleObstacles;

        public Transform obstacleRoot;
        public Transform groundMesh;

        public Vector3 lanesChance = Vector3.zero;

        public int[] occupiedLanes = { 0 , 0 , 0};

        [Range(0,11)]
        public int randomLaneChance;





#if UNITY_EDITOR
#if Debug
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


        private void OnValidate()
        {
            SetSize();
        }



        private void SetSize()
        {
            if (groundMesh != null)
            {
                groundMesh.transform.localScale = new Vector3(groundMesh.transform.localScale.x,
                                                    groundMesh.transform.localScale.y, segmentLength);
            }


            if (segmentEntry != null)
            {
                segmentEntry.transform.localPosition = new Vector3(segmentEntry.transform.localPosition.x,
                                                        segmentEntry.transform.localPosition.y, -segmentLength / 2);
            }

            if (segmentExit != null)
            {
                segmentExit.transform.localPosition = new Vector3(segmentExit.transform.localPosition.x,
                                                        segmentExit.transform.localPosition.y, segmentLength / 2);

            }

        }


#endif
    }
}

	

