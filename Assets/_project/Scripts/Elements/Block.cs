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
        foreach (var e in enemiesInMap)
        {
            e.StartEnemy(_player);
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
            newEnemy.StartEnemy(_player);
        }
    }
}
