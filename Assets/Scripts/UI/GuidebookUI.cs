using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GuidebookUI : MonoBehaviour
{
    public enum Page
    {
        rawMaterials,
        components,
        commodities
    }

    RectTransform rectTransform;

    [SerializeField] RectTransform elementA;
    [SerializeField] RectTransform elementB;
    [SerializeField] RectTransform elementC;

    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;
    [SerializeField] Button button4;

    [SerializeField] Button buttonPageLeft;
    [SerializeField] Button buttonPageRight;

    [SerializeField] TMP_Text header;

    [SerializeField] GuidebookData data;
    [SerializeField] ItemContentUI itemContent;
    [SerializeField] Page page;

    int currentPage;
    List<Button> buttons;

    

    private void EnableElementA()
    {

    }

    void UpdateListName()
    {
        int buttonTextIndex = 0;
        foreach (var button in buttons)
        {
            var buttonText = button.GetComponentInChildren<TMP_Text>();

            switch(page)
            {
                case Page.rawMaterials:
                    buttonText.text = data.rawMaterialsData[buttonTextIndex++].mainItem.itemName.ToUpper();
                    break;
                case Page.components:
                    buttonText.text = data.componentsData[buttonTextIndex++].mainItem.itemName.ToUpper();
                    break;
                case Page.commodities:
                    buttonText.text = data.commoditiesData[buttonTextIndex++].mainItem.itemName.ToUpper();
                    break;
            }

        }
    }

    void PageUpdate()
    {
        if (currentPage < 0) currentPage = 0;
        if (currentPage > 2) currentPage = 2;

        switch(currentPage)
        {
            case 0:
                buttonPageLeft.interactable = false;
                page = Page.rawMaterials;
                break;
            case 1:
                buttonPageLeft.interactable = true;
                buttonPageRight.interactable = true;
                page = Page.components;
                break;
            case 2:
                buttonPageRight.interactable = false;
                page = Page.commodities;
                break;
        }

        UpdateListName();
        ButtonContainerFadeIn(0f);

        SetHeaderText(page);

        elementC.DOLocalMoveX(-elementC.rect.width, 0f).OnComplete(() => ListButtonClicked(0)); //This is super hacky, very horrible
        elementC.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutQuart);


    }

    void SetHeaderText(Page page)
    {
        string output;
        switch(page)
        {
            case Page.rawMaterials:
                output = "RAW MATERIALS";
                break;
            case Page.components:
                output = "COMPONENTS";
                break;
            case Page.commodities:
                output = "COMMODITIES";
                break;
            default:
                output = "";
                break;
        }

        var text = header.GetComponent<typewriterUI_v2>();

        header.text = output;
        text.StartText();
    }

    void PageButtonClicked(int i)
    {
        switch(i)
        {
            case 0:
                currentPage -= 1;
                PageUpdate();
                break;
            case 1:
                currentPage += 1;
                PageUpdate();
                break;
        }
    }

    void ListButtonClicked(int i)
    {
        switch(page)
        {
            case Page.rawMaterials:
                itemContent.UpdateDisplay(data.rawMaterialsData[i]);
                break;
            case Page.components:
                itemContent.UpdateDisplay(data.componentsData[i]);
                break;
            case Page.commodities:
                itemContent.UpdateDisplay(data.commoditiesData[i]);
                break;
        }
    }

    //ElementC (ButtonContainer)
    void ButtonContainerFadeIn(float delay)
    {
        foreach (Transform child in elementC.gameObject.transform)
        {
            Image childImage = child.GetComponent<Image>();

            childImage.DOFade(0f, 0f);
            childImage.DOFade(1f, 0.5f).SetDelay(delay);
        }



        var button1text = button1.gameObject.GetComponentInChildren<TMP_Text>();
        var button2text = button2.gameObject.GetComponentInChildren<TMP_Text>();
        var button3text = button3.gameObject.GetComponentInChildren<TMP_Text>();
        var button4text = button4.gameObject.GetComponentInChildren<TMP_Text>();

        button1text.DOFade(0f, 0f);
        button1text.DOFade(1f, 0.5f).SetDelay(delay);

        button2text.DOFade(0f, 0f);
        button2text.DOFade(1f, 0.5f).SetDelay(delay);

        button3text.DOFade(0f, 0f);
        button3text.DOFade(1f, 0.5f).SetDelay(delay);

        button4text.DOFade(0f, 0f);
        button4text.DOFade(1f, 0.5f).SetDelay(delay);
    }

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        currentPage = 0;
        buttons = new List<Button>() { button1, button2, button3, button4 };

        button1.onClick.AddListener(() => ListButtonClicked(0));
        button2.onClick.AddListener(() => ListButtonClicked(1));
        button3.onClick.AddListener(() => ListButtonClicked(2));
        button4.onClick.AddListener(() => ListButtonClicked(3));

        buttonPageLeft.onClick.AddListener(() => PageButtonClicked(0));
        buttonPageRight.onClick.AddListener(() => PageButtonClicked(1));
    }

    private void Start()
    {
        PageUpdate();
    }

    public void OnEnable()
    {
        ServiceLocator.Instance.uiManager.isOpeningMenu = true;
        PageUpdate();

        // Self
        rectTransform.localPosition = new Vector2(0, -Screen.height);
        rectTransform.DOLocalMove(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutQuart);

        // ElementA (Header)
        elementA.DOScaleY(0f, 0f);
        elementA.DOScaleY(1f, 0.75f).SetEase(Ease.OutQuart);

        //ElementB (Header Shadow)
        elementB.DOLocalMoveX(-elementB.rect.width, 0f);
        elementB.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutQuart).SetDelay(0.6f);

        //ElementC (ButtonContainer)
        ButtonContainerFadeIn(0.6f);

        elementC.DOLocalMoveX(-elementC.rect.width, 0f).OnComplete(() => ListButtonClicked(0)); //This is super hacky, very horrible
        elementC.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutQuart).SetDelay(0.6f);

    }

    public void Close()
    {
        elementA.DOScaleY(0f, 0.5f);
        rectTransform.DOLocalMove(new Vector2(0f, -Screen.height), 0.5f).SetEase(Ease.OutQuart).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        ServiceLocator.Instance.uiManager.isOpeningMenu = false;
        gameObject.SetActive(false);
    }
}
