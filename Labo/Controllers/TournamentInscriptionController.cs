using Labo.API.Extensions;
using Labo.BLL.Exceptions;
using Labo.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentInscriptionController(ITournamentService tournamentService) : ControllerBase
    {
        [HttpPost("{id}")]
        [Authorize]
        public IActionResult Post(Guid id)
        {
            try
            {
                tournamentService.Register(User.GetId(), id);
                return NoContent();
            }
            catch (TournamentRegistrationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            try
            {
                tournamentService.Unregister(User.GetId(), id);
                return NoContent();
            }
            catch (TournamentRegistrationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
