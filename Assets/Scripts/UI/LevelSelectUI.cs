using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSelectUI : MonoBehaviour
{
    RectTransform rectTransform;
    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnEnable()
    {
        rectTransform.localPosition = new Vector2(0, -Screen.height);

        rectTransform.DOLocalMove(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutQuart);
    }

    public void Close()
    {
        rectTransform.DOLocalMove(new Vector2(0f, -Screen.height), 0.5f).SetEase(Ease.OutQuart).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        gameObject.SetActive(false);
    }

    public void PlayClick()
    {
        SoundManager.Instance.PlayClip(7); // Audioclip Click
    }
}
