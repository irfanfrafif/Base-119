using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SubItemUI : MonoBehaviour
{
    public enum DisplayItemType
    {
        main,
        left,
        right
    }

    public Image image;
    public TMP_Text info;

    public DisplayItemType type;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    //public void StartAnimation()
    //{
    //    switch (type)
    //    {
    //        case DisplayItemType.main:
    //            break;
    //        case DisplayItemType.left:
    //            image.DOFade(0f, 0f);
    //            image.DOFade(1f, 1f);
    //            break;
    //        case DisplayItemType.right:
    //            image.DOFade(0f, 0f);
    //            image.DOFade(1f, 1f);
    //            break;
    //    }
    //}

    private void OnEnable()
    {
        
        
    }
}
