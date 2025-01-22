using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffect
{
    protected PlayerManager owner;
    protected CharacterSO character;

    protected float cooldownTimer;

    public virtual void Setup(PlayerManager owner, CharacterSO character)
    {
        this.owner = owner;
        this.character = character;
    }

    /// <summary>
    /// Only happens if is not on cooldown
    /// </summary>
    protected virtual void EffectTick()
    {

    }

    /// <summary>
    /// Always calculates
    /// </summary>
    public virtual void EffectUpdate()
    {
        if (cooldownTimer <= 0)
        {
            EffectTick();
        }
        else
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
                CooldownEnded();
        }
    }

    /// <summary>
    /// Sets cooldown based on ability haste
    /// </summary>
    /// <param name="cooldown"></param>
    public virtual void SetOnCooldown(float cooldown)
    {
        //float characterCooldown = owner.Stats.GetStat(StatType.Haste).Value;
        //float newCooldown = cooldown * (100 / (100 + characterCooldown));

        cooldownTimer = cooldown;
    }

    protected virtual void CooldownEnded()
    {

    }

    public virtual Dictionary<string, string> GetItemUpgradeValues()
    {
        throw new NotImplementedException();
    }
}
