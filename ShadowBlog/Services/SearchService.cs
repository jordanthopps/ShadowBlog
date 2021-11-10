using Microsoft.EntityFrameworkCore;
using ShadowBlog.Data;
using ShadowBlog.Enums;
using ShadowBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Services
{
    public class SearchService
    {
        private readonly ApplicationDbContext _dbContext;

        public SearchService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BlogPost>> SearchAsync(string searchTerm)
        {
            List<BlogPost> posts = new();

            //If the user tries searching on an emtpy term
            if (!string.IsNullOrEmpty(searchTerm))
            {

                searchTerm = searchTerm.ToLower();

                //I am using an intermediate variable to hold all possible BlogPosts
                posts = await _dbContext.BlogPosts
                    .Include(b => b.Blog)
                    .Include(t => t.Tags)
                    .Include(b => b.Comments)
                    .ThenInclude(c => c.BlogUser)
                    .Where(b => b.ReadyStatus == ReadyState.ProductionReady)
                    .ToListAsync();

                posts = posts.Where(p => p.Title.ToLower().Contains(searchTerm) ||
                            p.Abstract.ToLower().Contains(searchTerm) ||
                            p.Tags.Any(t => t.Text.ToLower().Contains(searchTerm)) ||
                            p.Content.ToLower().Contains(searchTerm) ||
                            p.Comments.Any(c => c.CommentBody.ToLower().Contains(searchTerm) ||
                                                c.BlogUser.FullName.ToLower().Contains(searchTerm))).ToList();

            }

            return posts;

        }

    }
}