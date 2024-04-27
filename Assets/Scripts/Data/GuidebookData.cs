using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Guidebook Data", menuName = "SO/Guidebook Data")]
public class GuidebookData : ScriptableObject
{
    public GuidebookItemData[] rawMaterialsData;
    public GuidebookItemData[] componentsData;
    public GuidebookItemData[] commoditiesData;
}

[Serializable]
public class GuidebookItemData
{
    public ItemData mainItem;

    public ItemData subItem1;
    public string info1;

    public ItemData subItem2;
    public string info2;

    public ItemData subItem3;
    public string info3;
}
