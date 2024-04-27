using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "SO/Item")]
public class ItemData : ScriptableObject
{
    public int itemID;
    public string itemName;

    [TextArea(2, 4)] public string info;
    [TextArea(2, 4)] public string subInfo;

    public int price;

    public Sprite sprite;

}
