using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityAction OnLevelEnded;

    [SerializeField] private LevelSO[] levels;

    private List<CharacterSO> nextBonds;
    private int currentEnemies;
    private int currentLevel;

    private void Start()
    {
        GameEvents.current.OnKill += OnKill;

        StartNewLevel(0);
    }

    private void OnKill()
    {
        currentEnemies--;

        if (currentEnemies <= 0)
            EndLevel();
    }

    public void EndLevel()
    {
        nextBonds = GameManager.Instance.Characters.GetPossibleUpgrades();
        currentLevel++;
        StartNewLevel(currentLevel);

        OnLevelEnded?.Invoke();
    }

    public void StartNewLevel(int level)
    {
        currentEnemies = levels[level].Numbers;

        // Set spawn list in enemies manager
    }

    public List<CharacterSO> GetNextBonds() => nextBonds;
}
