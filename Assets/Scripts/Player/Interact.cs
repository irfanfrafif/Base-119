using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Interact : MonoBehaviour
{
    enum Facing
    {
        positiveX,
        positiveY,
        negativeX,
        negativeY
    }

    [SerializeField] IsometricGridMovement isometricGridMovement;
    [SerializeField] GridManager gridManager;
    [SerializeField] Tilemap tileGrid;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }   
}
