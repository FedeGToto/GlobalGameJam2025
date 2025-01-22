using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AgileEffect : CharacterEffect
{
    [SerializeField] private LevelUpValue<float> attackEnpowerment;
    [SerializeField] private LevelUpValue<float> cooldownHaste;
    [SerializeField] private float empowerDuration = 1f;

    private bool hasEmpowered;
    private float empowerTimer;

    public override void Setup(PlayerManager owner, CharacterSO character)
    {
        base.Setup(owner, character);

        owner.OnPlayerDash.AddListener(OnDash);
        owner.OnDamageDealt.AddListener(OnDamageDealt);
    }

    private void OnDamageDealt(Enemy enemy)
    {
        if (!hasEmpowered) return;

        owner.RefundDash(cooldownHaste.GetValue(character.Level - 1));
        ResetEmpowerment();
    }

    private void ResetEmpowerment()
    {
        hasEmpowered = false;
        owner.Stats.Attack.TryRemoveAllModifiersOf(this);
        float updateShownVal = owner.Stats.Attack.Value;
    }

    protected override void EffectTick()
    {
        base.EffectTick();

        if (hasEmpowered)
        {
            if (empowerTimer > 0)
                empowerTimer -= Time.deltaTime;
            else
                ResetEmpowerment();
        }
        
    }

    private void OnDash()
    {
        hasEmpowered = true;
        empowerTimer = empowerDuration;

        Stat attackStat = GameManager.Instance.Player.Stats.Attack;
        Modifier modifer = new Modifier(attackEnpowerment.GetValue(character.Level - 1), ModifierType.Additive, this);
        attackStat.AddModifier(modifer);
        Debug.Log(attackStat.Value);
    }

    public override Dictionary<string, string> GetItemUpgradeValues()
    {
        Dictionary<string, string> values = new()
        {
            { "oldValue1", (attackEnpowerment.GetValue(character.Level - 1) * 100).ToString() },
            { "newValue1", (attackEnpowerment.GetValue(character.Level) * 100).ToString() },
            { "oldValue2", (cooldownHaste.GetValue(character.Level - 1) * 100).ToString() },
            { "newValue2", (cooldownHaste.GetValue(character.Level) * 100).ToString() }
        };
        return values;

    }
}
