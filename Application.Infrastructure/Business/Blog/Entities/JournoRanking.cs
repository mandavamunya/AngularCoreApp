using Application.Infrastructure.Identity;
using System;

namespace Application.Core.Entities
{
    public class JournoRanking: BaseEntity
    {
        public int Ranking { get; set; }
        public double AverageViews { get; set; }
        public double AverageComments { get; set; }
        public int NumberOfPosts { get; set; }
        public DateTime Date { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
