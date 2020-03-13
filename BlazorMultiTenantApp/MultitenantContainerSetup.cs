using Autofac;
using Autofac.Multitenant;
using BlazorMultiTenantApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BlazorMultiTenantApp
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
                    .InstancePerLifetimeScope());
            multitenantContainer.ConfigureTenant(
                "b",
                cb => cb
                    .RegisterType<WeatherForecastService>()
                    .InstancePerLifetimeScope());

            return multitenantContainer;
        }
    }
}
