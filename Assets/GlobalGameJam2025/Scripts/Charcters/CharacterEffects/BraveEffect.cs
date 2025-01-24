using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BraveEffect : CharacterEffect
{
    [SerializeField] private LevelUpValue<float> buffValue;
    private bool hasAlreadyBuff;

    public override void Setup(PlayerManager owner, CharacterSO character)
    {
        base.Setup(owner, character);

        owner.Stats.OnShieldChanged.AddListener(ShieldUpdate);

        Modifier shieldValueModifier = new Modifier(-15f, ModifierType.Flat);
        Modifier shieldCooldownModifier = new Modifier(1f, ModifierType.Flat);
        owner.Stats.MaxShield.AddModifier(shieldValueModifier);
        owner.Stats.ShieldCooldown.AddModifier(shieldCooldownModifier);
    }

    private void ShieldUpdate(float currentShield)
    {
        if (currentShield <= 0)
        {
            if (hasAlreadyBuff)
                return;

            Modifier modifier = new Modifier(buffValue.GetValue(character.Level - 1), ModifierType.Additive, this);
            owner.Stats.Attack.AddModifier(modifier);
            Debug.Log(owner.Stats.Attack.Value);
            hasAlreadyBuff = true;
        }
        else
        {
            owner.Stats.Attack.TryRemoveAllModifiersOf(this);
            hasAlreadyBuff = false;
        }
    }

    public override Dictionary<string, string> GetItemUpgradeValues()
    {
        Dictionary<string, string> values = new();
        values.Add("oldValue1", (buffValue.GetValue(character.Level - 1) * 100).ToString());
        values.Add("newValue1", (buffValue.GetValue(character.Level) * 100).ToString());
        return values;

    }
}
