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

        public DataService(ApplicationDbContext dbContext,
            UserManager<BlogUser> userManager,
            IImageService imageService, 
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _imageService = imageService;
            _roleManager = roleManager;
        }


        public async Task ManageDataAsync()
        {
           await _dbContext.Database.MigrateAsync();

            //Step 1: Create 2 Roles
            await SeedRolesAsync();

            //Step 2: Create 2 Users and assign the roles.
            await SeedUsersAsync();
           
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
                Email = "JT@Mailinator.com",
                UserName = "JT@Mailinator.com",
                FirstName = "Jordan",
                LastName = "Taylor",
                PhoneNumber = "555-5555",
                EmailConfirmed = true,
                ImageData = await _imageService.EncodeImageAsync("generic-user-purpbg.png"),
                ImageType = "png"
            };

            await _userManager.CreateAsync(mod, "Abc@123!");
            await _userManager.AddToRoleAsync(mod, "Moderator");
        }
    }
}
