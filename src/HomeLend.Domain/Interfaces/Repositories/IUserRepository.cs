using HomeLend.Domain.Entities;
using System.Threading.Tasks;

namespace HomeLend.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}
