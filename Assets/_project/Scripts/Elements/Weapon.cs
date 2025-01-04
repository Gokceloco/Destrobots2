using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using Random = UnityEngine.Random;

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

    public Grenade grenadePrefab;

    public Transform leftHand;
    public float grenadeThrowDistance;
    public float grenadeThrowDuration;

    public Vector3 rotationVector;

    public WeaponType weaponType;

    void Update()
    {
        if (gameDirector.gameState == GameState.Play 
            && weaponType == WeaponType.Machinegun
            && Input.GetMouseButton(0) 
            && Time.time - lastShootTime > attackRate
            && !IsMouseOverUI())
        {
            ShootForMachineGun();
        }
        
        if (gameDirector.gameState == GameState.Play 
            && weaponType == WeaponType.Shotgun
            && Input.GetMouseButtonUp(0) 
            && Time.time - lastShootTime > attackRate
            && !IsMouseOverUI())
        {
            ShootForShotGun();
        }

        if (gameDirector.gameState == GameState.Play && Input.GetMouseButtonDown(1))
        {
            ThrowGrenade();
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void ThrowGrenade()
    {
        player.ThrowGrenade();
        Invoke(nameof(InstantiateGrenade), .15f);        
    }

    void InstantiateGrenade()
    {
        player.gameDirector.audioManager.PlayWhooshSFX();

        var newGrenade = Instantiate(grenadePrefab);
        newGrenade.transform.position = leftHand.position;
        newGrenade.StartGrenade(gameDirector);

        newGrenade.transform.DOLocalRotate(rotationVector, grenadeThrowDuration, RotateMode.LocalAxisAdd);
        newGrenade.transform
            .DOMoveX((transform.position + player.transform.forward * grenadeThrowDistance).x,
                grenadeThrowDuration).SetEase(Ease.Linear);
        newGrenade.transform
            .DOMoveZ((transform.position + player.transform.forward * grenadeThrowDistance).z,
                grenadeThrowDuration).SetEase(Ease.Linear);
        newGrenade.transform.DOMoveY(transform.position.y + 2, grenadeThrowDuration * .5f)
            .SetEase(Ease.OutQuad);
        newGrenade.transform.DOMoveY(player.transform.position.y + .25f, grenadeThrowDuration * .5f)
            .SetDelay(grenadeThrowDuration * .5f).SetEase(Ease.InQuad).OnComplete(newGrenade.Explode);
    }

    private void ShootForMachineGun()
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

        player.cameraHolder.ShakeCamera(.25f, .25f);
    }

    void ShootForShotGun()
    {
        for (int i = 0; i < 10; i++)
        {
            var newBullet = Instantiate(bulletPrefab);
            newBullet.transform.position = bulletSpawnPoint.position;
            var forwardVector = bulletSpawnPoint.forward * 10 + Vector3.up * Random.Range(-1f,1f);
            forwardVector = Quaternion.AngleAxis(Random.Range(-15,15), Vector3.up) * forwardVector; 
            newBullet.transform.LookAt(newBullet.transform.position + forwardVector);
            newBullet.StartBullet(gameDirector);

            muzzleFlashPS.Play();
            ActivateMuzzleFlashLight();
            gameDirector.audioManager.PlayGunSotSFX();
            Invoke(nameof(DeactivateMuzzleFlashLight), muzzleFlashLightDuration);

            lastShootTime = Time.time;

            player.cameraHolder.ShakeCamera(.8f, .8f);
        }
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

public enum WeaponType
{
    Machinegun,
    Shotgun,
}
