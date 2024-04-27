using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Machinery Data", menuName = "SO/Machinery Data")]
public class MachineryData : ScriptableObject
{
    public MachineryCraftingData[] electrocuteGunData;
    public MachineryCraftingData[] heatGunData;
    public MachineryCraftingData[] ssmData;
}

[Serializable]
public class MachineryCraftingData
{
    public ItemData craftedItem;

    public ItemData material1;
    public int quantity1;

    public ItemData material2;
    public int quantity2;

    public ItemData material3;
    public int quantity3;

    public float duration;
}
