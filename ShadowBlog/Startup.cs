using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShadowBlog.Data;
using ShadowBlog.Models;
using ShadowBlog.Services;
using ShadowBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ShadowBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    ConnectionService.GetConnectionString(Configuration)));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<BlogUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();


            //Registration of DataService.cs
            services.AddTransient<DataService>();
            //Registration of BasicImageService.cs as the concrete class to be used by IImageService inteface.
            services.AddScoped<IImageService, BasicImageService>(); //AddScoped means scoped to user.
            services.AddTransient<ISlugService, BasicSlugService>();
            services.AddTransient<IEmailSender, BasicEmailService>();
            services.AddScoped<SearchService>();


            //Add swagger as a service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Blog API",
                    Description = "This is an open API with no security",
                    Contact = new OpenApiContact
                    {
                        Name = "Jordan Hopps",
                        Email = "dev.jordanhopps@gmail.com",
                        Url = new Uri("https://heroku-shadow-blog.herokuapp.com")
                    }
                });

                //Construct the XML comment path
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //Configure a CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //Swagger config
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShadowBlogAPI");
                //c.RoutePrefix = "";
            });

            //Use the defined policy from ConfigureServices
            app.UseCors("DefaultCorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Custom route goes first...
                //Route definitions need to go from most specific to most generalized.
                endpoints.MapControllerRoute(
                    name: "slugRoute",
                    pattern: "JordansBlog/PostDetails/{slug}",
                    defaults: new { controller = "BlogPosts", action = "Details"});

                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=BlogPosts}/{action=ChildIndex}/{blogid=1}");
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
