using Labo.BLL.DTO.Matches;
using Labo.BLL.DTO.Users;
using Labo.DL.Entities;

namespace Labo.BLL.DTO.Tournaments
{
    public class TournamentDetailsDTO(Tournament tournament) : TournamentDTO(tournament)
    {
        public bool CanStart { get; set; }
        public bool CanValidateRound { get; set; }
        public IEnumerable<UserDTO> Players { get; set; } = tournament.Players.Select(p => new UserDTO(p));
        public IEnumerable<MatchDTO> Matches { get; set; } = tournament.Matches.Select(p => new MatchDTO(p));
    }
}
