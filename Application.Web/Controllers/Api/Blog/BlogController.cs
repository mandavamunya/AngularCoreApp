using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Blog")]
    public class BlogController : Controller
    {
        private IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IEnumerable<Blog>> Index()
        {
            return await _blogService.GetAllBlogs();
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetBlogById([FromRoute] int id)
        {
            var blog = await _blogService.GetAllBlogItems(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }
    }
}