using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerManager Player;
    public CharactersManager Characters;
    public LevelManager Level;

    private void Awake()
    {
        Instance = this;
    }
}
