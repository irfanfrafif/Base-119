using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsUI : MonoBehaviour
{
    [SerializeField] TMP_Text headline;
    [SerializeField] TMP_Text dayInfo;

    private void OnEnable()
    {
        headline.text = ServiceLocator.Instance.dayManager.headline;
        dayInfo.text = ServiceLocator.Instance.dayManager.dayInfo;
    }
}
