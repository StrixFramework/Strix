#region

using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Strix.Sessions.Abstractions.Storage;

#endregion

namespace Strix.Sessions.Abstractions
{
    /// <summary>
    /// Base interface for implementing session
    /// </summary>
    public interface ISession<TOwner, TUpdateType>
    {
        /// <summary>
        /// Session owner.
        /// </summary>
        TOwner Owner { get; }

        /// <summary>
        /// Session entity type.
        /// </summary>
        TUpdateType EntityType { get; }

        /// <summary>
        /// Session storage.
        /// </summary>
        ISessionStorage Storage { get; }
    }

    /// <summary>
    /// Base interface for implementing session
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ISession<TEntity, TOwner, TUpdateType> : ISession<TOwner, TUpdateType>
    {
        /// <summary>
        /// Indicated update entity request status.
        /// </summary>
        bool IsUpdateEntityRequested { get; }

        /// <summary>
        /// Channel writer, used to writing received entities if <see cref="IsUpdateEntityRequested"/> is set to <see langword="true"/>.
        /// </summary>
        ChannelWriter<TEntity> UpdateEntityChannelWriter { get; }

        /// <summary>
        /// Requests an entity.
        /// </summary>
        /// <param name="sessionRequestOptions">Session entity request options.</param>
        /// <param name="cancellationToken">Cancellation token for task cancellation.</param>
        /// <returns>Requested entity.</returns>
        ValueTask<ISessionUpdateEntityRequestResult<TEntity>> RequestEntityAsync(
            ISessionUpdateEntityRequestOptions<TEntity> sessionRequestOptions,
            CancellationToken cancellationToken = default);
    }
}