using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collideable.ObstacleSystem;
using Enums;

/// <summary>
/// 1. This class is responsible for spawning tracksegments
/// 2. This class provides all settings for segment spawns (active segments, initial segments, etc)
/// </summary>
namespace TrackSystem
{

    public class TrackSegmentSpawner : MonoBehaviour
    {
        #region Variables
        //Keep track of current Exit point which is the exit point of last spawned segment
        private Vector3 currentExitPoint;
        private Vector3 entryPoint;
        private Vector3 pos;


        [Tooltip("Total number of segments to be spawned at the start of the game play")]
        [SerializeField]
        private int startSegments;

        [Tooltip("Total number of segments to be active / remain in scene")]
        [SerializeField]
        private int maxActiveSegments;

        [Tooltip("Total number of segments to be pooled")]
        [SerializeField]
        private int trackSegmentPoolQuantity;

        [Tooltip("Total number of segments in a batch that will be spawned together")]
        [SerializeField]
        private int spawnBatchSize;

        [Tooltip("Total distance after which whole game will be recentered")]
        [SerializeField]
        private float maxZDistance;

        [Tooltip("Keep track of current distance in Z-axis")]
        public float currentZDistance;

        [Tooltip("Maximum difficulty level of segments that can be spawned")]
        public int maxSegmentLevel;

        [Tooltip("Track segment prefabs: Must include prefabs of all difficulty levels")]
        [SerializeField]
        private TrackSegment[] trackProducts;

        [Tooltip("List of all currently spawned segments in the scene")]
        [SerializeField]
        private List<TrackSegment> activeSegments;

        [Tooltip("Transform under which all track segments should be spawned")]
        [SerializeField]
        private Transform trackSegmentRoot;

        [Tooltip("Type of spawning: Random or Sequential")]
        [SerializeField]
        private TrackSegmentSpawnType spawnType;

        //Type of spawning factory
        private TrackSegmentFactory segmentFactory;

        #endregion

        #region Monobehaviour Methods

        //Set segment factory type
        private void OnEnable()
        {
            SetSpawnFactoryType();
        }

        //Initial spawn
        private void Start()
        {
            
            segmentFactory.ManufactureProduct(trackSegmentPoolQuantity, ref trackProducts, this.transform);
  
            for(int i = 0; i < startSegments; i++)
            {
                SpawnTrackSegment(false);
            }
        }

        #endregion

        #region Spawn Methods
        /// <summary>
        /// Set appropriate Factory type
        /// </summary>
        private void SetSpawnFactoryType()
        {
            switch(spawnType)
            {
                case TrackSegmentSpawnType.RandomSegments:
                    segmentFactory = new RandomTrackSegmentFactory();
                    break;

                case TrackSegmentSpawnType.SequentialSegments:
                    segmentFactory = new SequentialTrackSegmentFactory();
                    break;
            }
        }


        /// <summary>
        /// Spawn track segments either individually or in batches
        /// </summary>
        /// <param name="useBatch"></param>
        public void SpawnTrackSegment(bool useBatch = true)
        {   
            if(!useBatch)
            {
                Spawn();
            }

            else
            {
                for (int i = 0; i < spawnBatchSize; i++)
                {
                    Spawn();
                }

            }


        }


        /// <summary>
        /// Spawn track segment
        /// </summary>
        private void Spawn()
        {

            //Get a track segment from the factory's pool
            TrackSegment newSegment = segmentFactory.DeliverProduct(ref trackProducts, maxSegmentLevel);

            //If we have atleast 1 segment spawned
            if (activeSegments.Count > 0)
            {
                //Set value of current exit to the last spawned segment
                 currentExitPoint = activeSegments[activeSegments.Count - 1].segmentExit.transform.position;
            }

            //Else we start from 0
            else
            {
                currentExitPoint = Vector3.zero + newSegment.segmentEntry.position ;
            }

            //Entry point for new segment to be spawned
            entryPoint = newSegment.segmentEntry.transform.position;

            //Position of new segment will be last segment's exit position - new segment entry position
            pos = currentExitPoint + (newSegment.transform.position - entryPoint);
            newSegment.transform.position = pos;

            //Activate segment and set its parent
            newSegment.gameObject.SetActive(true);
            newSegment.transform.SetParent(trackSegmentRoot);

            //Update current Z distance
            currentZDistance = newSegment.segmentExit.transform.position.z;

            //Add to active segments pool
            activeSegments.Add(newSegment);

            //Spawn obstacles
            ObstacleSpawner.SpawnObstacles(ref newSegment);                 
            
        }

        #endregion

        #region Despawn Methods

        /// <summary>
        /// Despawn segments 
        /// </summary>
        public void DeSpawn()
        {
            //Check if active segments exceed max segment count
            if (activeSegments.Count > maxActiveSegments)
            {
                //Check for how many segments exceeded
                int deSpawnBatch = activeSegments.Count - maxActiveSegments;

                //Despawn the exceeded segments starting from the very first segment
                for (int i = 0; i < deSpawnBatch; i++)
                {
                    DeSpawnTrackSegment(0);
                }

            }
        }


     

        /// <summary>
        /// Despawn the segment
        /// </summary>
        /// <param name="deSpawnIndex"></param>
        public void DeSpawnTrackSegment(int deSpawnIndex)
        {
            TrackSegment despawnSegment = activeSegments[deSpawnIndex];

            //Despawn obstacles
            ObstacleSpawner.DeSpawnObstacles(ref despawnSegment);

            //Deactivate segment
            despawnSegment.gameObject.SetActive(false);
            despawnSegment.transform.SetParent(this.transform);

            //Remove from active segments
            activeSegments.RemoveAt(deSpawnIndex);
        }

        #endregion

        #region Recenter Methods
        /// <summary>
        /// Recenter the whole game play
        /// </summary>
        /// <returns></returns>
        public bool Recenter()
        {
            //Check if current Z distance excceds max allowed Z distance value
            if (currentZDistance > maxZDistance)
            {
                //If yes then recenter everything to 0
                Vector3 recenter = activeSegments[0].transform.position;

                for (int i = 0; i < activeSegments.Count; i++)
                {
                    activeSegments[i].transform.position -= recenter;
                }

                return true;
            }

            return false;
        }

        #endregion


    }

}
