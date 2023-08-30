using ProEventos.Domain.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUsersByIdAsync(int id);
        Task<User> GetUsersByUserNameAsync(string userName);
    }
}
