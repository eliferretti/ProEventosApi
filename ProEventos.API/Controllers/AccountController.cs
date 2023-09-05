using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extencions;
using ProEventos.Aplication.Contratos;
using ProEventos.Aplication.Dtos;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, 
                                 ITokenService tokenService )
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try 
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserbyUserNameAsync(userName);
                return Ok(user);
            } 
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar Usuário. Erro: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.UserName))
                    return BadRequest("Usuário já existe");

                var user = await _accountService.CreateAccountAsync(userDto);
 
                if (user != null)
                    return Ok( new
                    {
                        userName = user.UserName,
                        primeiroNome = user.PrimeiroNome,
                        token = await _tokenService.CreateToken(user)
                    });

                return BadRequest("Usuário não criado, tente novamente mais tarde!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar registrar usuário. Erro: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                if (userUpdateDto.UserName != User.GetUserName()) 
                    return Unauthorized("Usuário inválido"); 

                var user = await _accountService.GetUserbyUserNameAsync(User.GetUserName());

                if (user == null) 
                    return Unauthorized("Usuário inválido");

                var userReturn = await _accountService.UpdateAccount(userUpdateDto);

                if (userReturn == null) 
                    return NoContent();

                return Ok(new
                {
                    userName = userReturn.UserName,
                    primeiroNome = userReturn.PrimeiroNome,
                    token = await _tokenService.CreateToken(userReturn)
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar editar Usuário. Erro: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _accountService.GetUserbyUserNameAsync(userLoginDto.Username);
                if (user == null) return Unauthorized("Usuário ou senha inválido.");

                var result = await _accountService.CheckUserPasswordAsync(user, userLoginDto.Password);
                if (!result.Succeeded) return Unauthorized("Usuário ou senha inválido.");

                return Ok( new
                {
                    userName = user.UserName,
                    primeiroNome = user.PrimeiroNome,
                    token = await _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar fazer login. Erro: {ex.Message}");
            }
        }

    }
}
