using Homelend.Domain.AppConfiguration;
using HomeLend.Domain.Interfaces.Repositories;
using HomeLend.Domain.Interfaces.Service;
using HomeLend.Infrastructure.Data.Repositories;
using HomeLend.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace Homelend.Infrastructure.IoC
{
    public class ConfigureDependencyInjection
    {        
        public static IServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettings>();

            var services = new ServiceCollection();
            services.AddSingleton(configuration);
            services.AddSingleton(appSettings);
         

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserCreditService, UserCreditService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHttpClient("Credit", client =>
            {
                client.BaseAddress = new Uri(appSettings.EndPoint.Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services.BuildServiceProvider();
        }
    }
}
