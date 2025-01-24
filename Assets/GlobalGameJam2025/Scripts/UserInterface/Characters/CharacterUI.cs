using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;

    private CharacterSO character;
    private int level;

    public void UpdateAugment(CharacterSO character)
    {
        this.character = character;
        this.level = character.Level;

        nameText.text = character.GetName();
        levelText.text = "lvl " + level.ToString();
    }
}
