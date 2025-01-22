using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AggressiveEffect : CharacterEffect
{
    private Enemy lastEnemy;
    private float multiplier = 0f;

    public override void Setup(PlayerManager owner, CharacterSO character)
    {
        base.Setup(owner, character);

        owner.OnDamageDealt.AddListener(DamageDealt);
    }

    public void DamageDealt(Enemy hittedEnemy)
    {
        if (hittedEnemy == lastEnemy)
        {
            multiplier += 0.1f;
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
    }

    private void ResetModifier()
    {
        owner.Stats.Attack.TryRemoveAllModifiersOf(this);
        float updateShownVal = owner.Stats.Attack.Value;
    }
}
