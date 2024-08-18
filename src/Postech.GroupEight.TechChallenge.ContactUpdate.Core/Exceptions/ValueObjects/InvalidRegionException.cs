using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Enumerators;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Exceptions.Common;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class InvalidRegionException(string message, RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName) : DomainException(message)
    {
        public RegionNameEnumerator RegionName { get; } = regionName;
        public RegionStateNameEnumerator RegionStateName { get; } = regionStateName;
    }
}