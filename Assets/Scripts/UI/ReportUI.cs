using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ReportUI : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] RectTransform background;
    [SerializeField] GameObject darkBackground;

    [SerializeField] TMP_Text served;
    [SerializeField] TMP_Text missed;
    [SerializeField] TMP_Text earned;
    [SerializeField] TMP_Text profit;
    [SerializeField] TMP_Text info;

    [SerializeField] Button nextLevelButton;

    public bool infiniteMode;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnEnable()
    {
        ServiceLocator.Instance.uiManager.SetIsOpeningUI(true);

        darkBackground.SetActive(true);

        rectTransform.localPosition = new Vector2(0, -Screen.height);
        background.localPosition = new Vector2(0, background.rect.height);

        rectTransform.DOLocalMove(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutQuart);
        background.DOLocalMove(new Vector2(0f, 0f), 1.4f).SetEase(Ease.OutQuart);

        served.text = "CUSTOMER SERVED: " + ServiceLocator.Instance.customerManager.customerServed.ToString();
        missed.text = "CUSTOMER MISSED: " + ServiceLocator.Instance.customerManager.customerMissed.ToString();

        int earnedGold = ServiceLocator.Instance.dayManager.gold;
        earned.text = "EARNED: $" + earnedGold;

        if (infiniteMode)
        {
            nextLevelButton.gameObject.SetActive(false);
            info.text = "GAME OVER\n YOUR SCORE IS " + earnedGold;
        }
        else
        {
            
            int profitGold = earnedGold - ServiceLocator.Instance.dayManager.goldQuota;

            bool positive = (profitGold >= 0);


            profit.text = (positive) ? "PROFIT: $" + profitGold : "PROFIT: -$" + (-profitGold);

            if (positive)
            {
                info.text = "You achieved the target quota";
            }
            else
            {
                info.text = "You failed to achieve the target quota";
                nextLevelButton.interactable = false;
            }
        }

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
}
