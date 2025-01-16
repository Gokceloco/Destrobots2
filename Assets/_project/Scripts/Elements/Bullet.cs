using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameDirector _gameDirector;
    public float bulletSpeed;
    public int damage;
    public float pushForce;
    public float maxDistance;

    private Rigidbody _rb;
    private bool _isDestroyed;

    public GameObject mesh;
    public ParticleSystem glowPS;
    public TrailRenderer trail;
    public void StartBullet(GameDirector gameDirector)
    {
        _rb = GetComponent<Rigidbody>();
        _gameDirector = gameDirector;
    }

    void Update()
    {
        if ((transform.position - _gameDirector.player.transform.position).magnitude > maxDistance)
        {
            if (!_isDestroyed)
            {
                _isDestroyed = true;
                DestroyBullet();
            }
        }
        else
        {
            _rb.position += transform.forward * Time.deltaTime * bulletSpeed;
        }
    }

    void DestroyBullet()
    {
        mesh.SetActive(false);
        glowPS.Stop();
        glowPS.transform.DOScale(0, .2f);
        trail.emitting = false;
        Destroy(gameObject, .5f);
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
