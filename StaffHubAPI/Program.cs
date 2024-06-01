using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StaffHubAPI.DataAccess.Repositories.Interface;
using StaffHubAPI.DataAccess.Repositories;
using StaffHubAPI.Services.Implementations;
using StaffHubAPI.Services.Interfaces;
using StaffHubAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using StaffHubAPI.DataAccess.UnitOfWork;
using StaffHubAPI.Helper.Middleware;

namespace StaffHubAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Services
            ConfigureServices(builder);

            var app = builder.Build();

            // Configure Middleware
            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Database Context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Authentication
            ConfigureAuthentication(builder);

            // Dependency Injection
            RegisterServices(builder);

            // Swagger
            ConfigureSwagger(builder);

            // AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Controllers
            builder.Services.AddControllers();
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            var tokenKey = builder.Configuration.GetSection("Appsettings:Token").Value;
            var key = Encoding.UTF8.GetBytes(tokenKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                    };
                });

            builder.Services.AddHttpContextAccessor();
        }

        private static void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IAttachedFileRepository, AttachedFileRepository>();
            builder.Services.AddScoped<IActualSalaryRepository, ActualSalaryRepository>();
            builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IRoleClaimService, RoleClaimService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISubmissionService, SubmissionService>();
            builder.Services.AddScoped<IAttachedFileService, AttachedFileService>();
            builder.Services.AddScoped<IActualSalaryService, ActualSalaryService>();
            builder.Services.AddScoped<IClaimService, ClaimService>();
            builder.Services.AddScoped<IRoleClaimService, RoleClaimService>();
            //builder.Services.AddScoped<IClaimService, ClaimService>();
        }

        private static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            // Custom Middleware
            // app.UseMiddleware<RefreshTokenMiddleware>();
            app.UseMiddleware<ClaimCheckMiddleware>();

            app.MapControllers();
        }
    }
}
