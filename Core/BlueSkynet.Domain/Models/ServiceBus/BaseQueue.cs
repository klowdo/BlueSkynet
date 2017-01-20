namespace BlueSkynet.Domain.Models.ServiceBus
{
    public abstract class BaseQueue
    {
        public string Name { get; set; }
        public int MessageCount { get; set; }
        public int DeadLetterCount { get; set; }
    }
}