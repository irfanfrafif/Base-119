using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MachineManager : MonoBehaviour
{
    public Dictionary<Vector2Int, Machinery> machines;

    [SerializeField] List<MachineData> machineDatas;

    public Dictionary<TileBase, MachineData> machineDataFromTiles;
    public Dictionary<TileBase, int> machineDataFromTilesIndex;     // BRUH

    private GridManager gridManager;

    [SerializeField] float baseZ;

    [SerializeField] GameObject player;

    private void ReadMachineDatas()
    {
        machineDataFromTiles = new Dictionary<TileBase, MachineData>();
        machineDataFromTilesIndex = new Dictionary<TileBase, int>();

        int index = 0;

        foreach (var machineData in machineDatas)
        {
            index = 0;
            foreach (var tile in machineData.tiles)
            {
                if (tile != null)
                {
                    machineDataFromTiles.Add(tile, machineData);
                    machineDataFromTilesIndex.Add(tile, index);
                }
                index += 1;
                index = index % 4;
            }
        }
    }

    private Vector3 GetWorldPosFromGrid(Vector2Int vectorData)
    {
        return GetWorldPosFromGrid(((Vector3Int)vectorData));
    }

    private Vector3 GetWorldPosFromGrid(Vector3Int vectorData)
    {
        Vector3 newVector = gridManager.movementGrid.CellToWorld(vectorData);
        newVector.z = baseZ;

        return newVector;       
    }

    public void InitializeMachines()
    {
        machines.Clear();

        var machinesLocationData = ServiceLocator.Instance.gridManager.machineNodes;

        foreach (var vectorData in machinesLocationData.Keys)
        {
            // Might change this, absolute cluster fuck

            //var thisMachineData = FindMachineType(((Vector3Int)vectorData));

            // Get what tile it is
            var machineTile = gridManager.machineTiles.GetTile((Vector3Int)vectorData);

            // Get Machine Data from tile
            var thisMachineData = machineDataFromTiles[machineTile];

            // Get sprite index
            int spriteIndex = machineDataFromTilesIndex[machineTile];

            // instantiate new prefab
            var newMachine = Instantiate(thisMachineData.machinePrefab, GetWorldPosFromGrid(vectorData), Quaternion.identity);

            // Get the prefabs class
            var newMachinery = newMachine.GetComponent<Machinery>();

            // Set position
            newMachinery.SetGridPos(vectorData);

            // set player
            //newMachinery.player = player;

            // set sprite
            newMachinery.SetSprite(thisMachineData.sprites[spriteIndex]);

            // set facing
            newMachinery.SetFacing(spriteIndex);

            // add to machines dictionary
            machines.Add(vectorData, newMachine.GetComponent<Machinery>());
        }

        //DISABLES THE MACHINETILE TILEMAP FROM SCENE
        ServiceLocator.Instance.gridManager.machineTiles.GetComponent<TilemapRenderer>().enabled = false;
    }

    private void Awake()
    {
        machines = new Dictionary<Vector2Int, Machinery>();
    }

    private void Start()
    {
        gridManager = ServiceLocator.Instance.gridManager;
        player = GameObject.Find("Player");

        ReadMachineDatas();
        InitializeMachines();
    }
}
