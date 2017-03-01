using Microsoft.Azure.WebJobs;
using ServiceBusWatcher.Configuration;

namespace ServiceBusWatcher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var jobConfiguration = new JobHostConfiguration
            {
                JobActivator = new ServiceBusWatcherActivator()
            };

            using (var host = new JobHost(jobConfiguration))
            {
                host.RunAndBlock();
            }
        }
    }
}