using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public int mapLength;

    public List<Block> blockPrefabs;
    public List<Block> blocks;

    public Enemy enemyPrefab;

    private bool _isLastBlockClosed;
    public void DeleteLevel()
    {
        foreach (var b in blocks) 
        { 
            Destroy(b.gameObject);
        }
        blocks.Clear();
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < mapLength; i++)
        {
            var blockRandomizer = UnityEngine.Random.Range(0, 3);
            if (i == 0 || i == 1 || _isLastBlockClosed)
            {
                blockRandomizer = 0;
                _isLastBlockClosed = false;
            }
            else if (i == 2)
            {
                blockRandomizer = UnityEngine.Random.Range(1, 3);
            }
            if (blockRandomizer > 0)
            {
                _isLastBlockClosed = true;
            }
            var newBlock = Instantiate(blockPrefabs[blockRandomizer]);
            newBlock.transform.position = new Vector3(0, 0, 20 * i);
            blocks.Add(newBlock);
            newBlock.StartBlock(i, player);
        }        
    }
}
