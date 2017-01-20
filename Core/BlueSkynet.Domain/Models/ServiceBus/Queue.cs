namespace BlueSkynet.Domain.Models.ServiceBus
{
    public class Queue : BaseQueue
    {
        public Queue(string name)
        {
            Name = name;
            MessageCount = 0;
            DeadLetterCount = 0;
        }
    }
}