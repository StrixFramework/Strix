#region

using System.Collections.Generic;

#endregion

namespace Strix.Sessions.Abstractions.Storage
{
    /// <summary>
    /// Base interface for implementing session storage manager.
    /// </summary>
    public interface ISessionStorageManager<TOwner, TUpdateType>
    {
        /// <summary>
        /// Storage manager mode.
        /// </summary>
        SessionStorageMode Mode { get; }

        /// <summary>
        /// Gets all existing storages.
        /// </summary>
        /// <returns>Existing storages.</returns>
        IReadOnlyDictionary<SessionStorageDescriptor, ISessionStorage> GetStorages();

        /// <summary>
        /// Gets or creates session storage.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Session entity type.</typeparam>
        /// <returns>Session storage.</returns>
        ISessionStorage GetOrCreateStorage<TEntity>(TOwner owner, TUpdateType entityType);
    }
}