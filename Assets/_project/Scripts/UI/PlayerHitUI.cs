using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitUI : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }
    public void PlayGetHitFX()
    {
        _canvasGroup.DOKill();
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(.5f, .1f).SetLoops(2, LoopType.Yoyo);
    }
}
