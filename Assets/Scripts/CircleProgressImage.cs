using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CircleProgressImage : MonoBehaviour
{

    [SerializeField] Image element1;
    [SerializeField] Sprite grey1;
    [SerializeField] Sprite blue1;

    [SerializeField] Image element2;
    [SerializeField] Sprite grey2;
    [SerializeField] Sprite blue2;

    [SerializeField] RectTransform element3Rect;
    [SerializeField] Image element3;
    [SerializeField] Sprite grey3;
    [SerializeField] Sprite blue3;
    [SerializeField] float speed3;

    [SerializeField] RectTransform element4Rect;
    [SerializeField] Image element4;
    [SerializeField] Sprite grey4;
    [SerializeField] Sprite blue4;
    [SerializeField] float speed4;

    Vector3 rot = new Vector3(0, 0, 360);

    Tween tweenerA;
    Tween tweenerB;

    private void OnEnable()
    {
        tweenerA = element3Rect.DORotate(rot, speed3, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        tweenerB = element4Rect.DORotate(rot, speed4, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        tweenerA.Kill();
        tweenerB.Kill();
    }

    public void SetSprite(bool isBlue)
    {
        if (isBlue)
        {
            element1.sprite = blue1;
            element2.sprite = blue2;
            element3.sprite = blue3;
            element4.sprite = blue4;
        }
        else
        {
            element1.sprite = grey1;
            element2.sprite = grey2;
            element3.sprite = grey3;
            element4.sprite = grey4;
        }
    }
}
