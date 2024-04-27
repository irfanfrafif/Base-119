using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Image fade;
    public void LoadSceneIndex(int i)
    {
        Time.timeScale = 1f;
        fade.DOFade(0, 0f).SetUpdate(true);
        fade.gameObject.SetActive(true);

        Sequence startSequence = DOTween.Sequence();

        startSequence.SetUpdate(true).Append(fade.DOFade(1f, 3f)).AppendInterval(1f).OnComplete(() => SceneManager.LoadScene(i));
    }
}
