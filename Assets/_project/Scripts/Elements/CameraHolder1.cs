using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder1 : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        var pos = player.position;
        pos.x = 0;
        transform.position = pos;
    }
}
