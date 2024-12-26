using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MessageUI : MonoBehaviour
{
    public TextMeshProUGUI msgTMP;

    public void Show(float delay)
    {
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1,.5f).SetDelay(delay);
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().DOFade(0, .2f).OnComplete(SetActiveFalse);
    }

    void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

    public void ShowMessage(string msg, float duration, float delay = 0)
    {
        Show(delay);
        msgTMP.text = msg;
        Invoke(nameof(Hide), duration);
    }
}
