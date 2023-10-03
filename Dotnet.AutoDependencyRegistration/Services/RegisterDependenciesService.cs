using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoDependencyRegistration.Helpers;
using AutoDependencyRegistration.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AutoDependencyRegistration.Services
{
    public static class RegisterDependenciesService
    {
        public static string Register(IServiceCollection serviceCollection)
        {
            var assemblies = RegisterDependenciesHelper.GetAssemblies();
            var filterClasses = RegisterDependenciesHelper.FindRegisteredClassesByAttribute(assemblies);
            return RegisterServices(filterClasses, serviceCollection);
        }

        private static string RegisterServices(
            IEnumerable<ClassesToRegister> services,
            IServiceCollection serviceCollection)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

            var classesRegistered = new StringBuilder();

            var classesToRegisters = services.ToList();
            foreach (var service in classesToRegisters)
            {
                if (service.ClassName != null && service.InterfaceName.Any() && !service.IgnoreInterface)
                {
                    AddServiceWithInterface(service, serviceCollection, classesRegistered);
                }
                else if ((service.ClassName != null && !service.InterfaceName.Any()) 
                         || service is { ClassName: { }, IgnoreInterface: true }) 
                {
                    AddServiceWithoutInterface(service, serviceCollection, classesRegistered);
                }
            }
            
            Log.Logger.Information("{OverallCount} services were registered. " +
                                   "{SingletonCount} singleton, {ScopedCount} scoped, {TransientCount} transient.",
                classesToRegisters.Count(), 
                classesToRegisters.Count(x => x.ServiceLifetime == ServiceLifetime.Singleton),
                classesToRegisters.Count(x => x.ServiceLifetime == ServiceLifetime.Scoped),
                classesToRegisters.Count(x => x.ServiceLifetime == ServiceLifetime.Transient)
            );
            
            return classesRegistered.ToString();
        }

        private static void AddServiceWithInterface(
            ClassesToRegister service,
            IServiceCollection serviceCollection,
            StringBuilder classesRegistered)
        {
            foreach (var implementation in service.InterfaceName)
            {
                if (service.ClassName.IsGenericType)
                {
                    serviceCollection.Add(
                        new ServiceDescriptor(
                            implementation.GetGenericTypeDefinition(),
                            service.ClassName.GetGenericTypeDefinition(),
                            service.ServiceLifetime));
                }
                else
                {
                    serviceCollection.Add(
                        new ServiceDescriptor(
                            implementation,
                            service.ClassName,
                            service.ServiceLifetime));
                }

                var message = $"{service.ClassName?.Name}, {implementation} has been registered as {service.ServiceLifetime}. ";
                Log.Logger.Information("{Message}", message);
                classesRegistered.AppendLine(message);
            }
        }

        private static void AddServiceWithoutInterface(
            ClassesToRegister service,
            IServiceCollection serviceCollection,
            StringBuilder classesRegistered)
        {
            if (service.ClassName.IsGenericType)
            {
                serviceCollection.Add(
                    new ServiceDescriptor(
                        service.ClassName.GetGenericTypeDefinition(),
                        service.ClassName.GetGenericTypeDefinition(),
                        service.ServiceLifetime));
            }
            else
            {
                serviceCollection.Add(
                    new ServiceDescriptor(
                        service.ClassName,
                        service.ClassName,
                        service.ServiceLifetime));
            }

            var message = $"{service.ClassName?.Name} has been registered as {service.ServiceLifetime}. ";
            Log.Logger.Information("{Message}", message);
            classesRegistered.AppendLine(message);
        }
    }
}