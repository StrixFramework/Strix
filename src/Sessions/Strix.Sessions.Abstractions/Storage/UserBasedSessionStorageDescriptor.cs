namespace Strix.Sessions.Abstractions.Storage
{
    /// <summary>
    /// User-based session storage descriptor.
    /// </summary>
    public class UserBasedSessionStorageDescriptor<TOwner> : SessionStorageDescriptor
    {
        /// <summary>
        /// Session owner id this descriptor related to.
        /// </summary>
        public TOwner Owner { get; }

        /// <summary>
        /// Creates new <see cref="UserBasedSessionStorageDescriptor{TOwner}"/> with specified session owner id.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        public UserBasedSessionStorageDescriptor(TOwner owner)
        {
            Owner = owner;
        }
    }
}