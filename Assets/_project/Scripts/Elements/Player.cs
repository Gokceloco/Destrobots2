using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;
    public float cameraOffsetByLookDirection;
    public CameraHolder cameraHolder;

    public float smoothTime;
    private Vector3 _velocity;

    public void RestartPlayer()
    {
        transform.position = Vector3.zero;
    }

    public void DoorIsLocked()
    {
        gameDirector.DoorIsLocked();
    }

    private void FixedUpdate()
    {
        var pos = transform.position + transform.forward * cameraOffsetByLookDirection;
        //pos.y = 0f;
        cameraHolder.transform.position             
            = Vector3.SmoothDamp(cameraHolder.transform.position, pos, ref _velocity, smoothTime);
    }
}
