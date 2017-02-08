using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using SimpleInjector;

namespace BlueSkynet.Web.Configuration
{
    public sealed class SimpleInjectorControllerActivator : IControllerActivator
    {
        private readonly Container _container;

        public SimpleInjectorControllerActivator(Container c)
        {
            _container = c;
        }

        public object Create(ControllerContext c) =>
           _container.GetInstance(c.ActionDescriptor.ControllerTypeInfo.AsType());

        public void Release(ControllerContext c, object controller)
        {
        }
    }
}