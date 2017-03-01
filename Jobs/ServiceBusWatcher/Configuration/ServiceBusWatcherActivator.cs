using Microsoft.Azure.WebJobs.Host;
using SimpleInjector;

namespace ServiceBusWatcher.Configuration
{
    internal class ServiceBusWatcherActivator : IJobActivator
    {
        private readonly Container _container;

        public ServiceBusWatcherActivator()
        {
            _container = new Container();
        }

        public T CreateInstance<T>() => (T)_container.GetInstance(typeof(T));
    }
}