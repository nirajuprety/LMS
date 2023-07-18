using Library.Domain.Interface;
using Library.Infrastructure.Repository;
using Library.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(o => o.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<IServiceFactory, ServiceFactory>();

            services.AddScoped<IStudentService, StudentService>();
        
            return services;
        }
    }
}
