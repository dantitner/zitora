using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraFollow : NetworkBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {

        if (target != null)
        {
            transform.position = Vector3.Lerp(target.position + offset, target.position, smoothSpeed);
        }
        
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
