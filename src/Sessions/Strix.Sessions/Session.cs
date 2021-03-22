#region

using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Strix.Sessions.Abstractions;
using Strix.Sessions.Abstractions.Storage;

#endregion

namespace Strix.Sessions
{
    /// <summary>
    /// Base implementation of <see cref="ISession{TEntityбTOwner,TUpdateType}"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public class Session<TEntity, TOwner, TUpdateType> : ISession<TEntity, TOwner, TUpdateType>
    {
        /// <summary>
        /// Session owner.
        /// </summary>
        public TOwner Owner { get; }

        /// <summary>
        /// Session entity type.
        /// </summary>
        public TUpdateType EntityType { get; }

        /// <summary>
        /// Session storage.
        /// </summary>
        public ISessionStorage Storage { get; }

        /// <summary>
        /// Indicated update entity request status.
        /// </summary>
        public bool IsUpdateEntityRequested { get; private set; }

        /// <summary>
        /// Channel writer, used to writing received entities if <see cref="IsUpdateEntityRequested"/> is set to <see langword="true"/>.
        /// </summary>
        public ChannelWriter<TEntity> UpdateEntityChannelWriter { get; }

        private readonly ChannelReader<TEntity> _updateEntityChannelReader;

        /// <summary>
        /// Creates new <see cref="Session{TEntity,TOwner,TUpdateType}"/> with specifier session owner, session entity type and update entity channel. 
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <param name="updateEntityChannel">Update entity channel.</param>
        /// <param name="storage">Session storage.</param>
        public Session(TOwner owner, TUpdateType entityType, Channel<TEntity> updateEntityChannel,
            ISessionStorage storage)
        {
            Owner = owner;
            EntityType = entityType;
            UpdateEntityChannelWriter = updateEntityChannel;
            _updateEntityChannelReader = updateEntityChannel;
            Storage = storage;
        }

        /// <summary>
        /// Requests an entity.
        /// </summary>
        /// <param name="sessionRequestOptions">Session entity request options.</param>
        /// <param name="cancellationToken">Cancellation token for task cancellation.</param>
        /// <returns>Requested entity.</returns>
        public ValueTask<ISessionUpdateEntityRequestResult<TEntity>> RequestEntityAsync(
            ISessionUpdateEntityRequestOptions<TEntity> sessionRequestOptions,
            CancellationToken cancellationToken = default)
        {
            return new ValueTask<ISessionUpdateEntityRequestResult<TEntity>>(Task.Factory.StartNew(() =>
                RequestEntityAsyncInternal(sessionRequestOptions, cancellationToken), cancellationToken));
        }

        private ISessionUpdateEntityRequestResult<TEntity> RequestEntityAsyncInternal(
            ISessionUpdateEntityRequestOptions<TEntity> sessionRequestOptions,
            CancellationToken cancellationToken = default)
        {
            if (IsUpdateEntityRequested)
                throw new InvalidOperationException("Another request already running.");

            IsUpdateEntityRequested = true;

            SessionUpdateEntityRequestState<TEntity> sessionUpdateEntityRequestState
                = new SessionUpdateEntityRequestState<TEntity>();

            TEntity lastUpdateEntity = default;

            while (sessionUpdateEntityRequestState.CurrentAttempt != sessionRequestOptions.Attempts)
            {
                sessionRequestOptions.Action?.Invoke(sessionUpdateEntityRequestState);

                while (!cancellationToken.IsCancellationRequested)
                {
                    if (_updateEntityChannelReader.TryRead(out lastUpdateEntity))
                        break;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    IsUpdateEntityRequested = false;
                    break;
                }

                sessionUpdateEntityRequestState.Next(lastUpdateEntity);

                bool updateEntityMatchResult =
                    sessionRequestOptions.Matcher?.Invoke(sessionUpdateEntityRequestState) ?? true;

                if (updateEntityMatchResult)
                {
                    IsUpdateEntityRequested = false;
                    return SessionUpdateEntityRequestResult<TEntity>.Success(lastUpdateEntity,
                        sessionUpdateEntityRequestState.CurrentAttempt);
                }
            }

            return SessionUpdateEntityRequestResult<TEntity>.Fail(sessionUpdateEntityRequestState.CurrentAttempt);
        }
    }
}