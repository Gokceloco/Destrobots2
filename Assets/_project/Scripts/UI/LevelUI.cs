using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI levelTMP;
    public void SetLevelTMP(int levelNo) 
    {
        levelTMP.text = "LEVEL " + levelNo;
    }
}
