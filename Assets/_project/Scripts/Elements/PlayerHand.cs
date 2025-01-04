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

    public void RestartPlayerHand()
    {
        acquiredKeys.Clear();
        if (carryingCrate != null)
        {
            Destroy(carryingCrate.gameObject);
            carryingCrate = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touchingDoor != null)
        {
            if (haveKey && touchingDoor.isDoorLocked)
            {
                haveKey = false;
                touchingDoor.OpenCloseDoor(acquiredKeys, player);
            }
            else
            {
                touchingDoor.OpenCloseDoor(acquiredKeys, player);
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
        player.gameDirector.audioManager.PlayWhooshSFX();
    }

    private void PickUpCrate()
    {
        _lastPickedBlock = touchingCrate.parent;
        touchingCrate.SetParent(transform);
        touchingCrate.transform.DOKill();
        touchingCrate.transform.DOLocalMove(new Vector3(0, 1.5f, -1), .2f);
        touchingCrate.transform.DOLocalRotate(new Vector3(0, 90, 0), .2f);
        carryingCrate = touchingCrate;
        if (player.gameDirector.levelManager.GetCurLevel() == 1)
        {
            player.gameDirector.mainUI.ShowMessage("<color=#FF7D00>E</color> TO DROP!", 3f);
        }
        player.gameDirector.audioManager.PlayWhooshSFX();
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
            var door = other.GetComponent<Door>();
            if (door != null && other.GetComponent<Door>().showTutorial)
            {
                player.gameDirector.mainUI.ShowMessage("<color=#FF7D00>E</color> TO INTERACT!", 3f);
            }
            touchingDoor = other.GetComponent<Door>();
        }
        if (other.CompareTag("Key"))
        {
            player.gameDirector.mainUI.ShowMessage("KEY IS PICKED UP!", 3f);
            acquiredKeys.Add(other.GetComponentInParent<Key>().keyType);
            player.gameDirector.fxManager.PlayKeyPickedUpPS(other.transform.position);
            other.transform.parent.gameObject.SetActive(false);
            player.gameDirector.audioManager.PlayPickUpPositiveSFX();
        }
        if (other.CompareTag("Crate"))
        {
            if (player.gameDirector.levelManager.GetCurLevel() == 1)
            {
                player.gameDirector.mainUI.ShowMessage("<color=#FF7D00>E</color> TO INTERACT!", 3f);
            }
            touchingCrate = other.transform.parent;
        }
        if (other.CompareTag("Collectable"))
        {
            var collectable = other.GetComponentInParent<Collectable>();
            if (collectable.collectableType == CollectableType.Serum)
            {
                player.gameDirector.fxManager.PlaySerumPickedUpPS(other.transform.position);
                player.gameDirector.audioManager.PlayPositiveSFX();
                other.transform.parent.gameObject.SetActive(false);
                if (collectable.isLevelEndSerum)
                {
                    player.gameDirector.LevelCompleted();
                }
                else
                {
                    player.gameDirector.SerumCollected();
                }
            }
        }
        if (other.CompareTag("MessageTrigger"))
        {
            player.gameDirector.mainUI.ShowMessage(other.GetComponent<MessageTrigger>().msg, 3f);
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
