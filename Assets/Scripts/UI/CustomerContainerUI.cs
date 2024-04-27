using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomerContainerUI : MonoBehaviour
{
    public Image customerIcon;
    public TMP_Text customerName;
    public TMP_Text requestInfo;

    public int itemPrice;
    public TMP_Text itemPriceText;

    public Image itemImage;
    public TMP_Text itemAmountText;

    [SerializeField] Button submitButton;
    [SerializeField] Slider waitDisplay;

    private float defaultWaitTime;
    public float waitTime;

    public int itemID;
    public int itemAmount;

    private void Awake()
    {
        
    }

    private void Start()
    {
        submitButton.onClick.AddListener(() => SubmitButtonClicked());

        itemAmountText.text = itemAmount.ToString();

        defaultWaitTime = waitTime;

        itemPriceText.text = "$" + (itemPrice * itemAmount);
    }

    private void OnEnable()
    {
        submitButton.gameObject.SetActive(ServiceLocator.Instance.uiManager.isATM);
    }

    void SubmitButtonClicked()
    {
        int gold = itemPrice * itemAmount;

        ServiceLocator.Instance.dayManager.AddGold(gold);

        ServiceLocator.Instance.inventoryManager.RemoveItem(itemID, itemAmount);

        //TODO add to quota;

        ServiceLocator.Instance.customerManager.RemoveCustomer(this, true);
        ServiceLocator.Instance.customerManager.UpdateAvailabilty();

        SoundManager.Instance.PlayClip(6); // Audio clip submit to customer
    }

    public void UpdateWaitDisplay()
    {
        waitDisplay.value = waitTime / defaultWaitTime;
    }

    public void CheckItemAvailability()
    {
        if (!ServiceLocator.Instance.inventoryManager.ContainsItem(itemID))
        {
            submitButton.interactable = false;
            return;
        }

        if (ServiceLocator.Instance.inventoryManager.GetAmount(itemID) < itemAmount)
        {
            submitButton.interactable = false;
        }
        else
        {
            submitButton.interactable = true;
        }
    }

    public void OnDisable()
    {
        submitButton.gameObject.SetActive(false);
    }
}
