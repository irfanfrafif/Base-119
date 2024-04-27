using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Day Modifier", menuName = "SO/Day Modifier")]
public class DayModifier : ScriptableObject
{
    public float moveSpeedModifier;
    public float waitTimeModifier;
    public float materialCooldownModifier;
    // add something else
    public float customerSpawnModifier;
    public float priceModifier;



    public string headline;
    [TextArea(2,4)] public string dayInfo;
}
