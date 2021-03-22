#region

using System.Collections.Generic;
using Strix.Sessions.Abstractions.Storage;

#endregion

namespace Strix.Sessions.Abstractions
{
    /// <summary>
    /// Base interface for implementing session manager.
    /// </summary>
    public interface ISessionManager<TOwner, TUpdateType>
    {
        /// <summary>
        /// Session storage manager.
        /// </summary>
        ISessionStorageManager<TOwner, TUpdateType> StorageManager { get; }

        /// <summary>
        /// Enumerates all the sessions, related to the current session manager.
        /// </summary>
        /// <returns>Sessions collection.</returns>
        IEnumerable<ISession<TOwner, TUpdateType>> GetSessions();

        /// <summary>
        /// Adds a new session.
        /// </summary>
        /// <param name="session">Session to add.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        void AddSession<TEntity>(ISession<TEntity, TOwner, TUpdateType> session);

        /// <summary>
        /// Creates a new session without adding it to the session manager.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>Created session</returns>
        ISession<TEntity, TOwner, TUpdateType> CreateSession<TEntity>(TOwner owner, TUpdateType entityType);

        /// <summary>
        /// Checks a session for existence.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>Session existence.</returns>
        bool IsSessionExists<TEntity>(TOwner owner, TUpdateType entityType);

        /// <summary>
        /// Gets a session. Returns <see langword="null"/>, if session doesn't exists.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>A session.</returns>
        ISession<TEntity, TOwner, TUpdateType> GetSession<TEntity>(TOwner owner, TUpdateType entityType);
    }
}