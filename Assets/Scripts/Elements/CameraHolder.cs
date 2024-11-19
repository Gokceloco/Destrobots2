using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraHolder : MonoBehaviour
{
    public Transform mainCameraTransform;

    public Vector3 originalCameraPosition;

    private void Start()
    {
        originalCameraPosition = mainCameraTransform.localPosition;
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        mainCameraTransform.DOKill();
        mainCameraTransform.localPosition = originalCameraPosition;
        mainCameraTransform.DOShakePosition(duration, magnitude);
    }
}
