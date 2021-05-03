
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InnovoAssignment.Application.Contracts.Persistence;
using InnovoAssignment.Persistence.Repositories;

namespace InnovoAssignment.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InnovoAssignmentContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("InnovoAssignmentConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAddressRepository, UserAddressRepository>();
            services.AddScoped<IUserPreferencesRepository, UserPreferencesRepository>();

            return services;    
        }
    }
}
