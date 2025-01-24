using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;

    private int currentEnemies = -1;
    private int maxEnemies;

    private void Start()
    {
        GameManager.Instance.Level.OnLevelStarted.AddListener(OnLevelStarted);

        GameEvents.current.OnKill += UpdateCounter;

        OnLevelStarted(0);
    }

    private void OnDestroy()
    {
        GameManager.Instance.Level.OnLevelStarted.RemoveListener(OnLevelStarted);
    }

    public void OnLevelStarted(int level)
    {
        currentEnemies = -1;
        maxEnemies = GameManager.Instance.Level.GetLevel(level).Numbers;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        currentEnemies++;
        counterText.text = currentEnemies + "/" + maxEnemies;
    }
}
