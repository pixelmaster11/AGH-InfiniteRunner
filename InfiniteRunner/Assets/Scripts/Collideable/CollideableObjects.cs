using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collideable
{
    public abstract class CollideableObjects : MonoBehaviour
    {

        [Header("COLLIDEABLE OBJECT ATTRIBUTES")]
        [Space(10)]
        public bool impacted = false;


        //This gets called whenver an object is spawned
        public abstract void OnSpawn();



        /// <summary>
        /// This gets called whenever an object is despawned
        /// </summary>
        public abstract void OnDeSpawn();


        /// <summary>
        /// This gets called whenver an object is collided
        /// </summary>
        /// <param name="animalColorType"></param>
        public abstract void OnImpact();
  

    }


}

