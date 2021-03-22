#region

using System;

#endregion

namespace Strix.Sessions.Abstractions.Storage
{
    /// <summary>
    /// Entity type-based session storage descriptor.
    /// </summary>
    public class EntityBasedSessionStorageDescriptor<TOwner, TUpdateType> : UserBasedSessionStorageDescriptor<TOwner>
    {
        /// <summary>
        /// Session entity type.
        /// </summary>
        public TUpdateType EntityType { get; }

        /// <summary>
        /// Session entity type (C# <see cref="Type"/>).
        /// </summary>
        public Type EntityClassType { get; }

        /// <summary>
        /// Creates new <see cref="UserBasedSessionStorageDescriptor"/> with specified session owner id.
        /// </summary>
        /// <param name="ownerId">Session owner id.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <param name="entityClassType">Session entity type (C# <see cref="Type"/>).</param>
        public EntityBasedSessionStorageDescriptor(
            TOwner owner,
            TUpdateType entityType,
            Type entityClassType) : base(owner)
        {
            EntityType = entityType;
            EntityClassType = entityClassType ?? throw new ArgumentNullException(nameof(entityClassType));
        }

        public override bool Equals(object obj)
        {
            if (obj is EntityBasedSessionStorageDescriptor<TOwner, TUpdateType> descriptor)
            {
                return Equals(descriptor);
            }
            else
            {
                return Equals(this, obj);
            }
        }

        public bool Equals(EntityBasedSessionStorageDescriptor<TOwner, TUpdateType> descriptor)
        {
            return descriptor.Owner.Equals(Owner) && descriptor.EntityType.Equals(EntityType) &&
                   descriptor.EntityClassType == EntityClassType;
        }
    }
}