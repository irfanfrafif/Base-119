using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer Data", menuName = "SO/Customer Data")]
public class CustomerData : ScriptableObject
{
    public string internalName;
    public string displayName;
    public Sprite icon;
    public float waitTime;

    public RequestedItems[] requestedItems;

    [Serializable]
    public class RequestedItems
    {
        public ItemData requestedItem;
        public int amount;
    }
}
