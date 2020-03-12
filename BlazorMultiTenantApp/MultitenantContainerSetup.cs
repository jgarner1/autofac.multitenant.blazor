using Autofac;
using Autofac.Multitenant;
using BlazorMultiTenentApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BlazorMultiTenentApp
{
    public static class MultitenantContainerSetup
    {
        public static MultitenantContainer ConfigureMultitenantContainer(IContainer container)
        {
            var strategy = new QueryStringTenantIdentificationStrategy(container.Resolve<IHttpContextAccessor>(),
                container.Resolve<ILogger<QueryStringTenantIdentificationStrategy>>());

            var multitenantContainer = new MultitenantContainer(strategy, container);

            multitenantContainer.ConfigureTenant(
                "a",
                cb => cb
                    .RegisterType<WeatherForecastService>()
                    .SingleInstance());
            multitenantContainer.ConfigureTenant(
                "b",
                cb => cb
                    .RegisterType<WeatherForecastService>()
                    .SingleInstance());

            return multitenantContainer;
        }
    }
}
