using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public MessageUI messageUI;
    public PlayerHitUI playerHitUI;
    public TimerUI timerUI;
    public FailUI failUI;

    public void RestartMainUI()
    {
        ShowMessage("YOU'VE GOT 10 MIN TO GET THE SERUM!", 3, .3f);
    }
    public void ShowMessage(string msg, float duration, float delay = 0f)
    {
        messageUI.ShowMessage(msg, duration, delay);
    }
    public void RestartTimer()
    {
        timerUI.RestartTimer();
    }
    public void UpdateTimer(float ratio)
    {
        timerUI.UpdateTimer(ratio);
    }

    public void ShowFailUI()
    {
        failUI.Show();
    }
}
