using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SceneStart : MonoBehaviour
{
    [SerializeField] Vector3 cameraPos;

    [SerializeField] Image darkFadein;
    [SerializeField] TMP_Text daystartText;
    [SerializeField] Transform cameraTransform;
    [SerializeField] RectTransform hudCanvas;
    [SerializeField] TMP_Text text;

    private void Start()
    {
        cameraTransform.position = new Vector3(0, -10f, -10f);
        cameraPos = new Vector3(0, 0.7f, -10f);

        Sequence startSequence = DOTween.Sequence();

        text.DOFade(0f, 2f).SetDelay(3f);

        startSequence.Append(darkFadein.DOFade(0f, 3f))
            //.OnComplete(() => EnableText())
            .Append(cameraTransform.DOLocalMove(cameraPos, 2f).SetEase(Ease.OutExpo))
            .OnComplete(OnStartComplete);
    }

    void OnStartComplete()
    {
        gameObject.SetActive(false);
    }
    void EnableText()
    {
        daystartText.gameObject.SetActive(true);
    }
}
