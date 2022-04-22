using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Dotnet.AutoDependencyRegistration.Helpers;
using Dotnet.AutoDependencyRegistration.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Dotnet.AutoDependencyRegistration.Services;

public static class RegisterDependenciesService
{
    public static string Register(IServiceCollection serviceCollection)
    {
        // This isn't picking up all assemblies
        // var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        /* A limitation of this approach is that you may end up registering services which
        are from other packages using this package. To look into */
        var assemblies = RegisterDependenciesHelper.GetAssemblies();
        
        var discoveredClasses = RegisterDependenciesHelper.FindRegisteredClassesByAttribute(assemblies);
        
        return RegisterServices(discoveredClasses, serviceCollection);
    }
    
    private static string RegisterServices(
        IEnumerable<ClassesToRegister> servicesEnumerable,
        IServiceCollection serviceCollection)
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

                var message = $"{service.ClassName?.Name}, {service.InterfaceName?.Name} has been registered as {service.ServiceLifetime}. ";
                Log.Logger.Information("{Message}",message);
            
                classesRegistered.AppendLine(message);
            }
            else
            {
                if (service.ClassName != null && service.InterfaceName == null)
                {
                    Log.Logger.Error("{ClassName} doesn't have an interface. You can only register classes with an interface", service.ClassName.Name);
                }
            }
        }

        return classesRegistered.ToString();
    }
}