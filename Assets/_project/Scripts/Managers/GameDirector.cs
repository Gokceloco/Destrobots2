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
    }

    private void RestartLevel()
    {
        levelManager.DeleteLevel();
        levelManager.GenerateLevel();
        player.RestartPlayer();
        mainUI.RestartMainUI();
    }

    public void DoorIsLocked()
    {
        mainUI.ShowMessage("DOOR IS LOCKED! FIND THE KEY", 5);
    }
}
