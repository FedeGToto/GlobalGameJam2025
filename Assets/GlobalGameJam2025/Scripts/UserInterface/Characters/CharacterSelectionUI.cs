using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionUI : MonoBehaviour
{
    [SerializeField] private CharacterCardUI characterUIPrefab;

    private List<CharacterCardUI> characterUIs;

    private void Start()
    {
        characterUIs = new List<CharacterCardUI>();

        GameManager.Instance.Level.OnLevelEnded += OnLevelEnded;

        Hide();
    }

    private void OnLevelEnded()
    {
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        PopulateCharacters(GameManager.Instance.Level.GetNextBonds());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ClearCharacters();
    }

    private void PopulateCharacters(List<CharacterSO> nextCharacters)
    {
        foreach (var characterSO in nextCharacters)
        {
            CharacterCardUI augmentUI = Instantiate(characterUIPrefab, transform);
            characterUIs.Add(augmentUI);
            augmentUI.InitCharacterSelection(this, characterSO);
        }
    }

    private void ClearCharacters()
    {
        foreach (var character in characterUIs)
        {
            Destroy(character.gameObject);
        }
        characterUIs.Clear();
    }
}
