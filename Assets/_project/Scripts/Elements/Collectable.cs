using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableType collectableType;
    public void StartCollectable()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        transform.DOMoveY(transform.position.y + 1, .5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}

public enum CollectableType
{
    Coin,
    Serum,
    Heal,
    AttackSpeed,
    Damage,
}
