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

    public int startHealth;
    private int _currentHealth;

    public HealthBar healthBar;

    public ParticleSystem hitPS;

    public void RestartPlayer()
    {
        transform.position = Vector3.zero;
        gameObject.SetActive(true);
        _currentHealth = startHealth;
        healthBar.UpdateHealthBar(1);
        GetComponent<PlayerMovement>().ResetPlayerMovement();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetHit(1);
        }
    }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
        healthBar.UpdateHealthBar((float)_currentHealth / startHealth);
        hitPS.Play();
        gameDirector.mainUI.playerHitUI.PlayGetHitFX();
        cameraHolder.ShakeCamera(.5f, .5f);  
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
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
