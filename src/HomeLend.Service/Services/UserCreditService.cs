using Homelend.Domain.AppConfiguration;
using HomeLend.Domain.Enum;
using HomeLend.Domain.Interfaces.Repositories;
using HomeLend.Domain.Interfaces.Service;
using HomeLend.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeLend.Service.Services
{
    public class UserCreditService : IUserCreditService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppSettings _appSettings;
        private readonly IClientRepository _clientRepository;

        public UserCreditService(AppSettings appSettings, IHttpClientFactory clientFactory, IClientRepository clientRepository)
        {
            _appSettings = appSettings;
            _clientFactory = clientFactory;
            _clientRepository = clientRepository;
            
        }

        
        public async Task<UserCredit> Get(int clientId)
        {
            var cliente = await _clientRepository.GetByIdAsync(clientId);

            UserCredit userCredit = null;

            decimal creditLimit= await GetCreditLimitAsync();

            if (cliente != null)
            {
                switch (cliente.Name)
                {
                    case nameof(TypeEnum.VeryImportantClient):
                        userCredit = new UserCredit(false);
                        break;
                    case nameof(TypeEnum.ImportantClient):
                        userCredit = new UserCredit(true, creditLimit * 2);
                        break;
                    default:
                        userCredit = new UserCredit(true, creditLimit);
                        break;
                }
            }

            return userCredit;
        }


        public async Task<decimal> GetCreditLimitAsync()
        {
            try
            {
                var httpClient = _clientFactory.CreateClient("Credit");

                var response = await httpClient.GetAsync($"random?min=100&max=1000&count=1");

                response.EnsureSuccessStatusCode();
                string conteudo = response.Content.ReadAsStringAsync().Result;

                int[] resultados = JsonConvert.DeserializeObject<int[]>(conteudo);

                return resultados[0];

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar informações do cliente.  {ex.Message}");
            }
        }
    }
}
