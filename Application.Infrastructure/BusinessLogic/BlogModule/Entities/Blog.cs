using System.Collections.Generic;

namespace Application.Core.Entities
{
    public class Blog: TrackEntity
    {
        public Blog(string name, bool isPublished)
        {
            Name = name;
            IsPublished = isPublished;
        }

        public string Name { get; set; }
        public bool IsPublished { get; set; }
       
        public ICollection<Post> Posts { get; set; }
    }
}
