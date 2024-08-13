using Labo.DL.Entities;
using Labo.DL.Enums;

namespace Labo.BLL.DTO.Tournaments
{
    public class TournamentDTO(Tournament tournament)
    {
        public Guid Id { get; set; } = tournament.Id;
        public string Name { get; set; } = tournament.Name;
        public string? Location { get; set; } = tournament.Location;
        public int MinPlayers { get; set; } = tournament.MinPlayers;
        public int MaxPlayers { get; set; } = tournament.MaxPlayers;
        public int? EloMin { get; set; } = tournament.EloMin;
        public int? EloMax { get; set; } = tournament.EloMax;
        public bool WomenOnly { get; set; } = tournament.WomenOnly;
        public DateTime EndOfRegistrationDate { get; set; } = tournament.EndOfRegistrationDate;
        public int Count { get; set; } = tournament.Players.Count;
        public bool CanRegister { get; set; }
        public bool IsRegistered { get; set; }
        public int CurrentRound { get; set; } = tournament.CurrentRound;
        public TournamentStatus Status { get; set; } = tournament.Status;
        public IEnumerable<TournamentCategory> Categories { get; set; } = Enum.GetValues<TournamentCategory>().Where(v => tournament.Categories.HasFlag(v));
    }
}
