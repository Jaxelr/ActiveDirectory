using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ActiveDirectory.Extensions;

public static class TypeExtensions
{
    /// <summary>
    /// Check if the type inherits from IEnumerable
    /// </summary>
    /// <param name="type">The type declaration to determine if its an IEnumerable</param>
    /// <returns>A boolean that indicates if the type inherits from IEnumerable</returns>
    internal static bool IsIEnumerable(this Type type) => type.GetInterface(nameof(IEnumerable)) is not null;

    /// <summary>
    /// Returns the element from the underlying Collection
    /// </summary>
    /// <param name="type"></param>
    /// <returns>Gets the type from the element of the </returns>
    internal static Type GetAnyElementType(this Type type)
    {
        if (type.IsArray)
            return type.GetElementType()!;

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            return type.GetGenericArguments()[0];

        // type implements/extends IEnumerable<T>;
        var enumType = type.GetInterfaces()
                                .Where(t => t.IsGenericType &&
                                       t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                .Select(t => t.GenericTypeArguments[0]).FirstOrDefault();
        return enumType ?? type;
    }
}
