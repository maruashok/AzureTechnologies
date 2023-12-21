using Microsoft.EntityFrameworkCore;
using AzureTechnologies.Application.Services;
using AzureTechnologies.Domain.Interfaces.Repositiry;
using AzureTechnologies.Domain.Interfaces.Service;
using AzureTechnologies.Infrastructure.Data;
using AzureTechnologies.Infrastructure.Repository;
using System.Web;

namespace AzureTechnologies.Extensions
{
    public static class ApplicationServices
    {
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            var connection = string.Empty;
            if (builder.Environment.IsDevelopment())
            {
                connection = builder.Configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                connection = HttpUtility.HtmlDecode(Environment.GetEnvironmentVariable("SQLCONNSTR_AZURE_SQL_CONNECTIONSTRING"));
            }

            builder.Services.AddDbContext<AzureTechContext>(options => options.UseSqlServer(connection));
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}