using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource gunShotAS;
    public AudioSource zombieImpactAS;
    public AudioSource music1LoopAS;
    public void PlayGunSotSFX()
    {
        gunShotAS.Play();
    }
    public void PlayZombieImpactSFX()
    {
        zombieImpactAS.Play();
    }
}