using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShadowBlog.Data;
using ShadowBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogSvcController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogSvcController(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Returns the most recent X number of Blog Posts
        /// </summary>
        /// <remarks> Jordan, 12/13/2021 </remarks>
        /// <param name="num">The number of Blog Posts you want</param>
        /// <returns>List of type BlogPosts</returns>
        [HttpGet("/GetTopXPosts/{num}")]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetTopXPosts(int num)
        {
            //Return the most recent num blog posts
            return await _context.BlogPosts.OrderByDescending(p => p.Created).Take(num).ToListAsync();
        }
    }
}
