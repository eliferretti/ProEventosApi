using ProEventos.Domain;
using ProEventos.Persistence.Models;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface IPalestrantePersist : IGeralPersist
    {
        Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includePalestrantes = false);
        Task<Palestrante> GetPalestranteByUserIdAsync(int userId, bool includePalestrantes = false);
    }
}
