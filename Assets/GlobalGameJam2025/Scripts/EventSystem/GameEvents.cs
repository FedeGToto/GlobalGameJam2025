using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    

    private void Awake()
    {
        current = this;
    }

    public event Action OnKill;
    public event Action OnGameOver;
    public event Action OnWin;

    public void Kill()
    {
        OnKill?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void Win()
    {
        OnWin?.Invoke();
    }
}
