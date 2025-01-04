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

    public PlayerHand playerHand;

    public float smoothTime;
    private Vector3 _velocity;

    public int startHealth;
    private int _currentHealth;

    public HealthBar healthBar;

    public ParticleSystem hitPS;

    public List<Weapon> weapons;

    public void RestartPlayer()
    {
        transform.position = Vector3.zero;
        gameObject.SetActive(true);
        _currentHealth = startHealth;
        healthBar.UpdateHealthBar(1);
        GetComponent<PlayerMovement>().ResetPlayerMovement();
        playerHand.RestartPlayerHand();
        weapons.Clear();
        foreach (var w in GetComponentsInChildren<Weapon>())
        {
            weapons.Add(w);
        }
        ChangeWeapon(0);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallCollider"))
        {
            Die();
        }
    }

    private void Die()
    {
        gameDirector.LevelFailed();
        gameObject.SetActive(false);
    }

    public void DoorIsLocked()
    {
        gameDirector.DoorIsLocked();
    }

    private void FixedUpdate()
    {
        var pos = transform.position + transform.forward * cameraOffsetByLookDirection;
        cameraHolder.transform.position             
            = Vector3.SmoothDamp(cameraHolder.transform.position, pos, ref _velocity, smoothTime);
    }

    public void ThrowGrenade()
    {
        GetComponent<PlayerMovement>().PlayThrowGrenadeAnimation();
    }

    public void ChangeWeapon(int i)
    {
        GetComponent<PlayerMovement>().PlayWeaponChangeAnimation();

        StartCoroutine(ChangeWeaponDelayed(.3f, i));
    }

    IEnumerator ChangeWeaponDelayed(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        foreach (var w in weapons)
        {
            w.gameObject.SetActive(false);
        }
        weapons[index].gameObject.SetActive(true);
    }
}
