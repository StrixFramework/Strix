using System;
using Strix.Abstractions;
using Strix.Helpers;

namespace Strix
{
    public abstract class IdentifiableBase<TIdentifier> : IIdentifiable<TIdentifier>  where TIdentifier : notnull, IEquatable<TIdentifier>
    {
        public TIdentifier Identifier { get; }

        protected IdentifiableBase(TIdentifier identifier)
        {
            Identifier = identifier.GetOrThrowIfArgumentEqualsToNull(nameof(identifier));
        }
        
        public bool Equals(TIdentifier other)
        {
            other.ThrowIfArgumentEqualsToNull(nameof(other));

            Identifier.ThrowIfPropertyEqualsToNull(nameof(Identifier));

            return Identifier.Equals(other);
        }

        public override string ToString()
        {
            return Identifier.ToString();
        }
    }
}