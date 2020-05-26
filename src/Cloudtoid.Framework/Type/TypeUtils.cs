namespace Cloudtoid
{
    using System;

    public static class TypeUtils
    {
        public static Type GetNonNullableType(this Type type)
            => IsNullableType(type) ? type.GetGenericArguments()[0] : type;

        public static Type GetNullableType(this Type type)
        {
            return type.IsValueType && !IsNullableType(type)
                ? typeof(Nullable<>).MakeGenericType(type)
                : type;
        }

        public static bool IsNullableType(this Type type)
            => type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        public static bool IsNullableOrReferenceType(this Type type)
            => !type.IsValueType || IsNullableType(type);

        public static bool IsBool(this Type type)
            => GetNonNullableType(type) == typeof(bool);

        public static bool IsNumeric(this Type type)
        {
            type = GetNonNullableType(type);
            if (type.IsEnum)
                return false;

#pragma warning disable IDE0010 // Add missing cases
            switch (Type.GetTypeCode(type))
#pragma warning restore IDE0010 // Add missing cases
            {
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.Byte:
                case TypeCode.Char:
                case TypeCode.SByte:
                    return true;
            }

            return false;
        }

        public static bool IsInteger(this Type type)
        {
            type = GetNonNullableType(type);
            if (type.IsEnum)
                return false;
#pragma warning disable IDE0010 // Add missing cases
            switch (Type.GetTypeCode(type))
#pragma warning restore IDE0010 // Add missing cases
            {
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Byte:
                case TypeCode.SByte:
                    return true;
            }

            return false;
        }

        public static bool IsInteger64(this Type type)
        {
            type = GetNonNullableType(type);
            if (type.IsEnum)
                return false;

#pragma warning disable IDE0010 // Add missing cases
            switch (Type.GetTypeCode(type))
#pragma warning restore IDE0010 // Add missing cases
            {
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return true;
            }

            return false;
        }

        public static bool IsArithmetic(this Type type)
        {
            type = GetNonNullableType(type);
            if (type.IsEnum)
                return false;

#pragma warning disable IDE0010 // Add missing cases
            switch (Type.GetTypeCode(type))
#pragma warning restore IDE0010 // Add missing cases
            {
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Single:
                case TypeCode.UInt16:
                    return true;
            }

            return false;
        }
    }
}
