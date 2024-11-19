using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.LookAt(newBullet.transform.position + bulletSpawnPoint.forward * 10);

        player.cameraHolder.ShakeCamera(.5f, .25f);
    }
}
