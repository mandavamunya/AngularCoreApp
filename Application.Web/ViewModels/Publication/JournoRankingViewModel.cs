using Application.Core.Enums;
using System;

namespace Application.Models.PublicationViewModel
{
    public class JournoRankingViewModel
    {
        public int JournalistId { get; set; }
        public string Name { get; set; }
        public int PageViews { get; set; }
        public int AveragePageViews { get; set; }
        public int Comments { get; set; }
        public int Articles { get; set; }
        public Team Team { get; set; }
        public DateTime Year { get; set; }
    }
}
