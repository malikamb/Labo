using Labo.DL.Enums;

namespace Labo.BLL.DTO.Tournaments
{
    public class TournamentSearchDTO
    {
        public int Offset { get; set; } = 0;
        public string? Name { get; set; }
        public bool WomenOnly { get; set; }
        public TournamentCategory? Category { get; set; }
        public IEnumerable<TournamentStatus>? Statuses { get; set; } = [TournamentStatus.WaitingForPlayers, TournamentStatus.InProgress];
    }
}
