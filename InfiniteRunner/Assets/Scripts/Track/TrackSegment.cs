

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collideable.ObstacleSystem;
using Enums;

/// <summary>
/// 1. This is a base class which represents a track segment. (A section of the track)
/// 2. Each segment has an entry and exit point based on which next segments are spawned
/// 3. Each segment has a unique identifier and type to represent different region or type of segment
///    (e.g ice, lava, jungle regions, etc)
/// 4. Each segment has a difficulty level 
/// 5. All segments contains a list of all possible obstacles the segment can have and provides
///    chances for each obstacle to spawn or not and to swap lanes or not
/// </summary>
namespace TrackSystem
{
    public abstract class TrackSegment : MonoBehaviour
    {
        [Tooltip("Segment Identifier")]
        public int segID;

        [Tooltip("Segment Difficulty level")]
        public int segLevel;

        [Tooltip("Segment type")]
        public TrackSegmentType segType;

        [Tooltip("Segment Entry Point")]
        public Transform segmentEntry;

        [Tooltip("Segment Exit Point")]
        public Transform segmentExit;

        [Tooltip("How long is the segment")]
        public float segmentLength;

        [Tooltip("All possible obstacles on this segment")]
        public Obstacle[] possibleObstacles;

        [Tooltip("Parent under which all obstacles are present")]
        public Transform obstacleRoot;

        [Tooltip("Ground visual")]
        public Transform groundMesh;

        [Tooltip("Chance of each lane spawning obstacle")]
        public Vector3 lanesChance = Vector3.zero;

        [Tooltip("Obstacle present on which lanes")]
        public int[] occupiedLanes = { 0 , 0 , 0};

        [Tooltip("Chance for obstacles swapping their positions")]
        [Range(0,11)]
        public int randomLaneChance;





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

	

