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

    private Animator _animator;
    private bool _isWalking;

    private RaycastHit _groundRayHit;

    public ParticleSystem jumpPS;

    public void ResetPlayerMovement()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _isWalking = false;
        ResetTriggers();
        _animator.SetTrigger("Idle");
    }

    private void ResetTriggers()
    {
        _animator.ResetTrigger("Walk");
        _animator.ResetTrigger("Idle");
    }

    private void Update()
    {
        if (GetComponent<Player>().gameDirector.gameState != GameState.Play)
        {
            return;
        }
        MovePlayer();
        if (doesPlayerLookAtMouse)
        {
            LookAtMouse();
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, rayDistance, groundLayerMask))
        {
            if (!isTouchingGround)
            {
                _animator.SetTrigger("Idle");
                _isWalking = false;
            }
            isTouchingGround = true;
            _groundRayHit = hit;
        }
        else
        {
            isTouchingGround = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround)
        {
            Jump();
        }

        if (isTouchingGround)
        {
            jumpPS.transform.position = hit.point + Vector3.up * .05f;
        }
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
        _rb.AddForce(Vector3.up * jumpForce);
        GetComponent<Player>().gameDirector.audioManager.PlayWhooshSFX();
        jumpPS.Play();
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

            var gorundNormal = _groundRayHit.normal;

            if (gorundNormal.z < -0.25f)
            {
                gorundNormal.z = -gorundNormal.z;
                gorundNormal.x = -gorundNormal.x;
                direction += gorundNormal;
                var v = _rb.velocity;
                v.y = 0;
                _rb.velocity = v;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;

            var gorundNormal = _groundRayHit.normal;

            if (gorundNormal.z > 0.25f)
            {
                gorundNormal.z = -gorundNormal.z;
                gorundNormal.x = -gorundNormal.x;
                direction += gorundNormal;
                var v = _rb.velocity;
                v.y = 0;
                _rb.velocity = v;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;

            var gorundNormal = _groundRayHit.normal;

            if (gorundNormal.x > 0.25f)
            {
                gorundNormal.z = -gorundNormal.z;
                gorundNormal.x = -gorundNormal.x;
                direction += gorundNormal;
                var v = _rb.velocity;
                v.y = 0;
                _rb.velocity = v;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;

            var gorundNormal = _groundRayHit.normal;

            if (gorundNormal.x < -0.25f)
            {
                gorundNormal.z = -gorundNormal.z;
                gorundNormal.x = -gorundNormal.x;
                direction += gorundNormal;
                var v = _rb.velocity;
                v.y = 0;
                _rb.velocity = v;
            }
        }
        
        if (!Input.anyKey)
        {
            var v = _rb.velocity;
            v.x = 0;
            v.z = 0;
            _rb.velocity = v;
        }
        if (Input.GetKey(KeyCode.W) 
            || Input.GetKey(KeyCode.A) 
            || Input.GetKey(KeyCode.D) 
            || Input.GetKey(KeyCode.S))
        {
            if (!_isWalking)
            {
                _isWalking = true;
                ResetTriggers();
                _animator.SetTrigger("Walk");
            }
        }
        else
        {
            if (_isWalking)
            {
                _isWalking = false;
                ResetTriggers();
                _animator.SetTrigger("Idle");
            }
        }
        var tempSpeed = playerMoveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            tempSpeed = playerMoveSpeed * 2;
        }
        _rb.position += direction.normalized * tempSpeed * Time.deltaTime;
        var angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        _animator.SetFloat("WalkDirection", angle);
    }


    public void PlayThrowGrenadeAnimation()
    {
        //_animator.SetTrigger("ThrowGrenade");
        _animator.CrossFade("Throw Grenade", .1f);
        Invoke(nameof(TriggerGrenadeThrowEndAction), .15f);
    }

    void TriggerGrenadeThrowEndAction()
    {
        if (_isWalking)
        {
            _animator.SetTrigger("Walk");
        }
        else
        {
            _animator.SetTrigger("Idle");
        }
    }

    public void PlayWeaponChangeAnimation()
    {
        _animator.SetTrigger("WeaponChange");
    }
}
