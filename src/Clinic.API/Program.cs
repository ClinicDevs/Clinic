
using Clinic.Application;
using Clinic.Domain.Entities.Auth;
using Clinic.Infrastructure;
using Clinic.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using System.Text.Json.Serialization;

namespace Clinic.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddClinicInfrastructureDdependencyInjection(builder.Configuration);
            builder.Services.AddClinicApplicationDependencyInjection();


            builder.Services.AddRateLimiter(x =>
            {
                x.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                x.AddTokenBucketLimiter("bucket", options =>
                {
                    options.ReplenishmentPeriod = TimeSpan.FromSeconds(60);
                    options.TokenLimit = 60;
                    options.TokensPerPeriod = 20;
                    options.AutoReplenishment = true;
                });
            });

            builder.Services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ClinincDbContext>()
               .AddDefaultTokenProviders();


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers()
   .AddJsonOptions(options =>
   {
       options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
   });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRateLimiter();

            app.UseCors();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "User", "Doctor" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                string email = "admin@admin.com";
                string password = "Admin001!";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new User()
                    {
                        Firsname = email,
                        Lastname = email,
                        UserName = email,
                        Email = email,
                        Role = "Admin",
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            app.Run();
        }
    }
}
