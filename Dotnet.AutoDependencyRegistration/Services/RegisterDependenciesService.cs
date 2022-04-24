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
    /// <summary>
    /// Service method which calls helper methods to get all assemblies, filter classes within
    /// these which have a RegisterClass base attribute and then registers the classes.
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static string Register(IServiceCollection serviceCollection)
    {
        // This isn't picking up all assemblies
        // var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        /* A limitation of this approach is that you may end up registering services which
        are from other packages using this package. To look into */
        var assemblies = RegisterDependenciesHelper.GetAssemblies();
        
        var filterClasses = RegisterDependenciesHelper.FindRegisteredClassesByAttribute(assemblies);
        
        return RegisterServices(filterClasses, serviceCollection);
    }
    
    /// <summary>
    /// Logs and also adds discovered classes to the service collection. If classes are found
    /// with a name but no interface they are logged out for the user to understand why they
    /// were not added to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceCollection"></param>
    /// <returns>A string of classes which have been registered</returns>
    private static string RegisterServices(
        IEnumerable<ClassesToRegister> services,
        IServiceCollection serviceCollection)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

        var classesRegistered = new StringBuilder();

        foreach (var service in services)
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