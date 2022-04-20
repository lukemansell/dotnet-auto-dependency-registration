using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Dotnet.AutoDependencyRegistration.Extensions.Helpers;
using Dotnet.AutoDependencyRegistration.Extensions.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Dotnet.AutoDependencyRegistration.Extensions.Services;

public static class RegisterDependenciesService
{
    public static string Register(IServiceCollection serviceCollection)
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        var discoveredServices = RegisterDependenciesHelper.FindRegisteredClassesByAttribute(assemblies);
        
        return RegisterServices(discoveredServices, serviceCollection);
    }
    
    private static string RegisterServices(IEnumerable<ServicesToRegister> servicesEnumerable, IServiceCollection serviceCollection)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        var classesRegistered = new StringBuilder();

        foreach (var service in servicesEnumerable)
        {
            if (service.ClassName != null && service.InterfaceName != null)
            {
                // If for any reason a service can't be registered an error will automatically be thrown
                serviceCollection.Add(
                    new ServiceDescriptor(
                        service.InterfaceName, 
                        service.ClassName, 
                        service.ServiceLifetime)
                );
            }
            
            var message = $"{service.ClassName}, {service.InterfaceName} has been registered as {service.ServiceLifetime}. ";
            Log.Logger.Information("{Message}",message);
            
            classesRegistered.AppendLine(message);
        }

        return classesRegistered.ToString();
    }
}