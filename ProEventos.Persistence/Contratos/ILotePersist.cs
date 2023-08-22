using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist //: IGeralPersist
    {
        /// <summary>
        ///     Método get retornará uma lista de lotes por eventoId.
        /// </summary>
        /// <param name="eventoId">Chave da tabela Eventos</param>
        /// <returns>Array de Lotes</returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        
        /// <summary>
        ///     Método get retornará apenas um Lote
        /// </summary>
        /// <param name="eventoId">Chave da tabela Evento</param>
        /// <param name="id">Chave da tabela Lote</param>
        /// <returns>Apenas 1 lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int id);
    }
}
