using System;
using System.Linq;

namespace SqlRepoEx
{
    public static class TypeExtensions
    {
        public static bool IsSimpleType(
            this Type type)
        {
            return
                type.IsPrimitive ||
                new[]
                {
                    typeof(string),
                    typeof(decimal)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}