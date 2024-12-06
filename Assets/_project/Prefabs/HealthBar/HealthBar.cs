using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform fillBarParent;
    public SpriteRenderer fillBar;
    public Color fillColor;

    private void Start()
    {
        fillBar.color = fillColor;
        gameObject.SetActive(false);
    }

    public void UpdateHealthBar(float ratio)
    {
        fillBarParent.localScale = new Vector3(ratio, 1, 1);
        if (ratio <= 0)
        {
            gameObject.SetActive(false);
        }
        else if (ratio >= 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
