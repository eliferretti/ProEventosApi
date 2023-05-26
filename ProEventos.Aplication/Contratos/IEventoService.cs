using System.Threading.Tasks;
using ProEventos.Aplication.Dtos;

namespace ProEventos.Aplication.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(EventoDto model); 
        Task<EventoDto> UpdateEvento(int id, EventoDto model);
        Task<bool> DeleteEvento(int eventoId);
        Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes);
        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
        Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes);
    }
}
