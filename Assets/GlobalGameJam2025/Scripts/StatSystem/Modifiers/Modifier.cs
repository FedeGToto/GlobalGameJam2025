using System.Globalization;

public readonly struct Modifier
{
    public ModifierType Type { get; }
    public object Source { get; }

    private readonly float value;

    public Modifier(float value, ModifierType modifierType, object source = null)
    {
        this.value = value;
        Type = modifierType;
        Source = source;
    }

    public override string ToString() => $"Value:{value.ToString(CultureInfo.InvariantCulture)} Type: {Type}";

    public static implicit operator float(Modifier modifier) => modifier.value;
}