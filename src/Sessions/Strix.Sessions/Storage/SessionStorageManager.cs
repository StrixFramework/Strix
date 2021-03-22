#region

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Strix.Sessions.Abstractions.Storage;

#endregion

namespace Strix.Sessions.Storage
{
    /// <summary>
    /// Default implementation of <see cref="ISessionStorageManager{TOwner,TUpdateType}"/>.
    /// </summary>
    public class SessionStorageManager<TOwner, TUpdateType> : ISessionStorageManager<TOwner, TUpdateType>
    {
        /// <summary>
        /// Storage manager mode.
        /// </summary>
        public SessionStorageMode Mode { get; }

        private ConcurrentDictionary<SessionStorageDescriptor, ISessionStorage> _sessionStorages;

        /// <summary>
        /// Creates new <see cref="SessionStorageManager"/> with specified mode.
        /// </summary>
        /// <param name="mode"></param>
        public SessionStorageManager(SessionStorageMode mode)
        {
            Mode = mode;
            _sessionStorages = new ConcurrentDictionary<SessionStorageDescriptor, ISessionStorage>();
        }

        /// <summary>
        /// Gets all existing storages.
        /// </summary>
        /// <returns>Existing storages.</returns>
        public IReadOnlyDictionary<SessionStorageDescriptor, ISessionStorage> GetStorages()
        {
            return new ReadOnlyDictionary<SessionStorageDescriptor, ISessionStorage>(_sessionStorages);
        }

        /// <summary>
        /// Gets or creates session storage.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Session entity type.</typeparam>
        /// <returns>Session storage.</returns>
        public ISessionStorage GetOrCreateStorage<TEntity>(TOwner owner, TUpdateType entityType)
        {
            switch (Mode)
            {
                case SessionStorageMode.PerUser:
                    return GetOrCreate(new UserBasedSessionStorageDescriptor<TOwner>(owner));

                case SessionStorageMode.PerEntityType:
                    return GetOrCreate(new EntityBasedSessionStorageDescriptor<TOwner, TUpdateType>(owner, entityType, typeof(TEntity)));

                default:
                    return GetOrCreate(SessionStorageDescriptor.Shared);
            }
        }

        private ISessionStorage GetOrCreate(SessionStorageDescriptor descriptor)
        {
            if (_sessionStorages.TryGetValue(descriptor, out ISessionStorage globalStorage))
            {
                return globalStorage;
            }
            else
            {
                ISessionStorage newStorage = new SessionStorage();

                bool successfullAdding = false;

                while (!successfullAdding)
                {
                    if (!_sessionStorages.TryAdd(descriptor, newStorage))
                    {
                        if (_sessionStorages.TryGetValue(descriptor,
                            out ISessionStorage existingGlobalStorage))
                        {
                            if (_sessionStorages.TryUpdate(descriptor, existingGlobalStorage,
                                newStorage))
                            {
                                successfullAdding = true;
                            }
                        }
                        else successfullAdding = true;
                    }
                    else successfullAdding = true;
                }

                return newStorage;
            }
        }
    }
}