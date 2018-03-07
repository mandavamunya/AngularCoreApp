﻿using Application.Core.Entities;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface IJournoRankingsService
    {
        Task CreateJournoRankingAsync(JournoRanking journoRanking);
        Task SetJournoRankingAsync(JournoRanking journoRanking);
        Task DeleteJournoRankingsAsync(JournoRanking journoRanking);
    }
}
