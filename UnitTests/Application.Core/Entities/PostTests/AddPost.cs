using Application.Core.Entities;
using Application.Core.Enums;
using Xunit;

namespace UnitTests.Application.Core.Entities.PostTests
{
    public class AddPost
    {
        private string _title = "Article title goes here.";
        private string _description = "Description goes here";
        private string _content =  "A and B.";
        private int _views = 6747;
        private int _articles = 84;
        private PostType _type = PostType.Article;

        [Fact]
        public void AddsPostIfNotPresent()
        {
            var post = new Post(_title, _description, _content, _views, _articles, _type);
            Assert.Equal(_title, post.Title);
            Assert.Equal(_description, post.Description);
            Assert.Equal(_content, post.Content);
            Assert.Equal(_views, post.Views);
            Assert.Equal(_articles, post.Articles);
            Assert.Equal(_type, post.Type);
        }
    }
}
