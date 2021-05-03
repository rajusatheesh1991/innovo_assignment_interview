
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InnovoAssignment.Application.Contracts.Infrastructure;
using InnovoAssignment.Application.Models;
using InnovoAssignment.Infrastructure.Mail;

namespace InnovoAssignment.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
