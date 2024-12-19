using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public KeyType keyType;
    private Camera _camera;

    private void Start()
    {
        if (keyType == KeyType.Yellow)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
        }
        else if (keyType == KeyType.Blue)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        }
        else if (keyType == KeyType.Orange)
        {
            GetComponentInChildren<MeshRenderer>().material.color = new Color(1f,.6f,0,1);
        }
        StartAnimation();
        _camera = Camera.main;
    }

    private void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        var lookPos = _camera.transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
    }

    private void StartAnimation()
    {
        transform.DOMoveY(transform.position.y + 2, .6f)            
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}

public enum KeyType
{
    Yellow,
    Blue,
    Orange,
}