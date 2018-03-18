using Application.Core.Entities;
using Application.Core.Enums;
using Application.Core.Specifications;

namespace Application.Core.Specifications
{
    public class PostItemByTypeSpecification : BaseSpecification<Post>
    {
        public PostItemByTypeSpecification(PostType postType)
            : base(p => p.Type == postType)
        {

        }
    }
}
