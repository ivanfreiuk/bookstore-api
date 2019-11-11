using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BusinessLogic.Interfaces;
using BookStore.BusinessLogic.Models;
using BookStore.BusinessLogic.Profiles;
using BookStore.BusinessLogic.Services;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Identity;
using BookStore.DataAccess.UnitOfWork;
using BookStore.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Add CORS

            services.AddCors();

            #endregion

            #region Add Entity Framework and Identity Framework

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("BookStore"));
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<StoreDbContext>().AddDefaultTokenProviders();

            #endregion

            #region Add Authentication

            var appSettings = Configuration.GetSection("TokenSettings");
            services.Configure<TokenSettings>(appSettings);


            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenSettings:Key"]));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = true,
                    ValidAudience = Configuration["TokenSettings:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["TokenSettings:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            #endregion


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Add DI for application services

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IWishService, WishService>();
            services.AddTransient<ICartItemService, CartItemService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IMapperFactory, MapperFactory>();
            
            #endregion

            #region AutoMapper

            var serviceProvider = services.BuildServiceProvider();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AuthorProfile>();
                cfg.AddProfile<CategoryProfile>();
                cfg.AddProfile<WishProfile>();
                cfg.AddProfile<OrderProfile>();
                cfg.AddProfile<CartItemProfile>();
                cfg.AddProfile(new BookProfile(serviceProvider.GetService<IUnitOfWork>()));
                cfg.AddProfile(new CommentProfile(serviceProvider.GetService<IUnitOfWork>()));
            });

          //  services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped);
         

            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            
           // CreateRoles(serviceProvider).Wait();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roleNames = { "Admin", "User", "Manager" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await roleManager.CreateAsync(new Role
                    {
                        Name = roleName
                    });
                }
            }

            User user = await userManager.FindByEmailAsync("admin@gmail.com");

            if (user == null)
            {
                user = new User()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                };
                await userManager.CreateAsync(user, "Qwerty11");
            }
            await userManager.AddToRoleAsync(user, "Admin");


            var user1 = await userManager.FindByEmailAsync("manager@gmail.com");

            if (user1 == null)
            {
                user1 = new User()
                {
                    UserName = "manager@gmail.com",
                    Email = "manager@gmail.com",
                };
                await userManager.CreateAsync(user1, "Qwerty11");
            }
            await userManager.AddToRoleAsync(user1, "Manager");

            var user2 = await userManager.FindByEmailAsync("user@gmail.com");

            if (user2 == null)
            {
                user2 = new User()
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                };
                await userManager.CreateAsync(user2, "Qwerty11");
            }
            await userManager.AddToRoleAsync(user2, "User");

        }
    }
}
