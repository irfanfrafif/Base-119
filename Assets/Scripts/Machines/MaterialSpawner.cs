using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaterialSpawner : MonoBehaviour
{
    public enum Facing
    {
        negativeX,
        positiveY,
        positiveX,
        negativeY,
    }

    enum State { cooldown, ready}

    [SerializeField] Facing facing;

    [SerializeField] Vector2Int gridPos;
    public Vector2Int frontGrid;

    public Sprite sprite;
    private SpriteRenderer spriteRenderer;

    private State state;

    private Vector3 defaultPos;
    [SerializeField] float baseZ;

    private float progressFraction;

    private float progress;

    [SerializeField] float durationDefault;
    [SerializeField] float duration => durationDefault * ServiceLocator.Instance.dayManager.materialCooldownModifier; // CooldownModifier

    [SerializeField] ItemData itemData;
    [SerializeField] SpriteRenderer itemSprite;

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

    public void DispenseItem()
    {
        if(state == State.ready)
        {
            // TODO Give item;
            ServiceLocator.Instance.inventoryManager.AddItem(itemData.itemID);

            state = State.cooldown;

            transform.DOLocalMoveY(defaultPos.y - 0.2f, 0.5f).SetEase(Ease.OutCubic);
            itemSprite.DOFade(0.2f, 0.5f);
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;

        itemSprite.sprite = itemData.sprite;
        defaultPos = gameObject.transform.position;
    }

    private void Start()
    {
        gridPos = ((Vector2Int)ServiceLocator.Instance.gridManager.visualGrid.WorldToCell(gameObject.transform.position));

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
        switch(state)
        {
            case State.cooldown:
                progress += (1 * Time.deltaTime);

                progressFraction = progress / duration;

                if (progress >= duration)
                {
                    state = State.ready;
                    transform.DOLocalMoveY(defaultPos.y, 0.5f).SetEase(Ease.OutCubic);
                    itemSprite.DOFade(1f, 0.5f);
                    progress = 0;
                }
                break;
            case State.ready:
                break;
        }
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
