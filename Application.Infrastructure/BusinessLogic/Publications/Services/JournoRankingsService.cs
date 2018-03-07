using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces;

namespace Application.Core.Services
{
    public class JournoRankingsService: IJournoRankingsService
    {
        private readonly IAppLogger<JournoRankingsService> _logger;
        private readonly IAsyncRepository<JournoRanking> _journoRankingsRepository;

        public JournoRankingsService(
            IAsyncRepository<JournoRanking> journoRankingsRepository,
            IAppLogger<JournoRankingsService> logger
            )
        {
            _logger = logger;
            _journoRankingsRepository = journoRankingsRepository;
        }

        public async Task CreateJournoRankingAsync(JournoRanking journoRanking)
        {
            await _journoRankingsRepository.AddAsync(journoRanking);
        }

        public async Task DeleteJournoRankingsAsync(JournoRanking journoRanking)
        {
            await _journoRankingsRepository.DeleteAsync(journoRanking);
        }

        public async Task SetJournoRankingAsync(JournoRanking journoRanking)
        {
            await _journoRankingsRepository.UpdateAsync(journoRanking);
        }
    }
}
