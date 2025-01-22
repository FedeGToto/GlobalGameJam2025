using System;
using UnityEngine;

[Serializable]
public class LevelUpValue<T>
{
    [SerializeField] private T[] Values;

    public T GetValue(int level) => Values[level];
}
