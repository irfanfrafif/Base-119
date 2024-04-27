using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClip : MonoBehaviour
{
    public void PlaySound(int i)
    {
        SoundManager.Instance.PlayClip(i);
    }
}
