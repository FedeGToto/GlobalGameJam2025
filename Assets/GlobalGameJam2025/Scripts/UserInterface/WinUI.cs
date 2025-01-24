using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup winCanvasGroup;
    [SerializeField] private int mainMenuSceneIndex;
    [SerializeField] private float animationDuration;

    private bool isLoading;

    private void Start()
    {
        GameEvents.current.OnWin += OnWin;
    }

    private void OnWin()
    {
        winCanvasGroup.gameObject.SetActive(true);
        winCanvasGroup.DOFade(1f, animationDuration).OnComplete(() =>
        {
            winCanvasGroup.interactable = true;
        });
    }

    public void MainMenu()
    {
        if (isLoading) return;

        SceneManager.LoadScene(mainMenuSceneIndex);
        isLoading = true;
    }

    public void Retry()
    {
        if (isLoading) return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isLoading = true;
    }
}
