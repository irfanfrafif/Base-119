using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DarkBackground : MonoBehaviour
{
    Image image;
    [SerializeField, Range(0,1)] float fadeOpacity;
    [SerializeField] float fadeDuration;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.DOFade(0f, 0f).SetUpdate(true);
    }
    public void OnEnable()
    {
        image.DOFade(0f, 0f).SetUpdate(true);
        image.DOFade(fadeOpacity, fadeDuration).SetUpdate(true);
    }

    public void Close()
    {
        image.DOFade(0f, fadeDuration).SetUpdate(true).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
