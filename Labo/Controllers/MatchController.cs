using Labo.BLL.DTO.Matches;
using Labo.BLL.Exceptions;
using Labo.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController(IMatchService matchService) : ControllerBase
    {

        [HttpGet]
        public IActionResult Get([FromQuery]Guid tournamentId, [FromQuery]int? round)
        {
            return Ok(matchService.Get(tournamentId, round));
        }

        [HttpPatch("{id}/result")]
        [Authorize(Roles = "Admin")]
        public IActionResult Patch([FromRoute] int id, [FromBody]MatchResultDTO dto)
        {
            try
            {
                matchService.UpdateResult(id, dto.Result);
                return NoContent();
            }
            catch(TournamentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
