using Application.Core.Enums;
using Application.Core.Specifications;
using Application.Infrastructure.Identity;

namespace Application.Infrastructure.Specifications
{
    public class JournoWithItemsSpecification : BaseSpecification<ApplicationUser>
    {
        public JournoWithItemsSpecification(Role role)
            : base(b => b.Role == role)
        {
            AddInclude(b => b.JournoRankings);
        }
    }
}
