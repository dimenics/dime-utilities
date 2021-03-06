﻿using System.ComponentModel;
using System.Linq;

namespace System.Reflection
{
    public static class EnumUtilities
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
            => (T)Enum.Parse(typeof(T), value, true);

        /// <summary>
        /// Gets the value in the description attribute of the enum
        /// </summary>
        /// <typeparam name="T">The enumeration's type</typeparam>
        /// <param name="enumerationValue">The enumeration value</param>
        /// <returns>The text inside the enum's description attribute</returns>
        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.GetTypeInfo().IsEnum)
                throw new ArgumentException($"Value must be of enum type: {enumerationValue}", nameof(enumerationValue));

            // Tries to find a DescriptionAttribute for a potential friendly name for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length <= 0)
                return enumerationValue.ToString();

            object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Any()
                ? ((DescriptionAttribute)attributes.ElementAt(0)).Description
                : enumerationValue.ToString();
        }

        /// <summary>
        /// Reverse engineering a string value to an enum value
        /// </summary>
        /// <typeparam name="T">The enum's type</typeparam>
        /// <param name="description">The enum's description text</param>
        /// <returns></returns>
        public static T GetValueFromDescription<T>(this string description)
        {
            Type type = typeof(T);

            if (!type.GetTypeInfo().IsEnum)
                throw new InvalidOperationException();

            foreach (T enumValue in type.GetTypeInfo().GetEnumValues())
            {
                MemberInfo memberInfo = type.GetMember(enumValue.ToString()).First();
                DescriptionAttribute attribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

                if (attribute.Description == description)
                    return enumValue;
            }

            throw new ArgumentException($"Could not extract enum from this value: {description}", nameof(description));
        }
    }
}