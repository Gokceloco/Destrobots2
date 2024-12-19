using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public Player player;
    public LevelManager levelManager;
    public FxManager fxManager;
    public AudioManager audioManager;

    public MainUI mainUI;

    public float startTime;
    private float _remainingTime;
    private float _levelStartTime;

    public GameState gameState;

    private void Start()
    {
        RestartLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        _remainingTime = startTime - Time.time + _levelStartTime;
        mainUI.UpdateTimer(_remainingTime / startTime);
        if (_remainingTime <= 0)
        {
            LevelFailed();
        }
    }

    public void LevelFailed()
    {
        mainUI.ShowFailUI();
        gameState = GameState.FailUI;
    }

    public void RestartLevel()
    {
        gameState = GameState.Play;
        levelManager.DeleteLevel();
        levelManager.GenerateLevel();
        player.RestartPlayer();
        mainUI.RestartMainUI();
        RestartTimer();
        _levelStartTime = Time.time;
        mainUI.failUI.Hide();
    }

    public void DoorIsLocked()
    {
        mainUI.ShowMessage("DOOR IS LOCKED! FIND THE KEY", 5);
    }

    void RestartTimer()
    {
        mainUI.RestartTimer();
    }

    public void SerumCollected()
    {
        _levelStartTime += 20f;
    }
}

public enum GameState
{
    Play,
    FailUI,
    VictoryUI,
    GameMenu,
}
