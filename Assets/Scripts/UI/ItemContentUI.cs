using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ItemContentUI : MonoBehaviour
{
    public SubItemUI mainItem;
    public SubItemUI subItem1;
    public SubItemUI subItem2;
    public SubItemUI subItem3;
    
    public TMP_Text itemName;
    public TMP_Text itemInfo;
    public TMP_Text itemSubInfo;

    public Image infoBackground;

    public void UpdateDisplay(GuidebookItemData data)
    {
        if(data.mainItem != null)
        {
            mainItem.image.sprite = data.mainItem.sprite;
        }
        

        //Subitem 1
        if(data.subItem1 != null)
        {
            subItem1.gameObject.SetActive(true);
            subItem1.image.sprite = data.subItem1.sprite;
            subItem1.info.text = data.info1;
        }
        else
        {
            subItem1.gameObject.SetActive(false);
        }

        //Subitem 2
        if (data.subItem2 != null)
        {
            subItem2.gameObject.SetActive(true);
            subItem2.image.sprite = data.subItem2.sprite;
            subItem2.info.text = data.info2;
        }
        else
        {
            subItem2.gameObject.SetActive(false);
        }

        //Subitem 3
        if (data.subItem3 != null)
        {
            subItem3.gameObject.SetActive(true);
            subItem3.image.sprite = data.subItem3.sprite;
            subItem3.info.text = data.info3;
        }
        else
        {
            subItem3.gameObject.SetActive(false);
        }


        itemName.text = data.mainItem.itemName.ToUpper();
        itemInfo.text = data.mainItem.info;     
        itemSubInfo.text = data.mainItem.subInfo;

        itemName.gameObject.GetComponent<typewriterUI_v2>().StartText();
        itemInfo.gameObject.GetComponent<typewriterUI_v2>().StartText();
        itemSubInfo.gameObject.GetComponent<typewriterUI_v2>().StartText();
    }

    private void Awake()
    {
        infoBackground.DOFade(0f, 0f);
    }

    
    private void OnEnable()
    {
        infoBackground.DOFade(0f, 0f);
        infoBackground.DOFade(1f, 0.5f).SetDelay(0.5f);
    }

    private void OnDisable()
    {
        infoBackground.DOFade(0f, 1f);
    }
}
