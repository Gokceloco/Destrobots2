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

    public void OpenCloseDoor(List<KeyType> acquiredKey)
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
            return;
        }
        if (isDoorClosed)
        {
            leftDoor.transform.DOMoveX(leftDoor.transform.position.x - 2, .3f);
            rightDoor.transform.DOMoveX(rightDoor.transform.position.x + 2, .3f);
            isDoorClosed = false;
            isDoorLocked = false;
        }
        else
        {
            leftDoor.transform.DOMoveX(leftDoor.transform.position.x + 2, .3f);
            rightDoor.transform.DOMoveX(rightDoor.transform.position.x - 2, .3f);
            isDoorClosed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isDoorClosed)
        {
            leftDoor.transform.DOMoveX(leftDoor.transform.position.x + 2, .3f);
            rightDoor.transform.DOMoveX(rightDoor.transform.position.x - 2, .3f);
            isDoorClosed = true;
        }
    }
}
