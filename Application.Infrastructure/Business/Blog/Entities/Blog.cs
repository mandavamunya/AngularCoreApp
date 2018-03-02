using System.Collections.Generic;

namespace Application.Core.Entities
{
    public class Blog: TrackEntity
    {
        public string Title { get; set; }
        public bool IsPublished { get; set; }
       
        public ICollection<Post> Posts { get; set; }
    }
}
