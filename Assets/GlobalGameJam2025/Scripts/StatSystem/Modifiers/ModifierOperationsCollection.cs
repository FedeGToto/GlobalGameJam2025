using System;
using System.Collections.Generic;
using UnityEngine;

internal sealed class ModifierOperationsCollection
{
    private readonly Dictionary<ModifierType, Func<IModifiersOperations>> modifierOperationsDict = new();
    private bool modifiersCollectionHasBeenReturned;

    internal ModifierType AddModifierOperation(int order, Func<IModifiersOperations> modifierOperationsDelegate)
    {
        if (modifiersCollectionHasBeenReturned)
            throw new InvalidOperationException("Cannot change collection after is has been returned");

        var modifierType = (ModifierType)order;

        if (modifierType is ModifierType.Flat or ModifierType.Additive or ModifierType.Multiplicative)
            Debug.LogWarning("modifier operations for types flat, additive and multiplicative cannot be changed! Default operations for these types will be used.");

        modifierOperationsDict[modifierType] = modifierOperationsDelegate;

        return modifierType;
    }

    internal Dictionary<ModifierType, Func<IModifiersOperations>> GetModifierOperations(int capacity)
    {
        modifierOperationsDict[ModifierType.Flat] = () => new FlatModifierOperations(capacity);
        modifierOperationsDict[ModifierType.Additive] = () => new AdditiveModifierOperations(capacity);
        modifierOperationsDict[ModifierType.Multiplicative] = () => new MultiplicativeModifierOperations(capacity);

        modifiersCollectionHasBeenReturned = true;

        return modifierOperationsDict;
    }
}