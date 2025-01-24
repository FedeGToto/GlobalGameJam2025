using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionUI : MonoBehaviour
{
    [SerializeField] private CharacterCardUI characterUIPrefab;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private float selectionDuration;

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

        group.DOFade(1, selectionDuration).OnComplete(() =>
        {
            group.interactable = true;
            group.blocksRaycasts = true;
        });
    }

    public void Hide()
    {
        group.interactable = false;
        group.blocksRaycasts = false;
        group.alpha = 0;

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
