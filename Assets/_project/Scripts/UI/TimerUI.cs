using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Image fillImage;
    public void RestartTimer()
    {
        fillImage.fillAmount = 1;
    }
    public void UpdateTimer(float ratio)
    {
        fillImage.fillAmount = ratio;
    }
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
