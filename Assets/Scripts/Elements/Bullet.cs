using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _rb.position += transform.forward * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
