using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

[Serializable]
public sealed class Stat
{
    private const int DEFAULT_LIST_CAPACITY = 4;
    private const int DEFAULT_DIGIT_ACCURACY = 2;
    internal const int MAXIMUM_ROUND_DIGITS = 8;

    [SerializeField] private float baseValue;

    [SuppressMessage("NDepend", "ND1902:AvoidStaticFieldsWithAMutableFieldType", Justification = "Cannot mutate after Instantiation of Stat, will throw.")]
    [SuppressMessage("NDepend", "ND1901:AvoidNonReadOnlyStaticFields", Justification = "Not readonly so that it can be called from Init() for reset.")]
    private static ModifierOperationsCollection ModifierOperationsCollection = new();

    private readonly int digitAccuracy;
    private readonly List<Modifier> modifiersList = new();
    private readonly SortedList<ModifierType, IModifiersOperations> modifiersOperations = new();

    private float currentValue;
    private bool isDirty;

    [SuppressMessage("NDepend", "ND1701:PotentiallyDeadMethods", Justification = "Needed for Unity's disable domain reload feature.")]
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init() => ModifierOperationsCollection = new();

    public Stat(float baseValue, int digitAccuracy, int flatModsMaxCapacity)
    {
        this.baseValue = baseValue;
        currentValue = baseValue;
        this.digitAccuracy = digitAccuracy;

        InitializeModifierOperations(flatModsMaxCapacity);

        void InitializeModifierOperations(int capacity)
        {
            var modifierOperations = ModifierOperationsCollection.GetModifierOperations(capacity);

            foreach (var operationType in modifierOperations.Keys)
                modifiersOperations[operationType] = modifierOperations[operationType]();
        }
    }
    public Stat(float baseValue) : this(baseValue, DEFAULT_DIGIT_ACCURACY, DEFAULT_LIST_CAPACITY) { }
    public Stat(float baseValue, int digitAccuracy) : this(baseValue, digitAccuracy, DEFAULT_LIST_CAPACITY) { }

    public float BaseValue
    {
        get => baseValue;
        set
        {
            baseValue = value;
            currentValue = CalculateModifiedValue(digitAccuracy);
            OnValueChanged();
        }
    }

    public float Value
    {
        get
        {
            if (IsDirty)
            {
                currentValue = CalculateModifiedValue(digitAccuracy);
                OnValueChanged();
            }
            return currentValue;
        }
    }

    private bool IsDirty
    {
        get => isDirty;
        set
        {
            isDirty = value;
            if (isDirty)
                OnModifiersChanged();
        }
    }

    public event Action ValueChanged;
    public event Action ModifiersChanged;

    public void AddModifier(Modifier modifier)
    {
        modifiersOperations[modifier.Type].AddModifier(modifier);
        IsDirty = true;
    }

    public static ModifierType NewModifierType(int order, Func<IModifiersOperations> modifierOperationsDelegate)
    {
        try
        {
            return ModifierOperationsCollection.AddModifierOperation(order, modifierOperationsDelegate);
        }
        catch
        {
            throw new InvalidOperationException("Add any modifier operations before any initialization of the Stat class!");
        }
    }

    public IReadOnlyList<Modifier> GetModifiers()
    {
        modifiersList.Clear();

        foreach (var modifiersOperation in modifiersOperations.Values)
            modifiersList.AddRange(modifiersOperation.GetAllModifiers());

        return modifiersList.AsReadOnly();
    }

    public IReadOnlyList<Modifier> GetModifiers(ModifierType modifierType) => modifiersOperations[modifierType].GetAllModifiers();

    public bool TryRemoveModifier(Modifier modifier)
    {
        bool isModifierRemoved = false;

        if (modifiersOperations[modifier.Type].TryRemoveModifier(modifier))
        {
            IsDirty = true;
            isModifierRemoved = true;
        }

        return isModifierRemoved;
    }

    public bool TryRemoveAllModifiersOf(object source)
    {
        bool isModifierRemoved = false;

        for (int i = 0; i < modifiersOperations.Count; i++)
        {
            if (TryRemoveAllModifiersOfSourceFromList(source,
                modifiersOperations.Values[i].GetAllModifiers()))
            {
                isModifierRemoved = true;
                IsDirty = true;
            }
        }

        return isModifierRemoved;

        static bool TryRemoveAllModifiersOfSourceFromList(object source, List<Modifier> listOfModifiers)
        {
            bool modifierHasBeenRemoved = false;

            for (int i = listOfModifiers.Count - 1; i >= 0; i--)
            {
                if (ReferenceEquals(source, listOfModifiers[i].Source))
                {
                    listOfModifiers.RemoveAt(i);
                    modifierHasBeenRemoved = true;
                }
            }

            return modifierHasBeenRemoved;
        }
    }

    private float CalculateModifiedValue(int digitAccuracy)
    {
        digitAccuracy = Math.Clamp(digitAccuracy, 0, MAXIMUM_ROUND_DIGITS);

        float finalValue = baseValue;

        for (int i = 0; i < modifiersOperations.Count; i++)
            finalValue += modifiersOperations.Values[i].CalculateModifiersValue(baseValue, finalValue);

        IsDirty = false;
        return (float)Math.Round(finalValue, digitAccuracy);
    }


    private void OnValueChanged() => ValueChanged?.Invoke();
    private void OnModifiersChanged() => ModifiersChanged?.Invoke();
}