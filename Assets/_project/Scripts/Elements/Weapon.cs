using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Weapon : MonoBehaviour
{
    public GameDirector gameDirector;
    public Player player;
    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;
    public float attackRate;

    public ParticleSystem muzzleFlashPS;
    public Light muzzleFlashLight;
    public float muzzleFlashLightDuration;
    public float muzzleFlashLightIntensity;

    private float lastShootTime;


    void Update()
    {
        if (gameDirector.gameState == GameState.Play 
            && Input.GetMouseButton(0) 
            && Time.time - lastShootTime > attackRate)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.LookAt(newBullet.transform.position + bulletSpawnPoint.forward * 10);
        newBullet.StartBullet(gameDirector);

        muzzleFlashPS.Play();
        ActivateMuzzleFlashLight();
        gameDirector.audioManager.PlayGunSotSFX();
        Invoke(nameof(DeactivateMuzzleFlashLight), muzzleFlashLightDuration);

        lastShootTime = Time.time;

        player.cameraHolder.ShakeCamera(.25f, .15f);
    }

    private void ActivateMuzzleFlashLight()
    {
        muzzleFlashLight.intensity = muzzleFlashLightIntensity;
    }
    private void DeactivateMuzzleFlashLight()
    {
        muzzleFlashLight.intensity = 0f;
    }
}
