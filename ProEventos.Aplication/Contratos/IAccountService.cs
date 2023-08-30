using Microsoft.AspNetCore.Identity;
using ProEventos.Aplication.Dtos;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProEventos.Aplication.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string userName);
        Task<UserUpdateDto> GetUserbyUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
        Task CheckUserPasswordAsync(ClaimsPrincipal user, string password);
    }
}
