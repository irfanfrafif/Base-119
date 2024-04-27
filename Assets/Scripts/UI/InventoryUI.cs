using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryUI : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] RectTransform background;
    [SerializeField] Transform itemContainer;

    [SerializeField] ItemDatabase database;

    private List<InventoryItemContainerUI> items;
    public void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();

        items = new List<InventoryItemContainerUI>();

        int index = 0;
        foreach(Transform child in itemContainer)
        {
            var newItem = child.gameObject.GetComponent<InventoryItemContainerUI>();
            items.Add(newItem);
            newItem.image.sprite = database.itemDatas[index++].sprite;
        }
    }
    public void OnEnable()
    {
        ServiceLocator.Instance.uiManager.isOpeningMenu = true;

        rectTransform.localPosition = new Vector2(0, -Screen.height);
        background.localPosition = new Vector2(0, background.rect.height);

        rectTransform.DOLocalMove(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutQuart);
        background.DOLocalMove(new Vector2(0f, 0f), 1.4f).SetEase(Ease.OutQuart);


        var inventory = ServiceLocator.Instance.inventoryManager;

        for(int i = 0; i < database.itemDatas.Length; i++)
        {
            
            if(!inventory.ContainsItem(database.itemDatas[i].itemID))
            {
                items[i].gameObject.SetActive(false);
                continue;
            }

            items[i].gameObject.SetActive(true);
            items[i].amount.text = inventory.GetAmount(database.itemDatas[i].itemID).ToString();
        }
    }

    public void Close()
    {
        background.DOLocalMove(new Vector2(0f, background.rect.height), 0.5f).SetEase(Ease.OutQuart);
        rectTransform.DOLocalMove(new Vector2(0f, -Screen.height), 0.5f).SetEase(Ease.OutQuart).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        ServiceLocator.Instance.uiManager.isOpeningMenu = false;
        gameObject.SetActive(false);
    }
}
