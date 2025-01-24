using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EmpatheticEffect : CharacterEffect
{
    [SerializeField] private float radius;
    [SerializeField] private LevelUpValue<float> damagePower;

    private List<GameObject> enemies;
    private int lastEnemies;

    public override void Setup(PlayerManager owner, CharacterSO character)
    {
        base.Setup(owner, character);

        enemies = new List<GameObject>();

        Modifier shieldValueModifier = new Modifier(-10f, ModifierType.Flat);
        Modifier shieldRegenModifier = new Modifier(-1f, ModifierType.Flat);
        owner.Stats.MaxShield.AddModifier(shieldValueModifier);
        owner.Stats.ShieldRegen.AddModifier(shieldRegenModifier);
    }

    protected override void EffectTick()
    {
        base.EffectTick();

        enemies.Clear();
        Collider[] allColliders = Physics.OverlapSphere(owner.transform.position, radius);

        foreach (Collider collider in allColliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                if (!enemies.Contains(enemy.gameObject)) { enemies.Add(enemy.gameObject); }
            }
        }

        if (enemies.Count != lastEnemies)
        {
            lastEnemies = enemies.Count;
            owner.Stats.Attack.TryRemoveAllModifiersOf(this);
            Modifier modifier = new Modifier(damagePower.GetValue(character.Level - 1) * enemies.Count, ModifierType.Additive, this);
            owner.Stats.Attack.AddModifier(modifier);
            Debug.Log(owner.Stats.Attack.Value);
        }

    }

    public override Dictionary<string, string> GetItemUpgradeValues()
    {
        Dictionary<string, string> values = new()
        {
            { "oldValue1", (damagePower.GetValue(character.Level - 1) * 100).ToString() },
            { "newValue1", (damagePower.GetValue(character.Level) * 100).ToString() }
        };
        return values;

    }
}
