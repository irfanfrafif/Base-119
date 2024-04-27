using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayManager : MonoBehaviour
{
    enum Gamemode { level, infinite}

    [SerializeField] Gamemode gamemode;
    public int CustomerMissedLimit;

    public int gold;
    public int goldQuota;
    public TMP_Text goldContainer;

    public bool isTimeRunning;
    public float currentTime;
    public float endTime;

    [Space(10)]

    public float customerInterval;
    public float intervalMin;
    public float intervalMax;
    public float intervalTimer;

    [Space(10)]

    public float intervalChangeMultiplier;
    public float intervalChangeInterval;
    public float intervalChangeIntervalTimer;

    [Space(10)]

    public DayModifier[] modifiers;
    public float modifierInterval;
    public float modifierIntervalTimer;
    bool isModifierActive;

    [Space(10)]

    public float moveSpeedModifier;
    public float waitTimeModifier;
    public float materialCooldownModifier;
    public float customerSpawnModifier;
    public float priceModifier;

    [Space(10)]

    public string headline;
    public string dayInfo;

    [Space(10)]

    public Timer timer;

    [Space(10)]

    [SerializeField] GameObject reportUI;
    [SerializeField] GameObject menusCanvas;

    public void LoadModifier(int i)
    {
        moveSpeedModifier = modifiers[i].moveSpeedModifier;
        waitTimeModifier = modifiers[i].waitTimeModifier;
        materialCooldownModifier = modifiers[i].materialCooldownModifier;
        customerSpawnModifier = modifiers[i].customerSpawnModifier;
        priceModifier = modifiers[i].priceModifier;

        headline = modifiers[i].headline;
        dayInfo = modifiers[i].dayInfo;
    }

    private void Start()
    {
        currentTime = 0f;

        // customerInterval = 5f;

        LoadModifier(0);
        UpdateGoldDisplay();
    }


    private void Update()
    {
        if (isTimeRunning)
        {
            currentTime += Time.deltaTime;
            if (gamemode == Gamemode.level && currentTime > endTime)
            {
                isTimeRunning = false;
                DayEnd();
            }

            intervalTimer += Time.deltaTime;
            if(intervalTimer > customerInterval * customerSpawnModifier) // CustomerSpawnRate Modifier
            {
                ServiceLocator.Instance.customerManager.AddCustomer(Random.Range(0, ServiceLocator.Instance.customerManager.customerData.Length - 1));

                intervalTimer = 0f;

                customerInterval = Random.Range(intervalMin, intervalMax);
            }

            if(gamemode == Gamemode.infinite)
            {
                intervalChangeIntervalTimer += Time.deltaTime;
                if (intervalChangeIntervalTimer > intervalChangeInterval)
                {
                    intervalMin *= intervalChangeMultiplier;
                    intervalMax *= intervalChangeMultiplier;
                    intervalChangeIntervalTimer = 0f;
                }

                modifierIntervalTimer += Time.deltaTime;
                if(modifierIntervalTimer > modifierInterval)
                {
                    modifierIntervalTimer = 0f;
                    if (isModifierActive)
                    {
                        LoadModifier(0);
                        isModifierActive = false;
                    }
                    else
                    {
                        LoadModifier(Random.Range(1, modifiers.Length));
                        isModifierActive = true;
                    }
                }

                if(ServiceLocator.Instance.customerManager.customerMissed > CustomerMissedLimit)
                {
                    isTimeRunning = false;
                    DayEnd();
                }
            }
        }           
    }

    public void DayStart()
    {
        isTimeRunning = true;
        timer.StartTimer();
    }

    public void DayEnd()
    {
        foreach(Transform child in menusCanvas.transform)
        {
            child.gameObject.SetActive(false);
        }
        reportUI.SetActive(true);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldDisplay();
    }

    public void UpdateGoldDisplay()
    {
        if (goldContainer != null)
        {
            switch(gamemode)
            {
                case Gamemode.infinite:
                    goldContainer.text = "SCORE: " + gold;
                    break;
                case Gamemode.level:
                    goldContainer.text = "QUOTA: $" + gold + " / $" + goldQuota;
                    break;
            }
        }
    }

}
