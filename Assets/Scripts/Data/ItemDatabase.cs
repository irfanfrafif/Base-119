using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database" , menuName = "SO/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public ItemData[] itemDatas;
}
