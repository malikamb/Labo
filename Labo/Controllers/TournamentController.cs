using Labo.API.DTO;
using Labo.API.Extensions;
using Labo.BLL.DTO.Tournaments;
using Labo.BLL.Exceptions;
using Labo.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Labo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController(ITournamentService tournamentService) : ControllerBase
    {
        [HttpGet]
        [Produces(typeof(TournamentIndexDTO))]
        public IActionResult Get([FromQuery] TournamentSearchDTO criteria)
        {
            int total = tournamentService.Count(criteria);
            Response.AddTotalHeader(total);
            return Ok(new TournamentIndexDTO(total, tournamentService.Find(criteria, User.GetId())));
        }

        [HttpGet("{id}")]
        [Produces(typeof(IEnumerable<TournamentDetailsDTO>))]
        public IActionResult Get(Guid id)
        {
            TournamentDetailsDTO dto = tournamentService.GetWithPlayers(id, User.GetId());
            return Ok(dto);
        }

        [HttpPost]
        [Produces(typeof(Guid))]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] TournamentAddDTO dto)
        {
            Guid id = tournamentService.Add(dto);
            return Ok(id);
        }

        [HttpPatch("{id}/start")]
        [Authorize(Roles = "Admin")]
        public IActionResult Start(Guid id)
        {
            tournamentService.Start(id);
            return NoContent();
        }

        [HttpPatch("{id}/nextRound")]
        [Authorize(Roles = "Admin")]
        public IActionResult ValidateRound(Guid id)
        {
            try
            {
                tournamentService.ValidateRound(id);
                return NoContent();
            }
            catch (TournamentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Guid))]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                tournamentService.Remove(id);
                return Ok(id);
            }
            catch (TournamentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
