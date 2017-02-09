using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Services;
using BlueSkynet.Domain.Services.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BlueSkynet.Infrastructure.Configuration
{
    public class HandelerFactory : IHandlesFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public HandelerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommand<T> Create<T>() where T : Command =>
            _serviceProvider.GetRequiredService<ICommand<T>>();

        public IEnumerable<IHandles<T>> Get<T>() where T : Event
        {
            throw new NotImplementedException();
        }
    }
}