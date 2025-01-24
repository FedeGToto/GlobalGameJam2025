using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StrategistEffect : CharacterEffect
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private LevelUpValue<float> cooldown;
    [SerializeField] private LevelUpValue<float> damage;

    private Transform firePoint;

    public override void Setup(PlayerManager owner, CharacterSO character)
    {
        base.Setup(owner, character);

        firePoint = owner.FirePoint;

        Modifier shieldValueModifier = new Modifier(-5f, ModifierType.Flat);
        Modifier shieldRegenModifier = new Modifier(-0.5f, ModifierType.Flat);
        Modifier shieldCooldownModifier = new Modifier(1f, ModifierType.Flat);
        owner.Stats.MaxShield.AddModifier(shieldValueModifier);
        owner.Stats.ShieldRegen.AddModifier(shieldRegenModifier);
        owner.Stats.ShieldCooldown.AddModifier(shieldCooldownModifier);
    }

    protected override void EffectTick()
    {
        base.EffectTick();

        Shoot();
    }

    public void Shoot()
    {
        int correctLevel = character.Level - 1;

        Bullet spawnedBullet = GameObject.Instantiate(bullet, firePoint.position, firePoint.rotation);
        spawnedBullet.SetOwner(owner.gameObject);
        spawnedBullet.Shoot(owner.Stats.Attack.Value * damage.GetValue(correctLevel));
        SetOnCooldown(cooldown.GetValue(correctLevel));
    }

    public override Dictionary<string, string> GetItemUpgradeValues()
    {
        Dictionary<string, string> values = new()
        {
            { "oldValue1", (cooldown.GetValue(character.Level - 1) * 100).ToString() },
            { "newValue1", (cooldown.GetValue(character.Level) * 100).ToString() },
            { "oldValue2", (damage.GetValue(character.Level - 1) * 100).ToString() },
            { "newValue2", (damage.GetValue(character.Level) * 100).ToString() }
        };
        return values;

    }
}
