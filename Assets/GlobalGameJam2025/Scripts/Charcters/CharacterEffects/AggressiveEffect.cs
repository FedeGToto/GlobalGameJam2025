using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AggressiveEffect : CharacterEffect
{
    [SerializeField] LevelUpValue<float> maxMultiplier;
    [SerializeField] private float minIncrease = 0.01f;
    [SerializeField] private float attackCooldown = 3f;

    private Enemy lastEnemy;
    private float multiplier = 0f;
    private float attackTimer;

    public override void Setup(PlayerManager owner, CharacterSO character)
    {
        base.Setup(owner, character);

        owner.OnDamageDealt.AddListener(DamageDealt);
    }

    protected override void EffectTick()
    {
        base.EffectTick();

        if (lastEnemy != null)
        {
            if (attackTimer <= 0f)
            {
                lastEnemy = null;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    public override Dictionary<string, string> GetItemUpgradeValues()
    {
        Dictionary<string, string> values = new();
        values.Add("oldValue1", (maxMultiplier.GetValue(character.Level - 1) * 100).ToString());
        values.Add("newValue1", (maxMultiplier.GetValue(character.Level) * 100).ToString());
        return values;

    }

    public void DamageDealt(Enemy hittedEnemy)
    {
        if (hittedEnemy == lastEnemy)
        {
            float max = maxMultiplier.GetValue(character.Level - 1);

            multiplier += minIncrease;
            if (multiplier >= max)
                multiplier = max;

            Modifier modifier = new(multiplier, ModifierType.Additive, this);
            owner.Stats.Attack.AddModifier(modifier);
            Debug.Log(owner.Stats.Attack.Value);
        }
        else
        {
            if (lastEnemy)
                lastEnemy.OnDamageTaken.RemoveListener(ResetModifier);
            
            lastEnemy = hittedEnemy;
            lastEnemy.OnDamageTaken.AddListener(ResetModifier);
            multiplier = 0f;
        }

        attackTimer = attackCooldown;
    }

    private void ResetModifier()
    {
        owner.Stats.Attack.TryRemoveAllModifiersOf(this);
        float updateShownVal = owner.Stats.Attack.Value;
    }
}
