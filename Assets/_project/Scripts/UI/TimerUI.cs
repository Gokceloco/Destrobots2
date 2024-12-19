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
}
