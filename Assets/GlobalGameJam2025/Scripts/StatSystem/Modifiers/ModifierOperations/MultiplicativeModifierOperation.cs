public sealed class MultiplicativeModifierOperations : ModifierOperationsBase
{
    internal MultiplicativeModifierOperations(int capacity) : base(capacity) { }

    public override float CalculateModifiersValue(float baseValue, float currentValue)
    {
        float calculatedValue = currentValue;

        for (var i = 0; i < Modifiers.Count; i++)
            calculatedValue *= calculatedValue * Modifiers[i];

        return calculatedValue - currentValue;
    }
}