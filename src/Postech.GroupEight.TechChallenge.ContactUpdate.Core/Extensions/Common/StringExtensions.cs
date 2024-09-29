using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.Extensions.Common 
{
    [ExcludeFromCodeCoverage]
    public static class StringExtensions
    {
        public static bool ItHasALengthOf(this string text, int length)
        {
            return text.Length == length;
        }
    }
}