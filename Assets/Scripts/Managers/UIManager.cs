using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MachineUI machineUI;
    public Machinery currentMachinerySelected;
    public MaterialSpawner currentMaterialSpawnerSelected;
    public CustomerUI customerUI;

    //Is player walking toward a machine?
    public bool isOpeningMenu;

    //Is a UI on screen?
    public bool isMenuOpen;

    public bool isATM;

    public int currentSelectedIndex;

    public void OpenUI()
    {
        switch(currentSelectedIndex)
        {
            case 0:
                break;
            case 1:
                machineUI.gameObject.SetActive(true);
                break;
            case 2:
                currentMaterialSpawnerSelected.DispenseItem();
                SoundManager.Instance.PlayClip(6); // Audio clip take materials
                break;
            case 3:
                customerUI.gameObject.SetActive(true);
                break;
            default:
                break;
        }

        currentSelectedIndex = 0;
    }

    public void SetIsATM(bool value)
    {
        isATM = value;
    }

    public void SetIsOpeningUI(bool value)
    {
        isOpeningMenu = value;
    }
}
