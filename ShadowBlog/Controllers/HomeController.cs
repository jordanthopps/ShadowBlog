using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShadowBlog.Data;
using ShadowBlog.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailSender _emailSerivce;
        private IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IEmailSender emailSerivce, IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _emailSerivce = emailSerivce;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            //Show all Blogs...
            return View( await _dbContext.Blogs.ToListAsync());
        }

        public IActionResult ContactMe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //string generated on server and injected into the form. When the form is submitted, it compares it to other tokens generated recently and if they match, then it's validated.
        public async Task<IActionResult> ContactMe(string name,string email, string phone, string message)
        {
            var subject = $"{name} has reached out to you from the ShadowBlog Application";
            

            var body = $"{message}.<br/><br/>{name} can be called at {phone} or emailed at {email} if follow up is required.";

            var myEmail = _configuration["SmtpSettings:Email"];
            await _emailSerivce.SendEmailAsync(myEmail, subject, body);
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
