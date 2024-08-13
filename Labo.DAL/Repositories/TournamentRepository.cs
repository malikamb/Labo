using Labo.BLL.Interfaces;
using Labo.DAL.Contexts;
using Labo.DL.Entities;
using Labo.DL.Enums;
using Microsoft.EntityFrameworkCore;
using ToolBox.EF.Repository;

namespace Labo.DAL.Repositories
{
    public class TournamentRepository(TournamentContext context) : RepositoryBase<Tournament>(context), ITournamentRepository
    {
        public IEnumerable<Tournament> FindWithPlayersByCriteriaOrderByCreationDateDesc(
            string? name,
            TournamentCategory? category,
            IEnumerable<TournamentStatus>? statuses,
            bool womenOnly = false,
            int offset = 0,
            int limit = 10
        )
        {
            return Entities
                .Include(t => t.Players)
                .Where(t => name == null || t.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                .Where(t => category == null || t.Categories.HasFlag((TournamentCategory)category))
                .Where(t => statuses == null || !statuses.Any() || statuses.Contains(t.Status))
                .Where(t => !womenOnly || t.WomenOnly)
                .OrderByDescending(t => t.UpdateDate)
                .Skip(offset)
                .Take(limit);
        }

        public int CountByCriteria(
            string? name,
            TournamentCategory? category,
            IEnumerable<TournamentStatus>? statuses,
            bool wonenOnly = false
        )
        {
            return Entities
                .Where(t => name == null || t.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                .Where(t => category == null || t.Categories.HasFlag((TournamentCategory)category))
                .Where(t => statuses == null || !statuses.Any() || statuses.Contains(t.Status))
                .Where(t => !wonenOnly || t.WomenOnly)
                .Count();
        }

        public Tournament? FindOneWithPlayers(Guid tournamentId)
        {
            return Entities.Include(t => t.Players).FirstOrDefault(t => t.Id == tournamentId);
        }

        public Tournament? FindOneWithPlayersAndMatches(Guid tournamentId)
        {
            return Entities.Include(t => t.Matches).Include(t => t.Players)
                .FirstOrDefault(t => t.Id == tournamentId);
        }

        public void AddPlayer(Tournament tournament, User user)
        {
            tournament.Players.Add(user);
            _context.SaveChanges();
        }

        public void RemovePlayer(Tournament tournament, User user)
        {
            tournament.Players.Remove(user);
            _context.SaveChanges();
        }
    }
}
