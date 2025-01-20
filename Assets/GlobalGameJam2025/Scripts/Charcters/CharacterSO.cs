using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Global Game Jam 2025/Character")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] private string characterID;

    [Header("Appearence")]
    [SerializeField] private Sprite artwork;

    public Sprite Artwork => artwork;
}
