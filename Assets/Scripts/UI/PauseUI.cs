using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseUI : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] RectTransform mainContainer;
    [SerializeField] RectTransform optionContainer;

    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider masterSlider;
    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnEnable()
    {
        ServiceLocator.Instance.uiManager.SetIsOpeningUI(true);
        

        rectTransform.localPosition = new Vector2(0, -Screen.height);

        rectTransform.DOLocalMove(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutQuart).SetUpdate(true);

        mainContainer.gameObject.SetActive(true);
        optionContainer.gameObject.SetActive(false);

        bgmSlider.value = SoundManager.Instance.GetMusicVolume();
        sfxSlider.value = SoundManager.Instance.GetEffectVolume();
        masterSlider.value = SoundManager.Instance.GetMasterVolume();

        Time.timeScale = 0f;
    }

    public void Close()
    {
        Time.timeScale = 1f;
        ServiceLocator.Instance.uiManager.SetIsOpeningUI(false);
        rectTransform.DOLocalMove(new Vector2(0f, -Screen.height), 0.5f).SetEase(Ease.OutQuart).SetUpdate(true).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
