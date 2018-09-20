using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using TrackSystem;

namespace Collideable.ObstacleSystem
{
    public abstract class Obstacle : CollideableObjects
    {
        [SerializeField]
        protected ObstacleType type;

        [SerializeField]
        protected int graphicsID;

        public int obstacleLength;


        [Tooltip("Chance for the obstacle to be spawned: -1 meaning 0 chance, 11 means 100% chance of spawn")]
        [Range(0, 11)]
        public int obstacleChance;

        [Tooltip("Chance for the obstacle lane positions to be swapped: -1 meaning 0 % chance and 11 means 100% chance")]
        [Range(0, 11)]
        public int obstacleLaneSwapChance;

        [Tooltip("Which lane the obstacle is on")]
        public int onLane;

        [Tooltip("Which track segment obstacle is on")]
        public TrackSegment onSegment;

        public Vector3 originalPosition;


        public override void OnSpawn()
        {

        }

        public override void OnDeSpawn()
        {
            impacted = false;
        }

        public override void OnImpact()
        {
            impacted = true;
        }
    }

}
