using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private int mainMenuSceneIndex;
    [SerializeField] private float animationDuration;

    private bool isLoading;

    private void Start()
    {
        GameEvents.current.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        gameOverCanvasGroup.gameObject.SetActive(true);
        gameOverCanvasGroup.DOFade(1f, animationDuration).OnComplete(() =>
        {
            gameOverCanvasGroup.interactable = true;
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
