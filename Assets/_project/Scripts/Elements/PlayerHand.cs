using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Player player;
    public Door touchingDoor;
    public Transform touchingCrate;
    public Transform carryingCrate;

    public bool haveKey;

    public List<KeyType> acquiredKeys;

    private Transform _lastPickedBlock;

    public void StartPlayerHand()
    {
        acquiredKeys.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touchingDoor != null)
        {
            if (haveKey && touchingDoor.isDoorLocked)
            {
                haveKey = false;
                touchingDoor.OpenCloseDoor(acquiredKeys);
            }
            else
            {
                touchingDoor.OpenCloseDoor(acquiredKeys);
            }
            if (!haveKey && touchingDoor.isDoorLocked)
            {
                player.DoorIsLocked();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && touchingCrate != null && carryingCrate == null)
        {
            PickUpCrate();
        }
        else if (Input.GetKeyDown(KeyCode.E) && carryingCrate != null)
        {
            DropCrate();
        }
    }

    private void DropCrate()
    {
        carryingCrate.transform.DOKill();
        carryingCrate.transform.DOLocalMove(new Vector3(0, -1, 1), .2f)
            .OnComplete(() => SetTransformToWorld(carryingCrate));
    }

    private void PickUpCrate()
    {
        _lastPickedBlock = touchingCrate.parent;
        touchingCrate.SetParent(transform);
        touchingCrate.transform.DOKill();
        touchingCrate.transform.DOLocalMove(new Vector3(0, 1.5f, -1), .2f);
        touchingCrate.transform.DOLocalRotate(new Vector3(0, 90, 0), .2f);
        carryingCrate = touchingCrate;
        player.gameDirector.mainUI.ShowMessage("HIT E TO DROP OBJECTS!", 3f);
    }

    void SetTransformToWorld(Transform t)
    {
        carryingCrate = null;
        t.SetParent(_lastPickedBlock);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            player.gameDirector.mainUI.ShowMessage("HIT E TO INTERACT WITH OBJECTS!", 3f);
            touchingDoor = other.GetComponent<Door>();
        }
        if (other.CompareTag("Key"))
        {
            player.gameDirector.mainUI.ShowMessage("KEY PICKED UP!", 3f);
            acquiredKeys.Add(other.GetComponentInParent<Key>().keyType);
            player.gameDirector.fxManager.PlayKeyPicekdUpPS(other.transform.position);
            other.transform.parent.gameObject.SetActive(false);
        }
        if (other.CompareTag("Crate"))
        {
            player.gameDirector.mainUI.ShowMessage("HIT E TO INTERACT WITH OBJECTS!", 3f);
            touchingCrate = other.transform.parent;
        }
        if (other.CompareTag("Collectable"))
        {
            var collectable = other.GetComponentInParent<Collectable>();
            if (collectable.collectableType == CollectableType.Serum)
            {
                player.gameDirector.SerumCollected();
                other.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            touchingDoor = null;
        }
        if (other.CompareTag("Crate"))
        {
            touchingCrate = null;
        }
    }
}
