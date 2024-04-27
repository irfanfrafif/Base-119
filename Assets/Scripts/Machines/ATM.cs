using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATM : MonoBehaviour
{
    public enum Facing
    {
        negativeX,
        positiveY,
        positiveX,
        negativeY,
    }

    [SerializeField] Facing facing;

    [SerializeField] Vector2Int gridPos;
    public Vector2Int frontGrid;

    public Sprite sprite;
    private SpriteRenderer spriteRenderer;

    [SerializeField] float baseZ;

    [SerializeField] float duration;

    public Vector2Int GetFrontGrid()
    {
        return gridPos + frontGrid;
    }

    public void SetSprite(Sprite inputSprite)
    {
        sprite = inputSprite;
    }

    public void SetGridPos(Vector2Int vectorData)
    {
        gridPos = vectorData;
    }

    public void SetFacing(int i)
    {
        facing = (Facing)i;
    }


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
    }

    private void Start()
    {
        gridPos = ((Vector2Int)ServiceLocator.Instance.gridManager.movementGrid.WorldToCell(gameObject.transform.position));

        switch (facing)
        {
            case Facing.positiveX:
                frontGrid = new Vector2Int(1, 0);
                break;
            case Facing.positiveY:
                frontGrid = new Vector2Int(0, 1);
                break;
            case Facing.negativeX:
                frontGrid = new Vector2Int(-1, 0);
                break;
            case Facing.negativeY:
                frontGrid = new Vector2Int(0, -1);
                break;
        }
    }

    private void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (!ServiceLocator.Instance.uiManager.isOpeningMenu)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f);
    }
}
