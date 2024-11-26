using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    public ParticleSystem bulletHitPS;
    public void PlayBulletHitParticles(Vector3 pos, Color color)
    {
        var newPS = Instantiate(bulletHitPS);
        newPS.transform.position = pos;
        var main = newPS.main;
        main.startColor = color;
        newPS.Play();
    }
}
