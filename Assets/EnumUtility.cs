
using System.ComponentModel;
using System;

public static class EnumUtility
{
    public static string GetDescription<T>(T enumValue) where T : Enum
    {
        var enumType = typeof(T);
        var memberInfo = enumType.GetMember(enumValue.ToString());
        if (memberInfo.Length > 0)
        {
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return ((DescriptionAttribute)attributes[0]).Description;
            }
        }
        return enumValue.ToString();
    }

    public static T GetEnumValueByDescription<T>(string description) where T : Enum
    {
        var enumType = typeof(T);
        foreach (var enumValue in Enum.GetValues(enumType))
        {
            var memberInfo = enumType.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    var enumDescription = ((DescriptionAttribute)attributes[0]).Description;
                    if (enumDescription == description)
                    {
                        return (T)enumValue;
                    }
                }
            }
        }
        throw new ArgumentException($"No enum value with the description '{description}' found in {enumType.Name}");
    }
}
