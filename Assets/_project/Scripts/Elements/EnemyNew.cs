using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNew : MonoBehaviour
{
    public int enemyType;
    public List<GameObject> enemyMeshes;
    void Start()
    {
        foreach (GameObject e in enemyMeshes) 
        { 
            e.SetActive(false);
        }
        enemyMeshes[enemyType].SetActive(true);
    }
}
