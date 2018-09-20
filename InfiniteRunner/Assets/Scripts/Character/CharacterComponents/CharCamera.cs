using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem.CharacterComponents
{
    public class CharCamera : MonoBehaviour
    {


        [SerializeField]
        Transform lookAt;

        [SerializeField]
        Vector3 offset;

        [SerializeField]
        float smoothDamp;

        Vector3 velocity = Vector3.forward;


        public void SetLookAt(Transform target)
        {
            lookAt = target;
        }






        private void LateUpdate()
        {
            if (lookAt == null)
            {
                lookAt = this.transform;
            }

            Vector3 targetPos = lookAt.position + offset;
            //targetPos.y = transform.position.y;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothDamp * Time.deltaTime);
        }

    }
}

