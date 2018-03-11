using Application.Core.Entities;
using Application.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Application.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Team Team { get; set; }
        public Role Role { get; set; }

        public ICollection<JournoRanking> JournoRankings { get; set; }
    }
}
