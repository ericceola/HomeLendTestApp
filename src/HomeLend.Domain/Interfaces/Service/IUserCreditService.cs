using HomeLend.Domain.Models;
using System.Threading.Tasks;

namespace HomeLend.Domain.Interfaces.Service
{
    public  interface IUserCreditService
    {
        Task<UserCredit> Get(int clientId);
    }
}
