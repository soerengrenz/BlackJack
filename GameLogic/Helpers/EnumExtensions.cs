using System;
using System.Collections.Generic;
using System.Reflection;
namespace GameLogic.Helpers
{
    public static class EnumExtensions
    {
        public static int[] GetIntegerValues(this Enum enumValue)
        {
            Type type = enumValue.GetType();
            string name = Enum.GetName(type, enumValue);

            if (name == null) return Array.Empty<int>();

            FieldInfo field = type.GetField(name);
            var attribute = field?.GetCustomAttribute<CardValuesAttribute>();

            return attribute?.Values ?? Array.Empty<int>();
        }
    }
}
