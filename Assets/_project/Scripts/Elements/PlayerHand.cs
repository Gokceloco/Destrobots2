using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Player player;
    public Door door;
    public Transform touchingCrate;
    public Transform carryingCrate;

    public bool haveKey;

    public List<KeyType> acquiredKeys;

    public void StartPlayerHand()
    {
        acquiredKeys.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && door != null)
        {
            if (haveKey && door.isDoorLocked)
            {
                haveKey = false;
                door.OpenCloseDoor(acquiredKeys);
            }
            else
            {
                door.OpenCloseDoor(acquiredKeys);
            }
            if (!haveKey && door.isDoorLocked)
            {
                player.DoorIsLocked();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && touchingCrate != null)
        {
            touchingCrate.SetParent(transform);
            touchingCrate.transform.DOKill();
            touchingCrate.transform.DOLocalMove(new Vector3(0,1.5f, -1), .2f);
            touchingCrate.transform.DOLocalRotate(new Vector3(0,90,0), .2f);
            carryingCrate = touchingCrate;
        }
        else if (Input.GetKeyDown(KeyCode.E) && carryingCrate != null)
        {
            carryingCrate.transform.DOKill();
            carryingCrate.transform.DOLocalMove(new Vector3(0, -1, 1), .2f)
                .OnComplete(()=>SetTransformToWorld(carryingCrate));
            
        }
    }

    void SetTransformToWorld(Transform t)
    {
        t.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            door = other.GetComponent<Door>();
        }
        if (other.CompareTag("Key"))
        {
            acquiredKeys.Add(other.GetComponentInParent<Key>().keyType);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Crate"))
        {
            touchingCrate = other.transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            door = null;
        }
        if (other.CompareTag("Crate"))
        {
            touchingCrate = null;
        }
    }
}
