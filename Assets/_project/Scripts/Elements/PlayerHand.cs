using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Door door;

    public bool haveKey;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && door != null)
        {
            door.OpenCloseDoor(haveKey);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            door = other.GetComponent<Door>();
        }
        if (other.CompareTag("Key"))
        {
            haveKey = true;
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            door = null;
        }
    }
}
