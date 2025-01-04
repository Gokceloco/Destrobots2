using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    public ParticleSystem bulletHitPS;
    public ParticleSystem enemyExpirePS;
    public ParticleSystem keyPickedUpPS;
    public ParticleSystem serumPickedUpPS;

    public void PlaySerumPickedUpPS(Vector3 pos)
    {
        var newPS = Instantiate(serumPickedUpPS);
        newPS.transform.position = pos;
        newPS.Play();
    }

    public void PlayBulletHitPS(Vector3 pos, Color color)
    {
        var newPS = Instantiate(bulletHitPS);
        newPS.transform.position = pos;
        var main = newPS.main;
        main.startColor = color;
        newPS.Play();
    }

    public void PlayEnemyExpirePSDelayed(Transform t, float delay)
    {
        StartCoroutine(PlayEnemyExpirePS(t, delay));
    }

    public void PlayKeyPickedUpPS(Vector3 pos)
    {
        var newPS = Instantiate(keyPickedUpPS);
        newPS.transform.position = pos;
        newPS.Play();
    }

    IEnumerator PlayEnemyExpirePS(Transform t, float delay)
    {
        yield return new WaitForSeconds(delay);
        var newPS = Instantiate(enemyExpirePS);
        newPS.transform.position = t.position;
        newPS.Play();
    }
}
