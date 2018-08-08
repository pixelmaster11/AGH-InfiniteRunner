﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCamera : MonoBehaviour
{

    [SerializeField]
    Transform lookAt;

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    float smoothDamp;

    Vector3 velocity = Vector3.forward;

    

    private void LateUpdate()
    {
        Vector3 targetPos = lookAt.position + offset;
        targetPos.y = transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothDamp * Time.deltaTime);
    }

}