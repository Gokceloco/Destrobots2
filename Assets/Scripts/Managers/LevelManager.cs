using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public int mapLength;

    public Block blockPrefab;
    public List<Block> blocks;

    public Enemy enemyPrefab;
    public void DeleteLevel()
    {
        
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < mapLength; i++)
        {
            var newBlock = Instantiate(blockPrefab);
            newBlock.transform.position = new Vector3(0, 0, 10 * i);
            newBlock.StartBlock(i, player);
        }        
    }
}
