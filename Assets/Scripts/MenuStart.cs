using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuStart : MonoBehaviour
{
    [SerializeField] Image fadeOut;
    [SerializeField] Transform playArea;
    [SerializeField] RectTransform title;
    [SerializeField] RectTransform buttons;
    [SerializeField] Transform player;

    Vector3 titleEndPos;
    Vector3 playerEndPos;
    Vector3 buttonEndPos;

    private void Start()
    {
        fadeOut.DOFade(1f, 0f);
        playArea.DOScale(new Vector3(0f, 0f, 0f), 0f);

        playerEndPos = player.position;
        player.DOLocalMoveY(20f, 0f);

        buttonEndPos = buttons.localPosition;

        titleEndPos = title.localPosition;
        buttons.DOLocalMoveX(-1300f, 0f);

        title.DOLocalMove(new Vector3(0f, 0f, 0f), 0f);

        Sequence startSequence = DOTween.Sequence();

        startSequence.Append(fadeOut.DOFade(0f, 3f))
            .Append(title.DOLocalMove(titleEndPos, 2f).SetEase(Ease.InOutExpo)).AppendCallback(PlayAudio)
            .Append(playArea.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.OutExpo))
            .Append(player.DOLocalMove(playerEndPos, 1f).SetEase(Ease.OutExpo))
            .Append(buttons.DOLocalMove(buttonEndPos, 0.5f).SetEase(Ease.OutExpo))
            .OnComplete(OnComplete);
    }

    void OnComplete()
    {
        fadeOut.gameObject.SetActive(false);
    }

    void PlayAudio()
    {
        SoundManager.Instance.PlayClip(4);
    }
}
