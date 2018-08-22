using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObstacleSystem;
using Enums;

namespace TrackSystem
{

    

    public class TrackSegmentSpawner : MonoBehaviour
    {

        private Vector3 currentExitPoint;
        private Vector3 entryPoint;
        private Vector3 pos;


        [SerializeField]
        private int startSegments;

        [SerializeField]
        private int maxActiveSegments;

        [SerializeField]
        private int trackSegmentPoolQuantity;

        [SerializeField]
        private int spawnBatchSize;

        [SerializeField]
        private float maxZDistance;

        public float currentZDistance;

        public TrackSegment[] trackProducts;

        public List<TrackSegment> activeSegments;

        [SerializeField]
        private Transform trackSegmentRoot;

        public TrackSegmentSpawnType spawnType;


        private TrackSegmentFactory segmentFactory;



        private void OnEnable()
        {
            SetSpawnFactoryType();
        }

        private void Start()
        {
            
            segmentFactory.ManufactureProduct(trackSegmentPoolQuantity, ref trackProducts, this.transform);
  
            for(int i = 0; i < startSegments; i++)
            {
                SpawnTrackSegment(false);
            }
        }


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


        private void Spawn()
        {


            TrackSegment newSegment = segmentFactory.DeliverProduct(ref trackProducts);

            if (activeSegments.Count > 0)
            {
                currentExitPoint = activeSegments[activeSegments.Count - 1].segmentExit.transform.position;
            }

            else
            {
                currentExitPoint = Vector3.zero;
            }

            entryPoint = newSegment.segmentEntry.transform.position;

            pos = currentExitPoint + (newSegment.transform.position - entryPoint);
            newSegment.transform.position = pos;

            newSegment.gameObject.SetActive(true);
            newSegment.transform.SetParent(trackSegmentRoot);
            currentZDistance = newSegment.segmentExit.transform.position.z;

            activeSegments.Add(newSegment);

            ObstacleSpawner.SpawnObstacles(ref newSegment);                 
            
        }


        public void DeSpawn()
        {
            if (activeSegments.Count > maxActiveSegments)
            {
                int deSpawnBatch = activeSegments.Count - maxActiveSegments;

                for (int i = 0; i < deSpawnBatch; i++)
                {
                    DeSpawnTrackSegment(0);
                }

            }
        }


        public bool Recenter()
        {
            if (currentZDistance > maxZDistance)
            {
                Vector3 recenter = activeSegments[0].transform.position;

                for (int i = 0; i < activeSegments.Count; i++)
                {
                    activeSegments[i].transform.position -= recenter;
                }

                return true;
            }

            return false;
        }



        public void DeSpawnTrackSegment(int deSpawnIndex)
        {
            TrackSegment despawnSegment = activeSegments[deSpawnIndex];
            ObstacleSpawner.DeSpawnObstacles(ref despawnSegment);
            despawnSegment.gameObject.SetActive(false);
            despawnSegment.transform.SetParent(this.transform);

            activeSegments.RemoveAt(deSpawnIndex);
        }

    }

}
