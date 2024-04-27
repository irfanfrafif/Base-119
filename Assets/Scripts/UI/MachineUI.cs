using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MachineUI : MonoBehaviour
{
    public enum MachineType
    {
        erGun,
        hGun,
        ssm
    }

    RectTransform rectTransform;

    [SerializeField] RectTransform elementA;
    [SerializeField] RectTransform elementB;
    [SerializeField] RectTransform elementC;
    [SerializeField] GameObject darkBackground;

    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;
    [SerializeField] Button button4;
    [SerializeField] Image buttonBlocker;

    [SerializeField] Button craftButton;
    [SerializeField] Button takeButton;
    [SerializeField] Slider progressBar;

    [SerializeField] TMP_Text header;

    [SerializeField] MachineryData data;
    [SerializeField] CraftingContentUI craftingContent;
    [SerializeField] MachineType machineType;

    [SerializeField] TMP_Text machineStatus;

    Machinery selectedMachinery;
    List<Button> buttons;

    public List<int> itemIDToRemove;
    public List<int> itemIDToRemoveAmount; // BRUH

    public bool craftPossible;

    public void SetMachineType(int i)
    {
        machineType = (MachineType)i;
    }

    void UpdateListName()
    {
        int buttonCount = GetButtonCount();
        for(int i = 0; i < buttonCount; i++)
        {
            var buttonText = buttons[i].GetComponentInChildren<TMP_Text>();

            switch (machineType)
            {
                case MachineType.erGun:
                    buttonText.text = data.electrocuteGunData[i].craftedItem.itemName.ToUpper();
                    break;
                case MachineType.hGun:
                    buttonText.text = data.heatGunData[i].craftedItem.itemName.ToUpper();
                    break;
                case MachineType.ssm:
                    buttonText.text = data.ssmData[i].craftedItem.itemName.ToUpper();
                    break;
            }

        }
    }

    public void PageUpdate()
    {
        UpdateListName();
        ButtonContainerFadeIn(0f);

        SetHeaderText(machineType);

        elementC.DOLocalMoveX(-elementC.rect.width, 0f).OnComplete(() => ListButtonClicked(GetLastCheckedEntryIndex())); //This is super hacky, very horrible
        elementC.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutQuart);
    }

    public void PageRefresh()
    {
        CircleProgressImage circleProgress = progressBar.GetComponent<CircleProgressImage>();
        switch (GetSeletedMachinery().state)
        {
            case Machinery.State.done:
                //progressBar.gameObject.SetActive(true);
                circleProgress.SetSprite(true);
                progressBar.value = 1f;

                craftButton.interactable = false;
                takeButton.interactable = true;

                machineStatus.text = "DONE";

                buttonBlocker.gameObject.SetActive(true);
                break;
            case Machinery.State.process:
                //progressBar.gameObject.SetActive(true);
                circleProgress.SetSprite(false);
                progressBar.value = GetSeletedMachinery().GetProgress();

                machineStatus.text = "PROCESSING";

                craftButton.interactable = false;
                takeButton.interactable = false;
                buttonBlocker.gameObject.SetActive(true);

                break;
            case Machinery.State.idle:
                //progressBar.gameObject.SetActive(false);
                circleProgress.SetSprite(false);
                progressBar.value = 0f;

                machineStatus.text = "IDLE";

                craftButton.interactable = craftPossible;
                takeButton.interactable = false;
                buttonBlocker.gameObject.SetActive(false);
                break;
        }

        //UpdateListName();
        //ButtonContainerFadeIn(0f);

        //SetHeaderText(machineType);

        //elementC.DOLocalMoveX(-elementC.rect.width, 0f).OnComplete(() => ListButtonClicked(GetLastCheckedEntryIndex())); //This is super hacky, very horrible
        //elementC.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutQuart);


    }

    void SetHeaderText(MachineType type)
    {
        string output;
        switch (type)
        {
            case MachineType.erGun:
                output = "ER GUN";
                break;
            case MachineType.hGun:
                output = "HEAT GUN";
                break;
            case MachineType.ssm:
                output = "SSM GUN";
                break;
            default:
                output = "";
                break;
        }

        var text = header.GetComponent<typewriterUI_v2>();

        header.text = output;
        text.StartText();
    }

    Machinery GetSeletedMachinery()
    {
        return ServiceLocator.Instance.uiManager.currentMachinerySelected;
    }

    int GetLastCheckedEntryIndex()
    {
        return ServiceLocator.Instance.uiManager.currentMachinerySelected.lastCheckedEntryIndex;
    }

    void ListButtonClicked(int i)
    {
        ListButtonClicked(i, true);
    }
    void ListButtonClicked(int i, bool typewriter)
    {
        switch (machineType)
        {
            case MachineType.erGun:
                craftingContent.UpdateDisplay(data.electrocuteGunData[i], typewriter);
                break;
            case MachineType.hGun:
                craftingContent.UpdateDisplay(data.heatGunData[i], typewriter);
                break;
            case MachineType.ssm:
                craftingContent.UpdateDisplay(data.ssmData[i], typewriter);
                break;
        }

        PageRefresh();

        ServiceLocator.Instance.uiManager.currentMachinerySelected.lastCheckedEntryIndex = i;
    }

    int GetButtonCount()
    {
        int buttonCount = 0;
        switch (machineType)
        {
            case MachineType.erGun:
                buttonCount = data.electrocuteGunData.Length;
                break;
            case MachineType.hGun:
                buttonCount = data.heatGunData.Length;
                break;
            case MachineType.ssm:
                buttonCount = data.ssmData.Length;
                break;
        }
        return buttonCount;
    }

    //ElementC (ButtonContainer)
    void ButtonContainerFadeIn(float delay)
    {
        //Disable all button
        foreach (Transform child in elementC.gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }


        int buttonCount = GetButtonCount();

        for (int i = 0; i < buttonCount; i++)
        {
            buttons[i].gameObject.SetActive(true);

            Image buttonImage = buttons[i].gameObject.GetComponent<Image>();

            buttonImage.DOFade(0f, 0f);
            buttonImage.DOFade(1f, 0.5f).SetDelay(delay);

            var buttonText = buttons[i].gameObject.GetComponentInChildren<TMP_Text>();

            buttonText.DOFade(0f, 0f);
            buttonText.DOFade(1f, 0.5f).SetDelay(delay);
        }

        //for(int i = 0; i < buttonCount; i++)
        ////foreach (Transform child in elementC.gameObject.transform)
        //{
        //    Transform child = elementC.gameObject.transform.GetChild(i);
        //    Image childImage = child.GetComponent<Image>();

        //    childImage.DOFade(0f, 0f);
        //    childImage.DOFade(1f, 0.5f).SetDelay(delay);
        //}



        //var button1text = button1.gameObject.GetComponentInChildren<TMP_Text>();
        //var button2text = button2.gameObject.GetComponentInChildren<TMP_Text>();
        //var button3text = button3.gameObject.GetComponentInChildren<TMP_Text>();
        //var button4text = button4.gameObject.GetComponentInChildren<TMP_Text>();

        //button1text.DOFade(0f, 0f);
        //button1text.DOFade(1f, 0.5f).SetDelay(delay);

        //button2text.DOFade(0f, 0f);
        //button2text.DOFade(1f, 0.5f).SetDelay(delay);

        //button3text.DOFade(0f, 0f);
        //button3text.DOFade(1f, 0.5f).SetDelay(delay);

        //button4text.DOFade(0f, 0f);
        //button4text.DOFade(1f, 0.5f).SetDelay(delay);
    }

    public void CraftButtonClicked()
    {
        var thisMachine = ServiceLocator.Instance.uiManager.currentMachinerySelected;

        int i = GetLastCheckedEntryIndex();
        switch (machineType)
        {
            case MachineType.erGun:
                thisMachine.StartProcess(data.electrocuteGunData[i]);
                SoundManager.Instance.PlayClip(0); // Audio clip craft normal
                break;
            case MachineType.hGun:
                thisMachine.StartProcess(data.heatGunData[i]);
                SoundManager.Instance.PlayClip(0); // Audio clip craft normal
                break;
            case MachineType.ssm:
                thisMachine.StartProcess(data.ssmData[i]);
                SoundManager.Instance.PlayClip(0); // Audio clip craft ssm
                break;
        }

        thisMachine.ToggleParticle();

        craftButton.interactable = false;

        PageRefresh();

        // TODO Reduce item from inventory

        int removeIndex = 0;
        foreach (var id in itemIDToRemove)
        {
            ServiceLocator.Instance.inventoryManager.RemoveItem(id, itemIDToRemoveAmount[removeIndex++]);
        }

        //craftingContent.item1InHand -= craftingContent.item1Required;
        //craftingContent.item2InHand -= craftingContent.item2Required;
        //craftingContent.item3InHand -= craftingContent.item3Required;

        ListButtonClicked(i, false);
    }

    public void TakeButtonClicked()
    {
        craftButton.interactable = true;
        GetSeletedMachinery().state = Machinery.State.idle;
        PageRefresh();

        // TODO Add crafted item to inventory

        int index = GetSeletedMachinery().lastCheckedEntryIndex;
        switch (machineType)
        {
            case MachineType.erGun:
                ServiceLocator.Instance.inventoryManager.AddItem(data.electrocuteGunData[index].craftedItem.itemID); // PAIN
                break;
            case MachineType.hGun:
                ServiceLocator.Instance.inventoryManager.AddItem(data.heatGunData[index].craftedItem.itemID); // PAIN
                break;
            case MachineType.ssm:
                ServiceLocator.Instance.inventoryManager.AddItem(data.ssmData[index].craftedItem.itemID); // PAIN
                break;
        }
    }

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        buttons = new List<Button>() { button1, button2, button3, button4 };

        button1.onClick.AddListener(() => ListButtonClicked(0));
        button2.onClick.AddListener(() => ListButtonClicked(1));
        button3.onClick.AddListener(() => ListButtonClicked(2));
        button4.onClick.AddListener(() => ListButtonClicked(3));

        craftButton.onClick.AddListener(() => CraftButtonClicked());
        takeButton.onClick.AddListener(() => TakeButtonClicked());
    }

    private void Start()
    {
        PageUpdate();
    }

    public void OnEnable()
    {
        //SoundManager.Instance.PlayClip(7); // Audioclip Click

        ServiceLocator.Instance.uiManager.isOpeningMenu = true;
        darkBackground.SetActive(true);

        PageUpdate();
        PageRefresh();

        // Self
        rectTransform.localPosition = new Vector2(0, -Screen.height);
        rectTransform.DOLocalMove(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutQuart);

        // ElementA (Header)
        //elementA.DOScaleY(0f, 0f);
        //elementA.DOScaleY(1f, 0.75f).SetEase(Ease.OutQuart);

        //ElementB (Header Shadow)
        //elementB.DOLocalMoveX(-elementB.rect.width, 0f);
        //elementB.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutQuart).SetDelay(0.6f);

        //ElementC (ButtonContainer)
        ButtonContainerFadeIn(0.6f);

        elementC.DOLocalMoveX(-elementC.rect.width, 0f).OnComplete(() => ListButtonClicked(GetLastCheckedEntryIndex())); //This is super hacky, very horrible
        elementC.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutQuart).SetDelay(0.6f);

    }

    private void Update()
    {
        if(GetSeletedMachinery().state == Machinery.State.process)
        {
            progressBar.value = GetSeletedMachinery().GetProgress();
        }
    }

    public void Close()
    {
        //elementA.DOScaleY(0f, 0.5f);
        rectTransform.DOLocalMove(new Vector2(0f, -Screen.height), 0.5f).SetEase(Ease.OutQuart).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        ServiceLocator.Instance.uiManager.isOpeningMenu = false;
        gameObject.SetActive(false);
    }
}
