using Homelend.Domain.AppConfiguration;
using HomeLend.Domain.Entities;
using HomeLend.Domain.Interfaces.Repositories;
using HomeLend.Domain.Interfaces.Service;
using System;
using System.Threading.Tasks;

namespace HomeLend.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserCreditService _userCreditService;
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        public UserService(AppSettings appSettings, IUserCreditService userCreditService, IUserRepository userRepository)
        {
            _userCreditService = userCreditService;
            _userRepository = userRepository;
            _appSettings = appSettings;
        }

        public async Task<string> Add(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            try
            {
                var userCredit = await _userCreditService.Get(clientId);

                if(userCredit == null)
                {
                    return "User Credit not found.";
                }

                var user = new User(firstName, lastName, email, dateOfBirth, clientId, userCredit.HasCreditLimit, userCredit.CreditLimit);

                await _userRepository.AddAsync(user);

                return "User added successfully.";
            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
        }
    }
}
