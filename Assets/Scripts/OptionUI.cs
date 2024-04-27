using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] RectTransform background;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider masterSlider;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnEnable()
    {
        //SoundManager.Instance.PlayClip(7); // Audioclip Click

        bgmSlider.value = SoundManager.Instance.GetMusicVolume();
        sfxSlider.value = SoundManager.Instance.GetEffectVolume();
        masterSlider.value = SoundManager.Instance.GetMasterVolume();

        rectTransform.localPosition = new Vector2(0, -Screen.height);
        background.localPosition = new Vector2(0, background.rect.height);

        rectTransform.DOLocalMove(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutQuart);
        background.DOLocalMove(new Vector2(0f, 0f), 1.4f).SetEase(Ease.OutQuart);
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
