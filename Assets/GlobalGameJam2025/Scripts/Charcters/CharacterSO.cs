using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Localization;
using static UnityEngine.UI.GridLayoutGroup;

[CreateAssetMenu(fileName = "New Character", menuName = "Global Game Jam 2025/Character")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] private string characterID;

    [Header("Appearence")]
    [SerializeField] private Sprite artwork;
    [SerializeField] private LocalizedString characterName;
    [SerializeField] private LocalizedString characterDescription;
    [SerializeField] private LocalizedString upgradeText;

    [Header("Effect")]

    private int level = 0;
    private PlayerManager owner;

    public string ID => characterID;
    public Sprite Artwork => artwork;

    public int Level => level;
    public PlayerManager Owner => owner;

    public string GetName() => characterName.GetLocalizedString();

    public string GetUpgradeDescription()
    {
        if (Level < 1)
            return characterDescription.GetLocalizedString();

        //var dict = itemEffect.GetItemUpgradeValues();
        //upgradeText.Arguments = new object[] { dict };
        //return upgradeText.GetLocalizedString();

        return "Boh";
    }

    public void SetOwner(PlayerManager owner)
    {
        this.owner = owner;
    }

    public virtual void LevelUp()
    {
        level++;
    }
}
