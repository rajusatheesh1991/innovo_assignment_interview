using InnovoAssignment.Web.Contracts;
using InnovoAssignment.Web.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace InnovoAssignment.Web
{
    public static class WebRegistration
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           //services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IUserService, UserService>();
            services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44344/")
            });

            services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:44344/"));



            return services;
        }
    }
}
