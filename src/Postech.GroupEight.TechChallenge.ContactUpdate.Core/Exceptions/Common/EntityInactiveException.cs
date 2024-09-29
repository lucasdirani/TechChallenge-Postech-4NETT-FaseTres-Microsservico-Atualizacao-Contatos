using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Entities;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.Exceptions.Common
{
    [ExcludeFromCodeCoverage]
    public class EntityInactiveException(string message) : DomainException(message)
    {
        public static void ThrowWhenIsInactive(EntityBase entity, string errorMessage)
        {
            if (!entity.IsActive())
            {
                throw new EntityInactiveException(errorMessage);
            }
        }
    }
}