using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isDoorLocked;

    public Transform leftDoor;
    public Transform rightDoor;

    public bool isDoorClosed = true;

    public KeyType requiredKey;

    public MeshRenderer doorColorPieceMR;

    public Material lockedDoorMaterial;
    public Material unLockedDoorMaterial;

    public bool showTutorial;

    private void Start()
    {
        if (isDoorLocked)
        {
            doorColorPieceMR.material = lockedDoorMaterial;
        }
        else
        {
            doorColorPieceMR.material = unLockedDoorMaterial;
        }
    }

    public void OpenCloseDoor(List<KeyType> acquiredKey, Player player)
    {
        var haveKey = false;
        foreach (var key in acquiredKey) 
        {
            if (key == requiredKey)
            {
                haveKey = true;
            }
        }
        if (isDoorLocked && !haveKey)
        {
            player.gameDirector.audioManager.PlayDoorIsLockedSFX();
            return;
        }
        if (isDoorClosed)
        {
            leftDoor.transform.DOLocalMoveZ(leftDoor.transform.localPosition.z + 1, .3f);
            rightDoor.transform.DOLocalMoveZ(rightDoor.transform.localPosition.z - 1, .3f);
            isDoorClosed = false;
            isDoorLocked = false;
            player.gameDirector.audioManager.PlayDoorOpenSFX();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isDoorClosed)
        {
            CloseDoor();
        }
    }

    private void CloseDoor()
    {
        leftDoor.transform.DOLocalMoveZ(leftDoor.transform.localPosition.z - 1, .3f);
        rightDoor.transform.DOLocalMoveZ(rightDoor.transform.localPosition.z + 1, .3f);
        isDoorClosed = true;
    }
}
