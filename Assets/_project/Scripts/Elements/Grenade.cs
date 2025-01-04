using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private GameDirector _gameDirector;
    public ParticleSystem explosionPS;
    public List<Enemy> enemiesInRange;

    public void StartGrenade(GameDirector gameDirector)
    {
        _gameDirector = gameDirector;
    }
    public void Explode()
    {
        foreach (var e in enemiesInRange)
        {
            e.GetHit(10, Vector3.up, 10f);
        }
        var newExplosion = Instantiate(explosionPS);
        newExplosion.transform.position = transform.position;
        newExplosion.Play();
        _gameDirector.audioManager.PlayExplosionAS();
        _gameDirector.player.cameraHolder.ShakeCamera(1f,1f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") 
            || collision.collider.CompareTag("Enemy") 
            || collision.collider.CompareTag("Wall")
            || collision.collider.CompareTag("Door"))
        {
            Explode();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponentInParent<Enemy>();
            if (!enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.GetComponentInParent<Enemy>());
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
