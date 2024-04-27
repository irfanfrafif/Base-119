using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CustomerUI : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] GameObject darkBackground;
    [SerializeField] GameObject notification;
    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnEnable()
    {
        notification.SetActive(false);
        darkBackground.SetActive(true);
        ServiceLocator.Instance.uiManager.isOpeningMenu = true;

        rectTransform.localPosition = new Vector2(0, -Screen.height);
        rectTransform.DOLocalMove(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutQuart);

        ServiceLocator.Instance.customerManager.UpdateAvailabilty();
    }

    public void Close()
    {
        rectTransform.DOLocalMove(new Vector2(0f, -Screen.height), 0.5f).SetEase(Ease.OutQuart).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        ServiceLocator.Instance.uiManager.isOpeningMenu = false;
        gameObject.SetActive(false);
    }
}
