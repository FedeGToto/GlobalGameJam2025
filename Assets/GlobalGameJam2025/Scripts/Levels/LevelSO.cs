using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Global Game Jam 2025/Level Settings")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private int enemiesNumber;

    [SerializeField] private Enemy[] enemies;

    public int Numbers => enemiesNumber;
    public Enemy[] Enemies => enemies;
}
