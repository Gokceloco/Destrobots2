using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public GameDirector gameDirector;
    public MessageUI messageUI;
    public PlayerHitUI playerHitUI;
    public TimerUI timerUI;
    public FailUI failUI;

    public void RestartMainUI()
    {
        var time = gameDirector.startTime;
        var intTime = Mathf.RoundToInt(time);
        var min = intTime / 60;
        var sec = intTime % 60;
        if (sec == 0)
        {
            ShowMessage("YOU'VE GOT " + min + ":00" + " MIN TO GET THE SERUM!", 3, .3f);
        }
        else
        {
            ShowMessage("YOU'VE GOT " + min + ":" + sec + " MIN TO GET THE SERUM!", 3, .3f);
        }
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
