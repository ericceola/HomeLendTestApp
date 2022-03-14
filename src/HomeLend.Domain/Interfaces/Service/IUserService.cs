using System;
using System.Threading.Tasks;

namespace HomeLend.Domain.Interfaces.Service
{
    public interface IUserService
    {
        Task<string> Add(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId);
    }
}
