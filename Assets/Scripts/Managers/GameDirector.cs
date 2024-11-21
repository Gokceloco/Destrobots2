using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public FxManager fxManager;
    private void Start()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        levelManager.DeleteLevel();
        levelManager.GenerateLevel();
    }
}
