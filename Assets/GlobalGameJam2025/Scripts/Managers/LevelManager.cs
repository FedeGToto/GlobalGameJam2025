using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityAction OnLevelEnded;

    [SerializeField] private LevelSO[] levels;
    [SerializeField] private Transform[] spawnPoints;

    private List<CharacterSO> nextBonds;

    private Enemy[] possibleEnemies;
    private float spawnTimer;
    private float spawnedEnemies;
    private float currentSpawnRate;
    private int currentEnemies;
    private int currentLevel;

    private void Start()
    {
        GameEvents.current.OnKill += OnKill;

        StartNewLevel(0);
    }

    private void Update()
    {
        if (spawnedEnemies > 0)
        {
            if (spawnTimer <= 0)
            {
                SpawnEnemy();
                spawnTimer = currentSpawnRate;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
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

        // Move it after selecting the dialogue
        currentLevel++;
        StartNewLevel(currentLevel);

        OnLevelEnded?.Invoke();
    }

    public void StartNewLevel(int level)
    {
        currentEnemies = levels[level].Numbers;

        // Set spawn list in enemies manager
        possibleEnemies = levels[level].Enemies;
        currentSpawnRate = levels[level].SpawnRate;
        spawnedEnemies = currentEnemies;
    }

    public void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Enemy enemy = possibleEnemies[Random.Range(0, possibleEnemies.Length)];

        Enemy spawnedEnemy = Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
        spawnedEnemies--;
    }

    public List<CharacterSO> GetNextBonds() => nextBonds;
}
