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
    public VictoryUI victoryUI;
    public LevelUI levelUI;
    public MainMenu mainMenu;

    public void RestartMainUI()
    {
        var time = gameDirector.startTime;
        var intTime = Mathf.RoundToInt(time);
        var min = intTime / 60;
        var sec = intTime % 60;
        if (sec == 0)
        {
            ShowMessage("YOU'VE GOT <color=#FF7D00>" + min + ":00" + " MIN</color> TO GET THE SERUM!", 3, .3f);
        }
        else
        {
            ShowMessage("YOU'VE GOT <color=#FF7D00>" + min + ":" + sec + " MIN</color> TO GET THE SERUM!", 3, .3f);
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
        timerUI.Hide();
        levelUI.Hide();
    }

    public void ShowVictoryUI()
    {
        victoryUI.Show();
        timerUI.Hide();
        levelUI.Hide();
    }
}
