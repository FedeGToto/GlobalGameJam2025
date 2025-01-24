using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;
    bool isLoading = false;

    private void Start()
    {
        versionText.text = "Version " + Application.version;
    }

    public void StartGame()
    {
        if (isLoading) return;

        SceneManager.LoadScene(1);

        isLoading = true;
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
