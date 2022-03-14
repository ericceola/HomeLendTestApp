using HomeLend.Domain.Entities;
using System.Threading.Tasks;

namespace HomeLend.Domain.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetByIdAsync(int clientId);
    }
}
