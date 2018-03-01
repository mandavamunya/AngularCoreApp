using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers.Api.Blog
{
    [Produces("application/json")]
    [Route("api/Blog")]
    public class BlogController : Controller
    {
        public BlogController()
        {
        }

        public async Task<IActionResult> GetPostsByType(int type)
        {
            throw new NotImplementedException();
        }
    }
}