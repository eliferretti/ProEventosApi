﻿using System.Threading.Tasks;
using ProEventos.Aplication.Dtos;
using ProEventos.Persistence.Models;

namespace ProEventos.Aplication.Contratos
{
    public interface IPalestranteService
    {
        Task<PalestranteDto> AddPalestrante(int userId, PalestranteAddDto model);
        Task<PalestranteDto> UpdatePalestrante(int userId, PalestranteUpdateDto model);
        Task<PageList<PalestranteDto>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false);
        Task<PalestranteDto> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false);
    }
}
