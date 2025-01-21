using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardUI : MonoBehaviour
{
    [SerializeField] private Image characterArtwork;

    [SerializeField] private TextMeshProUGUI characterLevel;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterDesc;

    [SerializeField] private Button button;

    public void InitCharacterSelection(CharacterSelectionUI parent, CharacterSO character)
    {
        characterName.text = character.GetName();
        characterDesc.text = character.GetUpgradeDescription();

        characterLevel.text = (character.Level + 1).ToString();

        characterArtwork.sprite = character.Artwork;

        button.onClick.AddListener(() =>
        {
            GameManager.Instance.Player.Inventory.AddCharacter(character);
            parent.Hide();
        });
    }

}
