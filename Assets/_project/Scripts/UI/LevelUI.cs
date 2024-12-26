using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI levelTMP;
    public void SetLevelTMP(int levelNo) 
    {
        levelTMP.text = "LEVEL " + levelNo;
    }
    public void Show()
    {
        gameObject.SetActive(true);
        GetComponent<CanvasGroup>().DOFade(1, .2f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
