using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool doesPlayerLookAtMouse;
    public float playerMoveSpeed;
    private Rigidbody _rb;

    public LayerMask playerLookAtRayLayerMask;

    public GameObject clickSprite;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        MovePlayer();
        if (doesPlayerLookAtMouse)
        {
            LookAtMouse();
        }
    }

    private void LookAtMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, playerLookAtRayLayerMask))
        {
            transform.LookAt(hit.point);
        }
    }

    void MovePlayer()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        _rb.position += direction.normalized * playerMoveSpeed * Time.deltaTime;
    }
}
