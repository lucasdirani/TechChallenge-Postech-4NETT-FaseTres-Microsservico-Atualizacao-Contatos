using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.Extensions.Common 
{
    [ExcludeFromCodeCoverage]
    public static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<Enum, string> _descriptionsCache = new();

        public static string GetDescription(this Enum enumValue)
        {
            return _descriptionsCache.GetOrAdd(enumValue, e =>
            {
                FieldInfo? fieldInfo = e.GetType().GetField(e.ToString());
                DescriptionAttribute[]? attributes = (DescriptionAttribute[]?)fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return attributes?.Length > 0 ? attributes[0].Description : e.ToString();
            });
        }
    }
}