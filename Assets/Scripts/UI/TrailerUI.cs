using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TrailerUI : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] RectTransform background;
    [SerializeField] GameObject darkbackground;

    [SerializeField] RectTransform text0;
    [SerializeField] RectTransform text1;
    [SerializeField] RectTransform text2;
    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnEnable()
    {
        rectTransform.localPosition = new Vector2(0, -Screen.height);
        background.localPosition = new Vector2(0, background.rect.height);

        


    }

    public void Close()
    {
        background.DOLocalMove(new Vector2(0f, background.rect.height), 0.5f).SetEase(Ease.OutQuart);
        rectTransform.DOLocalMove(new Vector2(0f, -Screen.height), 0.5f).SetEase(Ease.OutQuart).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        gameObject.SetActive(false);
    }

    void CallElement()
    {
        rectTransform.DOLocalMove(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutQuart);
        background.DOLocalMove(new Vector2(0f, 0f), 1.4f).SetEase(Ease.OutQuart);

        darkbackground.SetActive(true);
    }
    void CallText0()
    {
        text0.gameObject.SetActive(true);
    }

    void CallText1()
    {
        text1.gameObject.SetActive(true);
    }

    void CallText2()
    {
        text2.gameObject.SetActive(true);
        text1.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CallElement();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CallText0();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            CallText1();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            CallText2();
        }
    }
}
