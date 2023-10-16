namespace zw.CharacterStats
{
    public enum StatModType
    {
        Flat = 100,
        PercentAdd = 200,
        PercentMult = 300,
    }

    public class StatModifier
    {
        public readonly int Order;
        public readonly float Value;
        public readonly StatModType Type;
        public readonly object Source;

        public StatModifier(float value, StatModType type, int order, object source) // Added "source" input parameter
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }


        public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
    }

}
