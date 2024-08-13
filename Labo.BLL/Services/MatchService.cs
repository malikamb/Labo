using Labo.BLL.DTO.Matches;
using Labo.BLL.DTO.Users;
using Labo.BLL.Exceptions;
using Labo.BLL.Interfaces;
using Labo.DL.Entities;
using Labo.DL.Enums;

namespace Labo.BLL.Services
{
    public class MatchService(IMatchRepository matchRepository, ITournamentRepository tournamentRepository) : IMatchService
    {
        public IEnumerable<MatchDTO> Get(Guid tournamentId, int? round)
        {
            Tournament? tournament = tournamentRepository.FindOne(tournamentId);
            if (tournament is null)
            {
                throw new KeyNotFoundException();
            }
            return matchRepository.FindWithPlayersByTournamentAndRound(tournamentId, round ?? tournament.CurrentRound)
                .Select(m => new MatchDTO(m));
        }

        public void UpdateResult(int id, MatchResult result)
        {
            Match? match = matchRepository.FindOneWithTournament(id);
            if (match is null)
            {
                throw new KeyNotFoundException();
            }
            if (match.Tournament.Status != TournamentStatus.InProgress)
            {
                throw new TournamentException("Cannot update a match the tournament is not in progress");
            }
            if (match.Tournament.CurrentRound != match.Round)
            {
                throw new TournamentException("Cannot update a match if the round has ended");
            }
            match.Result = result;
            matchRepository.Update(match);
        }

    }
}
