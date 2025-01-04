using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private GameDirector _gameDirector;
    public List<GameObject> inventoryItems;
    public float distanceBetweenItems;

    private bool _isShowing;

    public Image inventoryGBImage;

    public void StartInventoryUI(GameDirector gameDirector)
    {
        _gameDirector = gameDirector;
        CloseInventory();
    }

    public void ToggleOpenCloseInventory()
    {
        if (_isShowing)
        {
            CloseInventory();
            _isShowing = false;
        }
        else
        {
            OpenInventory();
            _isShowing = true;
        }
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

    void OpenInventory()
    {
        inventoryGBImage.color = new Color(.3f, .8f, 1f, 1f);
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryItems[i].gameObject.SetActive(true);
            var rectTransform = inventoryItems[i].GetComponent<RectTransform>();
            rectTransform.DOKill();
            rectTransform.DOAnchorPosY(
                - distanceBetweenItems * (i + 1), .25f);
        }
    }

    void CloseInventory()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            var rectTransform = inventoryItems[i].GetComponent<RectTransform>();
            rectTransform.DOKill();
            rectTransform.DOAnchorPosY(0, .25f)
                .OnComplete(()=>HideInventoryItem(rectTransform.gameObject));
        }
    }

    void HideInventoryItem(GameObject item)
    {
        item.SetActive(false);
        inventoryGBImage.color = new Color(.75f, .75f, .75f, .65f);
    }

    public void Inventory1ButtonPressed()
    {
        _gameDirector.player.ChangeWeapon(0);
    }
    public void Inventory2ButtonPressed()
    {
        _gameDirector.player.ChangeWeapon(1);
    }
}
