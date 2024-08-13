namespace Labo.BLL.DTO.Tournaments
{
    public class TournamentIndexDTO(int total, IEnumerable<TournamentDTO> results)
    {
        public int Total { get; set; } = total;
        public IEnumerable<TournamentDTO> Results { get; set; } = results;
    }
}
