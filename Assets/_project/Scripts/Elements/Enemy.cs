using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int startHealth;
    public CapsuleCollider deadCoolider;
    public Transform chestBone;

    private int _currentHealth;

    private Player _player;
    private Rigidbody _rb;

    private bool _didSeePlayer;

    public HealthBar healthBar;

    private Animator _animator;

    private bool _isWalking;
    private bool _isDead;

    public void StartEnemy(Player player)
    {
        _player = player;
        _rb = GetComponent<Rigidbody>();
        _currentHealth = startHealth;
        _animator = GetComponentInChildren<Animator>();
    }

    public void GetHit(int damage, Vector3 direction, float pushForce)
    {
        if (_isDead)
        {
            return;
        }
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
        _rb.AddForce(direction * pushForce, ForceMode.Impulse);
        healthBar.UpdateHealthBar((float)_currentHealth / startHealth);
    }

    private void Die()
    {
        _isDead = true;
        deadCoolider.gameObject.SetActive(true);
        _animator.SetTrigger("Fall");
        _player.gameDirector.fxManager.PlayEnemyExpirePSDelayed(chestBone, 1.9f);
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }
        if (_player != null)
        {
            var directionVector = _player.transform.position - transform.position;
            var direction = directionVector.normalized;
            var distance = directionVector.magnitude;
            if (distance < 10)
            {
                _didSeePlayer = true;
            }
            if (_didSeePlayer)
            {
                direction.y = 0;
                _rb.position += direction * Time.deltaTime * speed;
                transform.LookAt(_player.transform.position);
                if (!_isWalking)
                {
                    _isWalking = true;
                    _animator.SetTrigger("Walk");
                }
            }
        }        
    }
}
