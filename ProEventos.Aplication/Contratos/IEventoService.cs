using System.Threading.Tasks;
using ProEventos.Aplication.Dtos;

namespace ProEventos.Aplication.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(int userId, EventoDto model); 
        Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int userId, int eventoId);
        Task<EventoDto[]> GetAllEventosAsync(int userId, bool includePalestrantes);
        Task<EventoDto[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes);
        Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes);
    }
}
