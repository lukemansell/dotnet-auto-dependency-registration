using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoDependencyRegistration.Attributes;
using AutoDependencyRegistration.Interfaces;
using AutoDependencyRegistration.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AutoDependencyRegistration.Helpers
{
    public static class RegisterDependenciesHelper
    {
        public static IEnumerable<ClassesToRegister> FindRegisteredClassesByAttribute(IEnumerable<Assembly> assembly)
        {
            var classes = assembly
                .SelectMany(x => x.GetExportedTypes())
                .Where(FilterClassesWithRegisterClassAttributeOrInterface);

            return MapAssembliesToModel(classes);
        }

        public static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();

            foreach (var path in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
            {
                try
                {
                    assemblies.Add(Assembly.Load(AssemblyName.GetAssemblyName(path)));
                }
                catch (BadImageFormatException e)
                {
                    Log.Logger.Error("{Assembly} could not be loaded", e.Source);
                }
            }

            return assemblies;
        }
        
        private static bool FilterClassesWithRegisterClassAttributeOrInterface(Type type)
        {
            if (type.IsAbstract || type.IsGenericType || type.IsNested)
            {
                return false;
            }

            // Check for attribute-based registration
            if (type.GetCustomAttributes(typeof(RegisterClass), true).Any())
            {
                return true;
            }

            // Check for interface-based registration
            var interfaces = type.GetInterfaces();
            return interfaces.Any(i => 
                i == typeof(ITransientDependency) ||
                i == typeof(IScopedDependency) ||
                i == typeof(ISingletonDependency) ||
                i == typeof(ITransientDependencyIgnoreInterface) ||
                i == typeof(IScopedDependencyIgnoreInterface) ||
                i == typeof(ISingletonDependencyIgnoreInterface));
        }

        private static IEnumerable<ClassesToRegister> MapAssembliesToModel(IEnumerable<Type> classes)
        {
            return classes.Select(x =>
            {
                var attribute = x.CustomAttributes
                    .FirstOrDefault(a => a.AttributeType.FullName.Contains("AutoDependencyRegistration"));

                var serviceLifetime = GetServiceLifetime(x, attribute?.AttributeType?.FullName ?? "");
                var ignoreInterface = GetIgnoreInterfaceFlag(x, attribute?.AttributeType?.FullName ?? "");
                var serviceKey = GetServiceKey(x, attribute);

                // Filter out marker interfaces from the list of interfaces to register
                var allInterfaces = x.GetTypeInfo().ImplementedInterfaces.ToList();
                var markerInterfaces = new[]
                {
                    typeof(ITransientDependency),
                    typeof(IScopedDependency),
                    typeof(ISingletonDependency),
                    typeof(ITransientDependencyIgnoreInterface),
                    typeof(IScopedDependencyIgnoreInterface),
                    typeof(ISingletonDependencyIgnoreInterface)
                };
                var interfacesToRegister = allInterfaces
                    .Where(i => !markerInterfaces.Contains(i))
                    .ToList();

                return new ClassesToRegister
                {
                    ClassName = x.GetTypeInfo(),
                    InterfaceName = interfacesToRegister,
                    ServiceLifetime = serviceLifetime,
                    IgnoreInterface = ignoreInterface,
                    ServiceKey = serviceKey
                };
            });
        }

        private static string? GetServiceKey(Type type, CustomAttributeData? attribute)
        {
            if (attribute == null)
            {
                return null;
            }

            // Check if this is a keyed service attribute
            var attributeTypeName = attribute.AttributeType.FullName ?? "";
            if (!attributeTypeName.Contains("Keyed"))
            {
                return null;
            }

            // Extract the key from constructor arguments
            // Keyed attributes have the key as the first constructor parameter
            if (attribute.ConstructorArguments.Count > 0)
            {
                var firstArg = attribute.ConstructorArguments[0];
                if (firstArg.ArgumentType == typeof(string))
                {
                    return firstArg.Value?.ToString();
                }
            }

            return null;
        }

        private static ServiceLifetime GetServiceLifetime(Type type, string customAttribute)
        {
            // First check for interface-based registration
            var interfaces = type.GetInterfaces();
            
            if (interfaces.Contains(typeof(IScopedDependency)) || 
                interfaces.Contains(typeof(IScopedDependencyIgnoreInterface)))
            {
                return ServiceLifetime.Scoped;
            }

            if (interfaces.Contains(typeof(ISingletonDependency)) || 
                interfaces.Contains(typeof(ISingletonDependencyIgnoreInterface)))
            {
                return ServiceLifetime.Singleton;
            }

            if (interfaces.Contains(typeof(ITransientDependency)) || 
                interfaces.Contains(typeof(ITransientDependencyIgnoreInterface)))
            {
                return ServiceLifetime.Transient;
            }

            // Fall back to attribute-based registration
            if (customAttribute.Contains("RegisterClassAsScoped"))
            {
                return ServiceLifetime.Scoped;
            }

            if (customAttribute.Contains("RegisterClassAsSingleton"))
            {
                return ServiceLifetime.Singleton;
            }

            return ServiceLifetime.Transient;
        }

        private static bool GetIgnoreInterfaceFlag(Type type, string customAttribute)
        {
            // First check for interface-based registration
            var interfaces = type.GetInterfaces();
            
            if (interfaces.Contains(typeof(ITransientDependencyIgnoreInterface)) ||
                interfaces.Contains(typeof(IScopedDependencyIgnoreInterface)) ||
                interfaces.Contains(typeof(ISingletonDependencyIgnoreInterface)))
            {
                return true;
            }

            // Fall back to attribute-based registration
            return customAttribute.Contains("IgnoreInterface");
        }
    }
}