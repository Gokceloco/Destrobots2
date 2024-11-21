using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameDirector _gameDirector;
    public float bulletSpeed;
    public int damage;
    public float pushForce;

    private Rigidbody _rb;
    public void StartBullet(GameDirector gameDirector)
    {
        _rb = GetComponent<Rigidbody>();
        _gameDirector = gameDirector;
    }

    void Update()
    {
        _rb.position += transform.forward * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().GetHit(damage, transform.forward, pushForce);
            _gameDirector.fxManager.PlayBulletHitParticles(transform.position);
            gameObject.SetActive(false);
        }
    }
}
