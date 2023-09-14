using AutoMapper;
using ProEventos.Aplication.Contratos;
using ProEventos.Aplication.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Models;
using System;
using System.Threading.Tasks;

namespace ProEventos.Aplication
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        private readonly IMapper _mapper;
        public EventoService(IGeralPersist geralPersist, 
                             IEventoPersist eventoPersist,
                             IMapper mapper)
        {
            _mapper = mapper;
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
        }

        public async Task<EventoDto> AddEventos(int userId, EventoDto model)
        {
            var evento = _mapper.Map<Evento>(model);
            evento.UserId = userId;
            try
            {
                _geralPersist.Add<Evento>(evento);
                if (await _geralPersist.SaveChangesAsync()) 
                {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch(Exception ex)
            { 
                throw new Exception(ex.ToString());
            }
        }

        public async Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model)
        {
            try 
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;
                model.UserId = userId;
                
                _mapper.Map(model, evento);

                _geralPersist.Update(evento);
                if (await _geralPersist.SaveChangesAsync()) 
                {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
                    return _mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.ToString());
            }
        }

       
        public async Task<bool> DeleteEvento(int userId, int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);

                if (evento == null) throw new Exception("Evento não encontrado");

                _geralPersist.Delete<Evento>(evento);
                //var status = await _geralPersist.SaveChangesAsync();
                return await _geralPersist.SaveChangesAsync();
                //var resultado = _mapper.Map<EventoDto>(evento);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        public async Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
        {
            try 
            { 
                var eventos = await _eventoPersist.GetAllEventosAsync(userId, pageParams, includePalestrantes);
                if(eventos == null) return null;
                
                var resultado = _mapper.Map<PageList<EventoDto>>(eventos);

                resultado.CurrentPage = eventos.CurrentPage;
                resultado.TotalPages = eventos.TotalPages;
                resultado.PageSize = eventos.PageSize;
                resultado.TotalCount = eventos.TotalCount;

                return resultado;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, includePalestrantes);
                if (evento == null) return null;

                var resultado = _mapper.Map<EventoDto>(evento);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
