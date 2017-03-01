using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace ServiceBusWatcher.Extentions
{
    public static class BrokeredMessageExtentions
    {
        public static T GetBody<T>(this BrokeredMessage message) => JsonConvert.DeserializeObject<T>(message.GetBody<string>());
    }
}