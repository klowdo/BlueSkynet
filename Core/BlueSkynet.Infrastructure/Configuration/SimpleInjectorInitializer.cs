using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models.ServiceBus.Events;
using BlueSkynet.Domain.Repository;
using BlueSkynet.Domain.Services;
using BlueSkynet.Domain.Services.Commands;
using BlueSkynet.Infrastructure.Queries.ServiceBus;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using SimpleInjector;
using System;

namespace BlueSkynet.Infrastructure.Configuration
{
    public static class SimpleInjectorInitializer
    {
        public static Container Initialize(IConfiguration config, IServiceCollection services)
        {
            var client = CloudStorageAccount.DevelopmentStorageAccount.CreateCloudTableClient();
            var table = client.GetTableReference("Streams");
            table.CreateIfNotExists();
            var container = new Container();
            container.RegisterCollection(services);

            // register app services
            container.Register(typeof(IQuery<,>), new[] { typeof(IQuery<,>).Assembly });
            container.Register(typeof(ICommand<>), new[] { typeof(ICommand<>).Assembly });
            container.Register(typeof(IAsyncCommand<>), new[] { typeof(IAsyncCommand<>).Assembly });
            container.RegisterCollection(typeof(IHandles<>), typeof(ServiceBusItemListView).Assembly);
            container.Register<IDataContext>(() => new LiteBlueSkynetDatabase("MyDb.db"));
            container.Register<IHandlesFactory, HandelerFactory>();
            container.Register<IServiceProvider>(() => container);
            container.Register<IEventPublisher, EventBus>();
            container.Register<ICommandSender, EventBus>();
            container.Register(typeof(IRepository<>), typeof(EventRepository<>));
            container.Register<IEventStore, EventStore>();
            container.Register<ISerializer, Services.JsonSerializer>();
            container.Register(() => table);
            container.Verify();
            var ppp = container.GetAllInstances<IHandles<ServiceBusCreated>>();

            return container;
        }
    }
}