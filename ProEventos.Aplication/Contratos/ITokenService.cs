using ProEventos.Aplication.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Aplication.Contratos
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}
