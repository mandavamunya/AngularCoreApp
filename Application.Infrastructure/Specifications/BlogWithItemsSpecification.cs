using Application.Core.Entities;

namespace Application.Core.Specifications
{
    public class BlogWithItemsSpecification: BaseSpecification<Blog>
    {
        public BlogWithItemsSpecification(int blogId)
            : base(b => b.Id == blogId)
        {
            AddInclude(b => b.Posts);
        }
    }
}
