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
        levelManager.SetCurLevel();
        RestartLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            levelManager.ResetCurLevel();
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadNextLevelButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadPreviousLevel();
        }
        _remainingTime = startTime - Time.time + _levelStartTime;
        mainUI.UpdateTimer(_remainingTime / startTime);
        if (_remainingTime <= 0)
        {
            LevelFailed();
        }
    }

    private void LoadPreviousLevel()
    {
        var levelNo = PlayerPrefs.GetInt("CurrentLevel") - 1;
        if (levelNo < 1)
        {
            levelNo = 1;
        }
        PlayerPrefs.SetInt("CurrentLevel", levelNo);
        RestartLevel();
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
        levelManager.GenerateLevelNew();
        player.RestartPlayer();
        mainUI.RestartMainUI();
        RestartTimer();
        _levelStartTime = Time.time;
        mainUI.failUI.Hide();
        mainUI.victoryUI.Hide();
        mainUI.levelUI.SetLevelTMP(levelManager.GetCurLevel());
    }

    public void LoadNextLevelButtonPressed()
    {
        levelManager.IncreaseLevel();
        RestartLevel();
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
        if(startTime - Time.time + _levelStartTime > startTime)
        {
            var extraTime = startTime - Time.time + _levelStartTime - startTime;
            _levelStartTime -= extraTime;
        }
    }

    public void LevelCompleted()
    {
        gameState = GameState.VictoryUI;
        mainUI.ShowVictoryUI();
    }
}

public enum GameState
{
    Play,
    FailUI,
    VictoryUI,
    GameMenu,
}
