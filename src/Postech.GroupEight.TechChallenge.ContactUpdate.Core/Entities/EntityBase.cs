using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Exceptions.Common;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Interfaces.Entities;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? ModifiedAt { get; protected set; }     
        public bool Active { get; protected set; }
        
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
            Active = true;
        }

        protected EntityBase(Guid id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
            Active = true;
        }

        /// <summary>
        /// Indicates whether an entity is active in the system.
        /// </summary>
        /// <returns>Returns true if the entity is active. Otherwise, it returns false.</returns>
        public bool IsActive()
        {
            return Active;
        }

        /// <summary>
        /// Inactivates an entity if it is active in the system.
        /// </summary>
        /// <exception cref="EntityInactiveException">The entity has already been inactivated.</exception>
        public void Inactivate()
        {
            EntityInactiveException.ThrowWhenIsInactive(this, "The entity has already been deleted");
            Active = false;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}