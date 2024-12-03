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

    public float jumpForce;

    public bool isTouchingGround;
    public float rayDistance;
    public LayerMask groundLayerMask;

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

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, rayDistance, groundLayerMask))
        {
            isTouchingGround = true;
        }
        else
        {
            isTouchingGround = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * jumpForce);
    }

    private void LookAtMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, playerLookAtRayLayerMask))
        {
            var lookPos = hit.point;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
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
        if (!Input.anyKey)
        {
            var v = _rb.velocity;
            v.x = 0;
            v.z = 0;
            _rb.velocity = v;
        }
        _rb.position += direction.normalized * playerMoveSpeed * Time.deltaTime;
    }
}
