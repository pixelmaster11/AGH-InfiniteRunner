using System.Collections.Generic;
using UnityEngine;
//using Collideable.ObstacleSystem;
//using Collideable;

public class CharacterCollision : MonoBehaviour
{
//    [SerializeField]
//    Vector3 colliderHalfExtents = new Vector3(0.6f, 0.26f, 0.8f);

//    [SerializeField]
//    Vector3 colliderOffset = new Vector3(0 , 0.2f, 0.2f);

//    [SerializeField]
//    Color colliderColor = Color.red;

//    [SerializeField]
//    Transform groundCheck;

//    [SerializeField]
//    float sphereRadius;


//    [SerializeField]
//    LayerMask groundLayer;


//    [SerializeField]
//    LayerMask animalLayer;



//    [SerializeField]
//    LayerMask colliderLayers;

//    [SerializeField]
//    Enums.ColorType animalColor;

//    RaycastHit hit;

//    private Collider[] collidedObjects;


//    private void FixedUpdate()
//    {
//        CollisionEvents();
//    }


//    public bool GroundCollision()
//    {


//        if (Physics.OverlapSphere(groundCheck.position, sphereRadius, groundLayer).Length >= 1)
//        {
//            return true;
//        }

//        else
//        {
//            return false;
//        }

//    }


   

//    private void CollisionEvents()
//    {
//        //Collision Checks
//        collidedObjects = Physics.OverlapBox(transform.position + colliderOffset,
//                            colliderHalfExtents, transform.rotation, colliderLayers);

//        //if (Physics.BoxCast(transform.position + colliderOffset, colliderHalfExtents, transform.forward, 
//        //                    out hit, transform.rotation, 0.1f, colliderLayers))
//        //{
//        //    //Get the collided object
//        //    CollideableObjects obj = hit.collider.GetComponent<CollideableObjects>();


//        //    //If not already impacted
//        //    if (!obj.impacted)
//        //    {
//        //        Utils.DebugUtils.Log("Collided with: " + obj.name);

//        //        //Call the impact event
//        //        EventManager.CallOnImpactEvent(ref obj, animalColor);



//        //    }
//        //}

//        if (collidedObjects.Length > 0)
//        {

//            for (int i = 0; i < collidedObjects.Length; i++)
//            {

//                //Get the obstacle
//                CollideableObjects obj = collidedObjects[i].GetComponent<CollideableObjects>();


//                //If not already impacted
//                if (!obj.impacted)
//                {
//                    Utils.DebugUtils.Log("Obstacle Collision: " + obj.name);

//                    //Call obstacle impact event
//                    EventManager.CallOnImpactEvent(ref obj, animalColor);

//                    break;

//                }



//            }



//        }




//    }


  


//    //public bool CrossJumpCollision()
//    //{
//    //    //Collision Checks
//    //    Collider[] c = Physics.OverlapBox(transform.position + colliderOffset, colliderHalfExtents, transform.rotation, colliderLayers);
//    //    bool crossJumpCollision = false;

//    //    if (c.Length > 0)
//    //    {

//    //        for (int i = 0; i < c.Length; i++)
//    //        {
//    //            if (c[i].name != this.name)
//    //            {          
//    //               //If animal collision
//    //                if (c[i].gameObject.layer == animalLayer.value)
//    //                {
//    //                    crossJumpCollision = true;
//    //                }
//    //            }
//    //        }

//    //    }

//    //    return crossJumpCollision;
//    //}


//#if UNITY_EDITOR

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = colliderColor;

//        if(groundCheck != null)
//            Gizmos.DrawSphere(groundCheck.position, sphereRadius);

        
//        Gizmos.DrawWireCube(transform.position + colliderOffset , colliderHalfExtents * 2);

//    }

//#endif
}
