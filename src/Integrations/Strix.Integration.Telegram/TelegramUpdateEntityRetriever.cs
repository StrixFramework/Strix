using System;
using System.Runtime.InteropServices.ComTypes;
using Strix.Abstractions.Processing;
using Telegram.Bot.Types.Enums;

namespace Strix.Integration.Telegram
{
    public class TelegramUpdateEntityRetriever : IUpdateEntityRetriever<TelegramUpdate, UpdateType>
    {
        public TInnerEntity RetrieveEntity<TInnerEntity>(TelegramUpdate update)
        {
            return (TInnerEntity)update.GetType().GetProperty(typeof(TInnerEntity).Name)!.GetValue(update);
        }
    }
}