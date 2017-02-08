namespace BlueSkynet.Domain.Models.ServiceBus.Models
{
    public abstract class BaseQueue
    {
        public string Name { get; set; }
        public int MessageCount { get; set; }
        public int DeadLetterCount { get; set; }
    }
}