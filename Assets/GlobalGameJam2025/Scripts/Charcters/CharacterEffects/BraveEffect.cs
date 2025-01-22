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

        GameManager.Instance.Player.Stats.OnShieldChanged.AddListener(ShieldUpdate);
    }

    private void ShieldUpdate(float currentShield)
    {
        if (currentShield <= 0)
        {
            if (hasAlreadyBuff)
                return;

            Modifier modifier = new Modifier(buffValue.GetValue(character.Level - 1), ModifierType.Additive, this);
            GameManager.Instance.Player.Stats.Attack.AddModifier(modifier);
            Debug.Log(GameManager.Instance.Player.Stats.Attack.Value);
            hasAlreadyBuff = true;
        }
        else
        {
            GameManager.Instance.Player.Stats.Attack.TryRemoveAllModifiersOf(this);
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
