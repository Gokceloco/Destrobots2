using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public KeyType keyType;

    private void Start()
    {
        if (keyType == KeyType.Yellow)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
        }
        else if (keyType == KeyType.Blue)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        }
        else if (keyType == KeyType.Orange)
        {
            GetComponentInChildren<MeshRenderer>().material.color = new Color(1f,.6f,0,1);
        }
    }
}

public enum KeyType
{
    Yellow,
    Blue,
    Orange,
}