using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.VersionControl;
using DG.Tweening;

public class MessageUI : MonoBehaviour
{
    public TextMeshProUGUI msgTMP;

    public void Show()
    {
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1,.5f);
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().DOFade(0, .2f).OnComplete(SetActiveFalse);
    }

    void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

    public void ShowMessage(string msg, float duration)
    {
        Show();
        msgTMP.text = msg;
        Invoke(nameof(Hide), duration);
    }
}
