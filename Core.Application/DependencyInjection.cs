using Core.Application.Interfaces;
using Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddCoreServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICandidatoService, CandidatoService>();
            serviceCollection.AddScoped<IEmpleoService, EmpleoService>();
        }
    }
}