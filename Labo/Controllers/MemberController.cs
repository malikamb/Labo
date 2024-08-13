using Labo.API.Extensions;
using Labo.BLL.DTO.Users;
using Labo.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Labo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController(IMemberService memberService) : ControllerBase
    {

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostAsync([FromBody] MemberFormDTO dto)
        {
            try
            {
                await memberService.AddAsync(dto);
                return NoContent();
            }
            catch (SmtpFailedRecipientException)
            {
                return BadRequest("Invalid Email");
            }
        }

        [HttpPatch("password")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            memberService.ChangePassword(User.GetId(), dto);
            return NoContent();
        }

        [HttpHead("existsEmail")]
        public IActionResult ExistsEmail([FromQuery] ExistsEmailDTO dto)
        {
            if (memberService.ExistsEmail(dto))
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpHead("existsUsername")]
        public IActionResult ExistUsername([FromQuery] ExistsUsernameDTO dto)
        {
            if (memberService.ExistsUsername(dto))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
