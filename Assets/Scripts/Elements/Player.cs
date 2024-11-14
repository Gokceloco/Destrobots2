using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform cameraHolder;

    public float smoothTime;
    private Vector3 _velocity;

    private void FixedUpdate()
    {
        var pos = transform.position;
        pos.y = 0f;
        cameraHolder.position = Vector3.SmoothDamp(cameraHolder.position, pos, ref _velocity, smoothTime);
    }
}
