using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Machine Tile Data", menuName = "SO/Machinery Tile Data")]
public class MachineData : ScriptableObject
{
    public GameObject machinePrefab;
    public TileBase[] tiles;
    public Sprite[] sprites;
}
