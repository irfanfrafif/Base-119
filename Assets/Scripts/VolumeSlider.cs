using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    enum Type { master, effect, music}

    [SerializeField] Type type;

    [SerializeField] private Slider slider;

    private void Start()
    {
        
        if (type == Type.master)
        {
            SoundManager.Instance.ChangeMasterVolume(slider.value);
            slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
        }
        else if (type == Type.effect)
        {
            SoundManager.Instance.ChangeEffectVolume(slider.value);
            slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeEffectVolume(val));
        }
        else 
        {
            SoundManager.Instance.ChangeMusicVolume(slider.value);
            slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
        }
    }
}
