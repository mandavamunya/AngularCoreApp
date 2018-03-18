using Application.Core.Enums;
using Application.Infrastructure.Identity;

namespace Application.Core.Entities
{
    public class Post: TrackEntity
    {
        public Post()
        {
        }

        public Post(
            string title, 
            string description, 
            string content, 
            int views, 
            int publications,
            PostType type)
        {
            Title = title;
            Description = description;
            Content = content;
            Views = views;
            Publications = publications;
            Type = type;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int Views { get; set; }
        public int Comments { get; set; }
        public int Publications { get; set; }
        public PostType Type { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
