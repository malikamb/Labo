using Labo.DL.Entities;
using Labo.DL.Enums;

namespace Labo.BLL.DTO.Matches
{
    public class MatchDTO(Match m)
    {
        public int Id { get; set; } = m.Id;
        public Guid TournamentId { get; set; } = m.TournamentId;
        public string BlackName { get; set; } = m.Black.Username;
        public Guid BlackId { get; set; } = m.BlackId;
        public string WhiteName { get; set; } = m.White.Username;
        public Guid WhiteId { get; set; } = m.WhiteId;
        public MatchResult Result { get; set; } = m.Result;
        public int Round { get; set; } = m.Round;
    }
}
