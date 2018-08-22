using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrackSystem;

namespace ObstacleSystem
{
    public static class ObstacleSpawner
    {


        public static void SpawnObstacles(ref TrackSegment onSegment)
        {
            float lane0 = -5;
            float lane1 = 0;
            float lane2 = 5;


            int laneChangeChance = Random.Range(1, 11);

            if (laneChangeChance < onSegment.randomLaneChance)
            {
                int incrementChance = Random.Range(1, 11);
                int changeAmount = -1;

                if (incrementChance >= 5)
                {
                    changeAmount = 1;
                }

                for (int j = 0; j < onSegment.possibleObstacles.Length; j++)
                {
                    onSegment.possibleObstacles[j].onLane += changeAmount;

                    if (onSegment.possibleObstacles[j].onLane > 2)
                    {
                        onSegment.possibleObstacles[j].onLane = 0;
                    }

                    else if (onSegment.possibleObstacles[j].onLane < 0)
                    {
                        onSegment.possibleObstacles[j].onLane = 2;
                    }

                }
            }


            for (int i = 0; i < onSegment.possibleObstacles.Length; i++)
            {
                Obstacle obstacle = onSegment.possibleObstacles[i];
                Vector3 finalLanePos = obstacle.transform.position;



                int spawnChance = Random.Range(1, 11);

                //Check chances for each obstacle per lane
                switch (obstacle.onLane)
                {
                    case 0:

                        if (spawnChance < onSegment.lanesChance.x)
                        {
                            finalLanePos.x = lane0;



                            obstacle.gameObject.SetActive(true);
                        }

                        break;


                    case 1:

                        if (spawnChance < onSegment.lanesChance.y)
                        {
                            finalLanePos.x = lane1;


                            obstacle.gameObject.SetActive(true);
                        }

                        break;



                    case 2:

                        if (spawnChance < onSegment.lanesChance.z)
                        {
                            finalLanePos.x = lane2;

                            obstacle.gameObject.SetActive(true);
                        }

                        break;
                }


                obstacle.transform.position = finalLanePos;
            }
        }


        public static void DeSpawnObstacles(ref TrackSegment despawnSegment)
        {
            for (int i = 0; i < despawnSegment.possibleObstacles.Length; i++)
            {
                despawnSegment.possibleObstacles[i].gameObject.SetActive(false);
            }
        }






    }

}

