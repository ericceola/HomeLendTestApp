using Homelend.Infrastructure.IoC;
using HomeLend.Domain.Interfaces.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Homelend
{
    class Program
    {
        static async Task Main(string[] args)
        {

           
            try
            {
                Console.WriteLine("******************** START PROCESS ADD USER ************************\n");
                

                if (args.Length != 5)
                    System.Console.WriteLine("Check if the parameters are correct (firstName, lastName, email, dataNascimento, clientId).");

                var firstName = args[0];
                var lastName = args[1];
                var email = args[2];
                var age =  DateTime.Parse(args[3]);
                var clientId = int.Parse(args[4]);

               var messageResult =  await AddUser(firstName, lastName, email, age, clientId);

                Console.WriteLine(messageResult + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Notice: {ex.Message}\n");
               
            }

            Console.WriteLine("******************** END PROCESS ADD USER ************************\n");
        }

        public static async Task<string> AddUser(string firstName, string lastName, string email, DateTime age, int clientId)
        {
            string messageResult = string.Empty;
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            environmentName = string.IsNullOrWhiteSpace(environmentName) ? "Production" : environmentName;

            var config = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables()
                   .Build();

            var servicesProvider = ConfigureDependencyInjection.ConfigureServices(config);
            using (servicesProvider as IDisposable)
            {
                var user = servicesProvider.GetRequiredService<IUserService>();
                messageResult = await user.Add(firstName, lastName, email, age, clientId);
            }

            return messageResult;
        }
    }
}
