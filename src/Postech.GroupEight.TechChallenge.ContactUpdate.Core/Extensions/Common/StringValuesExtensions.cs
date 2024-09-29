using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Primitives;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.Extensions.Common 
{
    [ExcludeFromCodeCoverage]
    public static class StringValuesExtensions
    {
        public static bool ContainsAny(this StringValues source, params string[] values)
        {
            return values.Any(source.Contains);
        }
    }
}