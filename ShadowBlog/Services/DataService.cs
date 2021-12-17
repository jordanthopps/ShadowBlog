using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShadowBlog.Data;
using ShadowBlog.Models;
using ShadowBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IImageService _imageService;
        private readonly UserManager<BlogUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISlugService _slugService;

        public DataService(ApplicationDbContext dbContext,
            UserManager<BlogUser> userManager,
            IImageService imageService,
            RoleManager<IdentityRole> roleManager, 
            ISlugService slugService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _imageService = imageService;
            _roleManager = roleManager;
            _slugService = slugService;
        }


        public async Task ManageDataAsync()
        {
           await _dbContext.Database.MigrateAsync();

            //Step 1: Create 2 Roles
            await SeedRolesAsync();

            //Step 2: Create 2 Users and assign the roles.
            await SeedUsersAsync();

            //Step 3: Seed Blogs -- For paging purposes ...
            await SeedBlogsAsync();

            //Step 4: Seed Posts -- For purposes of working with paging and forwarding the search term
            await SeedBlogPostsAsync();
           
        }

        private async Task SeedRolesAsync()
        {
            //Ask if there are any Roles in the AspNetRoles table
            if (_dbContext.Roles.Any()) return;

            IdentityRole adminRole = new("Administrator");
            await _roleManager.CreateAsync(adminRole);

            IdentityRole moderatorRole = new("Moderator");
            await _roleManager.CreateAsync(moderatorRole);
        }

        private async Task SeedUsersAsync()
        {
            //Ask if there are any Users at all already in the AspNetUsers table
            if (_dbContext.Users.Any()) return;

            BlogUser admin = new()
            {
                Email = "JordanTaylor@Mailinator.com",
                UserName = "JordanTaylor@Mailinator.com",
                FirstName = "Jordan",
                LastName = "Taylor",
                PhoneNumber = "555-5555",
                EmailConfirmed = true,
                ImageData = await _imageService.EncodeImageAsync("generic-user-purpbg.png"),
                ImageType = "png"
            };

            await _userManager.CreateAsync(admin, "Abc@123!");
            await _userManager.AddToRoleAsync(admin, "Administrator");

            //TODO: Seed a User who will occupy the Moderator role
            BlogUser mod = new()
            {
                Email = "CoderFoundryDevs@Mailinator.com",
                UserName = "CoderFoundryDevs@Mailinator.com",
                FirstName = "Coder",
                LastName = "Foundry",
                PhoneNumber = "555-5555",
                EmailConfirmed = true,
                ImageData = await _imageService.EncodeImageAsync("generic-user-purpbg.png"),
                ImageType = "png"
            };

            await _userManager.CreateAsync(mod, "Abc@123!");
            await _userManager.AddToRoleAsync(mod, "Moderator");
            await _userManager.AddToRoleAsync(admin, "Moderator");
        }


        private async Task SeedBlogsAsync()
        {
            if (_dbContext.Blogs.Any())
                return;

            for (var loop = 1; loop <= 1; loop++)
            {
                _dbContext.Add(new Blog()
                {
                    Name = $"Blog for Application {loop}",
                    Description = $"Everything I learned while assembling the Portfolio Application {loop}",
                    Created = DateTime.Now.AddDays(loop),
                    ImageData = await _imageService.EncodeImageAsync("BlogDefaultImage.jpg"),
                    ContentType = "jpg"
                });
            }
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedBlogPostsAsync()
        {
            if (_dbContext.BlogPosts.Any())
                return;

            var blogId = (await _dbContext.Blogs.AsNoTracking().OrderBy(b => b.Created).FirstOrDefaultAsync()).Id;
            for (var loop = 1; loop <= 1; loop++)
            {
                var title = $"Post number {loop}";
                _dbContext.Add(new BlogPost()
                {
                    BlogId = blogId,
                    Title = title,
                    ReadyStatus = Enums.ReadyState.ProductionReady,
                    Slug = _slugService.UrlFriendly(title),
                    Abstract = $"Abstract for Posts number {loop}",
                    Content = $"Content of Post number {loop}",
                    Created = DateTime.Now.AddDays(loop),
                    ImageData = await _imageService.EncodeImageAsync("BlogPostDefaultImage.jpg"),
                    ImageType = "jpg"
                });
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
