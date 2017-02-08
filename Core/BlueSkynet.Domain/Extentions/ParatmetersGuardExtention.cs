using BlueSkynet.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueSkynet.Domain.Extentions
{
    public static class ParameterGuardExtensions
    {
        public static void ThrowIfNegative(this int value, string paramName)
        {
            if (value < 0) throw new InvalidOperationException($"{paramName} can not be negative");
        }

        public static void ThrowIfNull<T>(this T value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }

        public static void ThrowIfNullOrBlank(this string value, string paramName)
        {
            value.ThrowIfNull(paramName);

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(string.Format(ExceptionMessages.StringParamCannotBeBlank_ParamName, paramName), paramName);
        }

        public static void ThrowIfValueIsOutsideRange<T>(this T value, T minValue, T maxValue, string paramName)
            where T : struct, IComparable<T>
        {
            if (value.CompareTo(minValue) < 0 || value.CompareTo(maxValue) > 0)
                throw new ArgumentOutOfRangeException(string.Format(ExceptionMessages.ParamOutOfRange_ParamName_Value_MinRangeValue_MaxRangeValue, paramName, value, minValue, maxValue), paramName);
        }

        public static void ThrowIfUriIsNotAbsolute(this Uri value, string paramName, IEnumerable<string> allowedSchema = null)
        {
            value.ThrowIfNull(paramName);
            if (!value.IsAbsoluteUri)
                throw new ArgumentException(string.Format(ExceptionMessages.UriParamMustBeAbsolute_UriAddress, value), paramName);

            if (allowedSchema == null)
                return;

            if (!allowedSchema.Contains(value.Scheme))
                throw new ArgumentException(
                    string.Format(ExceptionMessages.UriParamSchemaNotAllowed_UriAddress_AllowedSchemas, value, string.Join(", ", allowedSchema),
                    paramName));
        }

        public static void ThrowIfNullOrEmpty<T>(this IEnumerable<T> collection, string paramName)
        {
            collection.ThrowIfNull(paramName);
            if (!collection.Any())
                throw new ArgumentException(string.Format(ExceptionMessages.ParamCollectionCannotBeEmpty_ParamName, paramName));
        }

        public static void ThrowIfNullOrEmpty<T>(this ICollection<T> collection, string paramName)
        {
            if (collection == null)
                throw new ArgumentNullException(paramName);

            if (collection.Count == 0)
                throw new ArgumentException(string.Format(ExceptionMessages.ParamCollectionCannotBeEmpty_ParamName, paramName));
        }

        public static void ThrowIfParamArrayLengthIsToShort<T>(this T[] array, int minimumAllowedLength, string paramName)
        {
            array.ThrowIfNull(paramName);
            minimumAllowedLength.ThrowIfValueIsOutsideRange(1, int.MaxValue, nameof(minimumAllowedLength));

            if (array.Length < minimumAllowedLength)
                throw new ArgumentException(string.Format(ExceptionMessages.ParamArrayLengthIsToShort_ParamName_MinimumAllowedLength, paramName, minimumAllowedLength), paramName);
        }
    }
}