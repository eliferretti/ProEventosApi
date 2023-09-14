using System.Threading.Tasks;
using ProEventos.Aplication.Dtos;
using ProEventos.Persistence.Models;

namespace ProEventos.Aplication.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(int userId, EventoDto model); 
        Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int userId, int eventoId);
        Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes);
        Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes);
    }
}
