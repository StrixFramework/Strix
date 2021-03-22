using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Strix.Widgets.Extensions
{
    public static class ChannelExtensions
    {
        public static async ValueTask<long> ReadUntilCancelledAsync<T>(
            this ChannelReader<T> reader,
            CancellationToken cancellationToken,
            Func<T, long, ValueTask> receiver,
            bool deferredExecution = false)
        {
            if (reader is null) throw new ArgumentNullException(nameof(reader));
            if (receiver is null) throw new ArgumentNullException(nameof(receiver));

            if (deferredExecution)
                await Task.Yield();

            long index = 0;
            try
            {
                do
                {
                    var next = new ValueTask();
                    while (
                        !cancellationToken.IsCancellationRequested
                        && reader.TryRead(out var item))
                    {
                        await next.ConfigureAwait(false);
                        next = receiver(item, index++);
                    }
                    await next.ConfigureAwait(false);
                }
                while (
                    !cancellationToken.IsCancellationRequested
                    && await reader.WaitToReadAsync(cancellationToken).ConfigureAwait(false));
            }
            catch (TaskCanceledException)
            {
                // In case WaitToReadAsync is cancelled.
            }

            return index;
        }
    }
}