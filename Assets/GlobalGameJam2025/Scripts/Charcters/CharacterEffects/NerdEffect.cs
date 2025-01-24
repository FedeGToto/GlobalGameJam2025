using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NerdEffect : CharacterEffect
{
    [SerializeField] private LevelUpValue<float> attackSpeed;
    [SerializeField] private LevelUpValue<float> moveSpeed;
    [SerializeField] private float cooldown = 6f;

    public override void Setup(PlayerManager owner, CharacterSO character)
    {
        base.Setup(owner, character);

        GameEvents.current.OnKill += OnKill;

        Modifier shieldRegenModifier = new Modifier(-0.5f, ModifierType.Flat);
        Modifier shieldCooldownModifier = new Modifier(1f, ModifierType.Flat);
        owner.Stats.ShieldRegen.AddModifier(shieldRegenModifier);
        owner.Stats.ShieldCooldown.AddModifier(shieldCooldownModifier);
    }

    private void OnKill()
    {
        if (cooldownTimer <= 0)
        {
            int level = character.Level - 1;
            Modifier attackModifier = new Modifier(-attackSpeed.GetValue(level), ModifierType.Additive, this);
            Modifier speedModifier = new Modifier(moveSpeed.GetValue(level), ModifierType.Additive, this);

            owner.Stats.Speed.AddModifier(speedModifier);
            owner.Stats.AttackSpeed.AddModifier(attackModifier);

            SetOnCooldown(cooldown);
        }
    }

    protected override void CooldownEnded()
    {
        base.CooldownEnded();

        owner.Stats.Speed.TryRemoveAllModifiersOf(this);
        owner.Stats.AttackSpeed.TryRemoveAllModifiersOf(this);
    }

    public override Dictionary<string, string> GetItemUpgradeValues()
    {
        Dictionary<string, string> values = new()
        {
            { "oldValue1", (attackSpeed.GetValue(character.Level - 1) * 100).ToString() },
            { "newValue1", (attackSpeed.GetValue(character.Level) * 100).ToString() },
            { "oldValue2", (moveSpeed.GetValue(character.Level - 1) * 100).ToString() },
            { "newValue2", (moveSpeed.GetValue(character.Level) * 100).ToString() }
        };
        return values;

    }
}
