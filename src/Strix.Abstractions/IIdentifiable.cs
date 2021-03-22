using System;
using System.Collections;
using System.Collections.Generic;

namespace Strix.Abstractions
{
    public interface IIdentifiable<TIdentifier> : IEquatable<TIdentifier>
        where TIdentifier : notnull, IEquatable<TIdentifier>
    {
        TIdentifier Identifier { get; }
    }
}