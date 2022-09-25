using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLCD.CourseContent
{
    internal class Helpers
    {
        internal static Type GetCollectionElementType(Type type)
        {
            var elementType = typeof(string);
            //Type is not array or IEnumerable
            if (!typeof(IEnumerable).IsAssignableFrom(type) || type == typeof(string))
            {
                return type;
            }
            // Type is Array
            // short-circuit if you expect lots of arrays 
            if (type.IsArray)
                return type.GetElementType();

            // type is IEnumerable<T>;
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
}
