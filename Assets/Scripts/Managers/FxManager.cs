using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    public ParticleSystem bulletHitPS;
    public void PlayBulletHitParticles(Vector3 pos)
    {
        var newPS = Instantiate(bulletHitPS);
        newPS.transform.position = pos;
        newPS.Play();
    }
}
