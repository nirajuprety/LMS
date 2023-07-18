using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Domain.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddInApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStudentManager, StudentManager>();
            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<IStaffManager, StaffManager>();

            return services;
        }
    }
}
