namespace BlueSkynet.Infrastructure.ReadModels.ServiceBus
{
    public class ServiceBusQueue
    {
        public ServiceBusQueue()
        {
            MessageCount = 0;
            DeadLetterMessateCount = 0;
        }

        public string Name { get; set; }
        public int MessageCount { get; set; }
        public int DeadLetterMessateCount { get; set; }
    }
}