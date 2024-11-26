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

    public void OpenCloseDoor(bool haveKey)
    {
        if (isDoorLocked && !haveKey)
        {
            return;
        }
        if (isDoorClosed)
        {
            leftDoor.transform.DOMoveX(leftDoor.transform.position.x - 2, .3f);
            rightDoor.transform.DOMoveX(rightDoor.transform.position.x + 2, .3f);
            isDoorClosed = false;
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
