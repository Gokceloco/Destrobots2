using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float speed;
    private Vector3 _mousePivotPos;
    private Vector3 _playerPivotPos;
    public float ratio;
    void FixedUpdate()
    {
        var direction = Vector3.forward;
        if (Input.GetMouseButtonDown(0))
        {
            
            _playerPivotPos = transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            var dragVector = Input.mousePosition - _mousePivotPos;
            var playerDrag = dragVector * .02f;
            playerDrag.y = 0;
            playerDrag.z = 0;
            _mousePivotPos = Input.mousePosition;

            direction += playerDrag;

        }
        if (Input.GetMouseButtonUp(0))
        {
            _mousePivotPos = Vector3.zero;
        }

        transform.position += direction * Time.fixedDeltaTime * speed;
    }
}
