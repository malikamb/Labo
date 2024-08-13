using Labo.API.DTO;
using Labo.BLL.DTO.Users;
using Labo.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace Labo.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController(IAuthenticationService authenticationService, IJwtManager jwtManager) : ControllerBase
    {
        [HttpPost("login")]
        [Produces(typeof(TokenDTO))]
        public IActionResult Login(LoginDTO dto)
        {
            UserDTO connectedUser = authenticationService.Login(dto);
            string token = jwtManager.CreateToken(connectedUser.Id.ToString(), connectedUser.Email, connectedUser.Role.ToString());
            return Ok(new TokenDTO(token, connectedUser));
        }
    }
}
