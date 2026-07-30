namespace GameLogic.Helpers
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CardValuesAttribute : Attribute
    {
        public int[] Values { get; }

        public CardValuesAttribute(params int[] values)
        {
            if (values == null || values.Length < 1 || values.Length > 2)
            {
                throw new ArgumentException("You must provide either 1 or 2 integer values.");
            }
            Values = values;
        }
    }
}
