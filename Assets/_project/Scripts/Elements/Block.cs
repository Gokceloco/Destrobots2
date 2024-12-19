using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Enemy enemyPrefab;

    private int _blockNo;

    public int enemyCount;

    private Player _player;

    public List<Enemy> enemiesInMap;

    public void StartBlock(int no, Player player)
    {
        _blockNo = no;
        _player = player;
        GenerateEnemies();
        StartEnemiesInMap();
    }

    private void StartEnemiesInMap()
    {
        foreach (Enemy e in enemiesInMap)
        {
            e.StartEnemy(_player, this);
        }
    }

    private void GenerateEnemies()
    {
        var difficultyScale = _blockNo / 3;

        if (_blockNo == 0) 
        {
            enemyCount = 0;
        }
        else
        {
            enemyCount = difficultyScale + 1;
        }

        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy = Instantiate(enemyPrefab, transform);
            newEnemy.transform.localPosition = new Vector3(i - enemyCount / 2f, 0, 3.5f);
            newEnemy.StartEnemy(_player, this);
        }
    }
}
