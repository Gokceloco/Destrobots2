using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int startHealth;
    private int _currentHealth;

    private Player _player;
    private Rigidbody _rb;

    private bool _didSeePlayer;
    private bool _isWalking;

    public HealthBar healthBar;

    public Animator animator;

    public void StartEnemy(Player player)
    {
        _player = player;
        _rb = GetComponent<Rigidbody>();
        _currentHealth = startHealth;
    }

    public void GetHit(int damage, Vector3 direction, float pushForce)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        _rb.AddForce(direction * pushForce, ForceMode.Impulse);
        healthBar.UpdateHealthBar((float)_currentHealth / startHealth);
    }

    private void Update()
    {
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
                    animator.SetTrigger("Walk");
                    _isWalking = true;
                }
            }
        }        
    }
}
