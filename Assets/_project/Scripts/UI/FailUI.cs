using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailUI : MonoBehaviour
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
