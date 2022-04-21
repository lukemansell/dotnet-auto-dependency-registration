using System;
using System.Collections.Generic;
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
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        var discoveredClasses = RegisterDependenciesHelper.FindRegisteredClassesByAttribute(assemblies);
        
        return RegisterServices(discoveredClasses, serviceCollection);
    }

    private static string RegisterServices(IEnumerable<ClassesToRegister> servicesEnumerable, IServiceCollection serviceCollection)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        var classesRegistered = new StringBuilder();

        foreach (var service in servicesEnumerable)
        {
            if (service.ClassName != null && service.InterfaceName != null)
            {
                serviceCollection.Add(
                    new ServiceDescriptor(
                        service.InterfaceName, 
                        service.ClassName, 
                        service.ServiceLifetime)
                );
            }
            
            var message = $"{service.ClassName?.Name}, {service.InterfaceName?.Name} has been registered as {service.ServiceLifetime}. ";
            Log.Logger.Information("{Message}",message);
            
            classesRegistered.AppendLine(message);
        }

        return classesRegistered.ToString();
    }
}