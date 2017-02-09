using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace BlueSkynet.Infrastructure
{
    public class EventBus : ICommandSender, IEventPublisher
    {
        private readonly IHandlesFactory _handlesFactory;
        private readonly IServiceProvider _serviceProvider;

        public EventBus(IHandlesFactory handlesFactory,
            IServiceProvider serviceProvider)
        {
            _handlesFactory = handlesFactory;
            _serviceProvider = serviceProvider;
        }

        public void Send<T>(T command) where T : Command
        {
            var handler = _handlesFactory.Create<T>();
            handler.Execute(command);
        }

        public void Publish<T>(T @event) where T : Event
        {
            //TODO move to HandelerFactory

            // Magic need to be simplified and readable.
            // This get all the handelers from IOC Container.
            // Can't go to IServiceProvider.GetServices<IHandles<T>>() because T is Event

            var genericHandeler = typeof(IHandles<>);
            var specific = genericHandeler.MakeGenericType(@event.GetType());
            var handleMethod = specific.GetMethod(nameof(IHandles<T>.Handle),
                BindingFlags.Instance | BindingFlags.Public);
            var handelers = _serviceProvider.GetServices(specific);
            foreach (var handeler in handelers)
            {
                handleMethod.Invoke(handeler, new object[] { @event });
            }
        }
    }
}