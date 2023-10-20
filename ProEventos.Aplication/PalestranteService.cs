﻿using AutoMapper;
using ProEventos.Aplication.Contratos;
using ProEventos.Aplication.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Models;
using System;
using System.Threading.Tasks;

namespace ProEventos.Aplication
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IPalestrantePersist _palestrantePersist;
        private readonly IMapper _mapper;
        public PalestranteService(IPalestrantePersist palestrantePersist, IMapper mapper)
        {
            _mapper = mapper;
            _palestrantePersist = palestrantePersist;
        }

        public async Task<PalestranteDto> AddPalestrante(int userId, PalestranteAddDto model)
        {
            var palestrante = _mapper.Map<Palestrante>(model);
            palestrante.UserId = userId;
            try
            {
                _palestrantePersist.Add<Palestrante>(palestrante);
                if (await _palestrantePersist.SaveChangesAsync())
                {
                    var palestranteRetorno = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);
                    return _mapper.Map<PalestranteDto>(palestranteRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<PalestranteDto> UpdatePalestrante(int userId, PalestranteUpdateDto model)
        {
            try
            {
                var palestrante = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);
                if (palestrante == null) return null;

                model.Id = palestrante.Id;
                model.UserId = userId;

                _mapper.Map(model, palestrante);

                _palestrantePersist.Update(palestrante);
                if (await _palestrantePersist.SaveChangesAsync())
                {
                    var palestranteRetorno = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);
                    return _mapper.Map<PalestranteDto>(palestranteRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<PageList<PalestranteDto>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false)
        {
            try
            {
                var palestrantes = await _palestrantePersist.GetAllPalestrantesAsync(pageParams, includeEventos);
                if (palestrantes == null) return null;

                var resultado = _mapper.Map<PageList<PalestranteDto>>(palestrantes);

                resultado.CurrentPage = palestrantes.CurrentPage;
                resultado.TotalPages = palestrantes.TotalPages;
                resultado.PageSize = palestrantes.PageSize;
                resultado.TotalCount = palestrantes.TotalCount;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<PalestranteDto> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false)
        {
            try
            {
                var palestrante = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, includeEventos);
                if (palestrante == null) return null;

                var resultado = _mapper.Map<PalestranteDto>(palestrante);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
