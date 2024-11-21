using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameDirector gameDirector;
    public Player player;
    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;
    public float attackRate;

    public ParticleSystem muzzleFlashPS;

    private float lastShootTime;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time - lastShootTime > attackRate)
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

        lastShootTime = Time.time;

        player.cameraHolder.ShakeCamera(.25f, .15f);
    }
}
