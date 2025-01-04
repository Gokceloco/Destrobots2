using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource gunShotAS;
    public AudioSource zombieImpactAS;
    public AudioSource positiveAS;
    public AudioSource buttonPressedAS;
    public AudioSource zombieAttackAS;
    public AudioSource doorOpenAS;
    public AudioSource doorIsLockedAS;
    public AudioSource pickUpPositiveAS;
    public AudioSource jumpAS;
    public AudioSource explosionAS;
    public AudioSource music1LoopAS;
    public void PlayGunSotSFX()
    {
        gunShotAS.Play();
    }
    public void PlayZombieImpactSFX()
    {
        zombieImpactAS.Play();
    }
    public void PlayPositiveSFX()
    {
        positiveAS.Play();  
    }
    public void PlayPickUpPositiveSFX()
    {
        pickUpPositiveAS.Play();  
    }
    public void PlayButtonPressedSFX()
    {
        buttonPressedAS.Play();  
    }
    public void PlayZombieAttackSFX()
    {
        zombieAttackAS.Play();  
    }
    public void PlayDoorOpenSFX()
    {
        doorOpenAS.Play();  
    }

    public void PlayDoorIsLockedSFX()
    {
        doorIsLockedAS.Play();
    }
    public void PlayWhooshSFX()
    {
        jumpAS.Play();
    }

    public void PlayExplosionAS()
    {
        explosionAS.Play();
    }
}
