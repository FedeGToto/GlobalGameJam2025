using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    private List<CharacterEffect> effects = new();

    public void AddEffect(CharacterEffect effect, CharacterSO character)
    {
        if (!effects.Contains(effect))
        {
            effects.Add(effect);
            effect.Setup(playerManager, character);
        }
    }

    private void Update()
    {
        foreach (var effect in effects)
        {
            effect.EffectUpdate();
        }
    }
}
