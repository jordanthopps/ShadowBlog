using Microsoft.EntityFrameworkCore;
using ShadowBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        public DataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task ManageDataAsync()
        {
           await _dbContext.Database.MigrateAsync();
        }
    }
}
