using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public CustomerData[] customerData;

    [SerializeField] GameObject containerPrefab;

    [SerializeField] Transform listContainer;

    List<CustomerContainerUI> customers;

    [SerializeField] GameObject notification;

    public int customerServed;
    public int customerMissed;

    private void Awake()
    {
        customers = new List<CustomerContainerUI>();
    }

    public void AddCustomer(int i)
    {
        Debug.Log("Instantiate!!!");
        var newCustomer = Instantiate(containerPrefab, listContainer);

        var customerContainerUI = newCustomer.GetComponent<CustomerContainerUI>();

        // Could've use constructor but eh

        customerContainerUI.customerIcon.sprite = customerData[i].icon;
        customerContainerUI.customerName.text = customerData[i].displayName;
        customerContainerUI.requestInfo.text = "REQUESTS " + customerData[i].requestedItems[0].requestedItem.itemName.ToUpper();
        customerContainerUI.itemID = customerData[i].requestedItems[0].requestedItem.itemID;
        customerContainerUI.itemAmount = customerData[i].requestedItems[0].amount;
        customerContainerUI.itemImage.sprite = customerData[i].requestedItems[0].requestedItem.sprite;
        customerContainerUI.itemPrice = (int)(customerData[i].requestedItems[0].requestedItem.price * ServiceLocator.Instance.dayManager.priceModifier); // PriceModifier
        // customerContainerUI.itemPriceText.text = "$" + (customerData[i].requestedItems[0].requestedItem.price * customerData[i].requestedItems[0].amount);

        customerContainerUI.waitTime = customerData[i].waitTime * customerData[i].requestedItems[0].amount * ServiceLocator.Instance.dayManager.waitTimeModifier; // WaitTime modifier

        customers.Add(customerContainerUI);

        notification.SetActive(true);

        SoundManager.Instance.PlayClip(3); // Audio clip new customer
    }

    private void Update()
    {
        for(int i = 0; i < customers.Count; i++)
        {
            customers[i].waitTime -= Time.deltaTime;
            customers[i].UpdateWaitDisplay();

            if(customers[i].waitTime <= 0)
            {
                RemoveCustomer(customers[i], false);
            }
        }
    }

    public void RemoveCustomer(CustomerContainerUI customer, bool isSale)
    {
        if (isSale) customerServed++;
        else customerMissed++;

        customers.Remove(customer);
        Destroy(customer.gameObject);
    }

    public void UpdateAvailabilty()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            customers[i].CheckItemAvailability();
        }
    }
}
