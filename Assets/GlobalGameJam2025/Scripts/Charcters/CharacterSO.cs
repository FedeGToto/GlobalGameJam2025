using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New Character", menuName = "Global Game Jam 2025/Character")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] private string characterID;

    [Header("Appearence")]
    [SerializeField] private Sprite artwork;
    [SerializeField] private LocalizedString characterName;
    [SerializeField] private LocalizedString characterDescription;
    [SerializeField] private LocalizedString upgradeText;

    [Header("Dialogues")]
    [SerializeField] private LevelUpValue<Dialogue> dialogues;

    [Header("Effect")]
    [SerializeReference, SubclassSelector] public CharacterEffect CharEffect;

    private int level = 0;
    private PlayerManager owner;

    public string ID => characterID;
    public Sprite Artwork => artwork;

    public Dialogue Dialogue => dialogues.GetValue(level-1) ;

    public int Level => level;
    public PlayerManager Owner => owner;

    public string GetName() => characterName.GetLocalizedString();

    public string GetUpgradeDescription()
    {
        if (Level < 1)
            return characterDescription.GetLocalizedString();

        var dict = CharEffect.GetItemUpgradeValues();
        upgradeText.Arguments = new object[] { dict };
        return upgradeText.GetLocalizedString();
    }

    public void SetOwner(PlayerManager owner)
    {
        this.owner = owner;
    }

    public virtual void LevelUp()
    {
        level++;

        GameManager.Instance.Player.Effects.AddEffect(CharEffect, this);
    }
}
