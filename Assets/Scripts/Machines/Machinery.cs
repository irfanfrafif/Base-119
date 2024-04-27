using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinery : MonoBehaviour
{
    [SerializeField] Vector2Int gridPos;

    public enum Facing
    {
        negativeX,
        positiveY,
        positiveX,
        negativeY,
    }

    public enum State { idle, process, done}

    public enum MachineType { electrocute, heat, ssm}

    public Facing facing;
    public Vector2Int frontGrid;

    public Sprite sprite;
    private SpriteRenderer spriteRenderer;

    public ParticleSystem particle;

    //private MachineManager machineManager;

    //Might change this in the future, but it'll work for now

    //public GameObject player;
    //private IsometricGridMovement playerMovement;

    [SerializeField] MachineryData machineryData;
    //private MachineryCraftingData[] data;
    public MachineType machineType;
    public State state;

    [SerializeField] float baseZ;

    private float progressFraction;

    private float progressBase;
    private float progressTarget;

    public int lastCheckedEntryIndex;

    private void Awake()
    {
        //switch(machineType)
        //{
        //    case MachineType.electrocute:
        //        data = machineryData.electrocuteGunData;
        //        break;
        //    case MachineType.heat:
        //        data = machineryData.heatGunData;
        //        break;
        //    case MachineType.ssm:
        //        data = machineryData.ssmData;
        //        break;
        //}

        transform.position = new Vector3(transform.position.x, transform.position.y, baseZ);
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        //machineManager = ServiceLocator.Instance.machineManager;

        switch(facing)
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

        //machineManager = ServiceLocator.Instance.machineManager;

        // Might change this
        //playerMovement = player.GetComponent<IsometricGridMovement>();

        // BRUH
        //if ((int)state < 2)
        //{
        //    particle.gameObject.transform.position += new Vector3(-0.25f, 0);
        //}
        //else
        //{
        //    particle.gameObject.transform.position += new Vector3(0.25f, 0);
        //}

        ToggleParticle();
    }

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

    public void StartProcess(MachineryCraftingData data)
    {
        progressBase = 0f;
        progressTarget = data.duration;
        progressFraction = 0f;

        state = State.process;
    }

    public float GetProgress()
    {
        return progressFraction;
    }

    public void ToggleParticle()
    {
        if (state == State.process)
        {
            particle.Play();
            Debug.Log("Particle toggle on");
        }
        else
        {
            particle.Stop();
            Debug.Log("Particle toggle off");
        }

    }

    private void OnMouseOver()
    {
        if(!ServiceLocator.Instance.uiManager.isOpeningMenu)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f);
    }

    private void OnMouseDown()
    {
        //playerMovement.GoHere((Vector3Int)GetFrontGrid(), true);
        //ServiceLocator.Instance.uiManager.machineUI.SetMachineType((int)(this.machineType));
    }

    private void Update()
    {
        if(state == State.process)
        {
            progressBase += (1 * Time.deltaTime);
            progressFraction = progressBase / progressTarget;

            if(progressFraction >= 1)
            {
                state = State.done;
                ToggleParticle();
                ServiceLocator.Instance.uiManager.machineUI.PageRefresh();
            }
        }
    }
}
