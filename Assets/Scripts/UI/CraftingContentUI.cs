using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CraftingContentUI : MonoBehaviour
{
    public SubItemUI mainItem;
    public SubItemUI subItem1;
    public SubItemUI subItem2;
    public SubItemUI subItem3;

    public int item1InHand;
    public int item2InHand;
    public int item3InHand;

    public int item1Required;
    public int item2Required;
    public int item3Required;

    public TMP_Text itemName;
    public TMP_Text itemInfo;
    public TMP_Text itemSubInfo;

    public Image infoBackground;

    [SerializeField] MachineUI machineUI;

    public void UpdateAmountText()
    {
        subItem1.info.text = item1InHand.ToString() + "/" + item1Required;
        subItem2.info.text = item2InHand.ToString() + "/" + item2Required;
        subItem3.info.text = item3InHand.ToString() + "/" + item3Required;
    }
    public void UpdateDisplay(MachineryCraftingData data, bool typewriter)
    {
        machineUI.craftPossible = true;

        if (data.craftedItem != null)
        {
            mainItem.image.sprite = data.craftedItem.sprite;
        }

        item1InHand = ServiceLocator.Instance.inventoryManager.GetAmount((data.material1 != null) ? data.material1.itemID : 0); // PAIN
        item2InHand = ServiceLocator.Instance.inventoryManager.GetAmount((data.material2 != null) ? data.material2.itemID : 0); // PAIN
        item3InHand = ServiceLocator.Instance.inventoryManager.GetAmount((data.material3 != null) ? data.material3.itemID : 0); // PAIN

        machineUI.itemIDToRemove = new List<int>();
        machineUI.itemIDToRemoveAmount = new List<int>();


        //Subitem 1
        if (data.material1 != null)
        {
            subItem1.gameObject.SetActive(true);
            subItem1.image.sprite = data.material1.sprite;
            item1Required = data.quantity1;

            if (item1InHand < data.quantity1) machineUI.craftPossible = false;
            machineUI.itemIDToRemove.Add(data.material1.itemID);
            machineUI.itemIDToRemoveAmount.Add(data.quantity1);
        }
        else
        {
            subItem1.gameObject.SetActive(false);
        }

        //Subitem 2
        if (data.material2 != null)
        {
            subItem2.gameObject.SetActive(true);
            subItem2.image.sprite = data.material2.sprite;
            item2Required = data.quantity2;

            if (item2InHand < data.quantity2) machineUI.craftPossible = false;
            machineUI.itemIDToRemove.Add(data.material2.itemID);
            machineUI.itemIDToRemoveAmount.Add(data.quantity2);
        }
        else
        {
            subItem2.gameObject.SetActive(false);
        }

        //Subitem 3
        if (data.material3 != null)
        {
            subItem3.gameObject.SetActive(true);
            subItem3.image.sprite = data.material3.sprite;
            item3Required = data.quantity3;

            if (item3InHand < data.quantity3) machineUI.craftPossible = false;
            machineUI.itemIDToRemove.Add(data.material3.itemID);
            machineUI.itemIDToRemoveAmount.Add(data.quantity3);
        }
        else
        {
            subItem3.gameObject.SetActive(false);
        }

        UpdateAmountText();

        itemName.text = data.craftedItem.itemName.ToUpper();
        //itemInfo.text = data.craftedItem.info;
        itemSubInfo.text = "Time to process: " + ((int)data.duration).ToString() + " seconds";

        if(typewriter)
        {
            itemName.gameObject.GetComponent<typewriterUI_v2>().StartText();
            //itemInfo.gameObject.GetComponent<typewriterUI_v2>().StartText();
            itemSubInfo.gameObject.GetComponent<typewriterUI_v2>().StartText();
        }
        
    }

    private void Awake()
    {
        infoBackground.DOFade(0f, 0f);
    }


    private void OnEnable()
    {
        infoBackground.DOFade(0f, 0f);
        infoBackground.DOFade(1f, 0.5f).SetDelay(0.5f);
    }

    private void OnDisable()
    {
        infoBackground.DOFade(0f, 1f);
    }
}
