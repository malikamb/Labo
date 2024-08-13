using Labo.BLL.Interfaces;
using Labo.DAL.Contexts;
using Labo.DL.Entities;
using Microsoft.EntityFrameworkCore;
using ToolBox.EF.Repository;

namespace Labo.DAL.Repositories
{
    public class MatchRepository(TournamentContext context) : RepositoryBase<Match>(context), IMatchRepository
    {
        public IEnumerable<Match> FindWithPlayersByTournamentAndRound(Guid tournamentId, int round)
        {
            return Entities
                .Include(m => m.White)
                .Include(m => m.Black)
                .Where(m => m.TournamentId == tournamentId && m.Round == round);
        }

        public Match? FindOneWithTournament(int id)
        {
            return Entities.Include(m => m.Tournament).FirstOrDefault(m => m.Id == id);
        }
    }
}
