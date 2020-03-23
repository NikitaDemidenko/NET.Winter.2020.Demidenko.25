using System;
using Microsoft.Extensions.DependencyInjection;
using DependencyResolver;
using Bll.Contract.Interfaces;

namespace SolidTaskConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ResolverConfig().CreateServiceProvider();

            var service = serviceProvider.GetService<IService>();

            service.Run();
        }
    }
}
