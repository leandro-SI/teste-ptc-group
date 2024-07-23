using BlogPTC.Application.Interfaces;
using BlogPTC.Application.Mappings;
using BlogPTC.Application.Services;
using BlogPTC.Domain.Account;
using BlogPTC.Domain.Interfaces;
using BlogPTC.Infra.Data.Context;
using BlogPTC.Infra.Data.Identity;
using BlogPTC.Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Infra.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfraEstructureAPI(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
                ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.SignIn.RequireConfirmedAccount = false;
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticate, AuthenticateService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}
