using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Services.Commands;
using System.Collections.Generic;

namespace BlueSkynet.Domain.Services
{
    public interface IHandlesFactory
    {
        ICommand<T> Create<T>() where T : Command;

        IEnumerable<IHandles<T>> Get<T>() where T : Event;
    }
}