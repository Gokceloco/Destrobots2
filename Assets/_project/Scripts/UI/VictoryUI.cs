using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VictoryUI : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1, .2f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
