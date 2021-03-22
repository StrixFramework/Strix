#region

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using Strix.Sessions.Abstractions;
using Strix.Sessions.Abstractions.Storage;
using Strix.Sessions.Storage;

#endregion

namespace Strix.Sessions
{
    /// <summary>
    /// Base implementation of <see cref="ISessionManager{TOwner,TUpdateType}"/>.
    /// </summary>
    public class SessionManager<TOwner, TUpdateType> : ISessionManager<TOwner, TUpdateType>
    {
        public ISessionStorageManager<TOwner, TUpdateType> StorageManager { get; }

        private readonly ConcurrentDictionary<TOwner, IList<ISession<TOwner, TUpdateType>>> _sessions;

        /// <summary>
        /// Creates new session manager.
        /// </summary>
        public SessionManager(SessionStorageMode sessionStorageMode = SessionStorageMode.PerUser)
        {
            _sessions = new ConcurrentDictionary<TOwner, IList<ISession<TOwner, TUpdateType>>>();

            StorageManager = new SessionStorageManager<TOwner, TUpdateType>(sessionStorageMode);
        }

        /// <summary>
        /// Enumerates all the sessions, related to the current session manager.
        /// </summary>
        /// <returns>Sessions collection.</returns>
        public IEnumerable<ISession<TOwner, TUpdateType>> GetSessions()
        {
            return _sessions.Values.SelectMany(x => x);
        }

        /// <summary>
        /// Adds a new session.
        /// </summary>
        /// <param name="session">Session to add.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        public void AddSession<TEntity>(ISession<TEntity, TOwner, TUpdateType> session)
        {
            if (_sessions.TryGetValue(session.Owner, out IList<ISession<TOwner, TUpdateType>> existingSessions))
            {
                foreach (ISession<TOwner, TUpdateType> existingSession in existingSessions.ToList())
                {
                    if (!(existingSession is ISession<TEntity, TOwner, TUpdateType> typedSession &&
                          typedSession.EntityType.Equals(session.EntityType)))
                    {
                        existingSessions.Add(session);
                    }

                    break;
                }
            }
            else
            {
                _sessions[session.Owner] = new List<ISession<TOwner, TUpdateType>> { session };
            }
        }

        /// <summary>
        /// Creates a new session without adding it to the session manager.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>Created session</returns>
        public ISession<TEntity, TOwner, TUpdateType> CreateSession<TEntity>(TOwner owner, TUpdateType entityType)
        {
            return new Session<TEntity, TOwner, TUpdateType>(owner, entityType, Channel.CreateUnbounded<TEntity>(),
                StorageManager.GetOrCreateStorage<TEntity>(owner, entityType));
        }

        /// <summary>
        /// Checks a session for existence.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>Session existence.</returns>
        public bool IsSessionExists<TEntity>(TOwner owner, TUpdateType entityType)
        {
            if (_sessions.TryGetValue(owner, out IList<ISession<TOwner, TUpdateType>> existingSessions))
            {
                foreach (ISession<TOwner, TUpdateType> existingSession in existingSessions)
                {
                    if (existingSession is ISession<TEntity, TOwner, TUpdateType> typedSession
                        && typedSession.EntityType.Equals(entityType))
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a session. Returns <see langword="null"/>, if session doesn't exists.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>A session.</returns>
        public ISession<TEntity, TOwner, TUpdateType> GetSession<TEntity>(TOwner owner, TUpdateType entityType)
        {
            if (_sessions.TryGetValue(owner, out IList<ISession<TOwner, TUpdateType>> existingSessions))
            {
                foreach (ISession<TOwner, TUpdateType> existingSession in existingSessions)
                {
                    if (existingSession is ISession<TEntity, TOwner, TUpdateType> typedSession
                        && typedSession.EntityType.Equals(entityType))
                    {
                        return typedSession;
                    }
                }

                return null;
            }
            else
            {
                return null;
            }
        }
    }
}