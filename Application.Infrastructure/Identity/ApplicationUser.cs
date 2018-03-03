using Application.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Application.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<JournoRanking> JournalistRankings { get; set; }
    }
}
