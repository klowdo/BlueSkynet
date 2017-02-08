using System;
using System.Collections.Generic;
using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Services.Commands;
using BlueSkynet.Infrastructure.Queries;
using BlueSkynet.Infrastructure.Queries.ServiceBus;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;
using Microsoft.AspNetCore.Mvc;

namespace BlueSkynet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuery<EmptyArgs, IEnumerable<ServiceBusItemListDto>> _serviceBusItemListDtoQuery;
        private readonly IQuery<ServiceBusByIdQueryArgs, ServiceBusItemListDto> _serviceBusByIdQuery;
        private readonly ICommandSender _commandSender;

        public HomeController(
            IQuery<EmptyArgs, IEnumerable<ServiceBusItemListDto>> serviceBusItemListDtoQuery,
            IQuery<ServiceBusByIdQueryArgs, ServiceBusItemListDto> serviceBusByIdQuery,
            ICommandSender commandSender
            )
        {
            _serviceBusItemListDtoQuery = serviceBusItemListDtoQuery;
            _serviceBusByIdQuery = serviceBusByIdQuery;
            _commandSender = commandSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult ServiceBus()
        {
            _commandSender.Send(new CreateServiceBusCommand(Guid.NewGuid(), "TEstarConnec", "Namn"));

            var items = _serviceBusItemListDtoQuery.Execute(new EmptyArgs());
            return View(items);
        }

        //public IActionResult ServiceBus(Guid id)
        //{
        //    var item = _serviceBusByIdQuery.Execute(new ServiceBusByIdQueryArgs(id));
        //    return View(item);
        //}

        public IActionResult Error()
        {
            return View();
        }
    }
}