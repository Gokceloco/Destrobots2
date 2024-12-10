using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameDirector _gameDirector;
    public float bulletSpeed;
    public int damage;
    public float pushForce;
    public float maxDistance;

    private Rigidbody _rb;
    public void StartBullet(GameDirector gameDirector)
    {
        _rb = GetComponent<Rigidbody>();
        _gameDirector = gameDirector;
    }

    void Update()
    {
        _rb.position += transform.forward * Time.deltaTime * bulletSpeed;
        if ((transform.position - _gameDirector.player.transform.position).magnitude > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().GetHit(damage, transform.forward, pushForce);
            _gameDirector.fxManager.PlayBulletHitPS(transform.position, Color.red);
            _gameDirector.audioManager.PlayZombieImpactSFX();
            Destroy(gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            _gameDirector.fxManager.PlayBulletHitPS(transform.position, Color.gray);
            Destroy(gameObject);
        }
        if (other.CompareTag("Door") && other.GetComponent<Door>().isDoorClosed)
        {
            _gameDirector.fxManager.PlayBulletHitPS(transform.position, Color.gray);
            Destroy(gameObject);
        }
    }
}
