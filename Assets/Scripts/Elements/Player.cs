using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform cameraHolder;

    private void Update()
    {
        cameraHolder.position = transform.position;
    }
}
