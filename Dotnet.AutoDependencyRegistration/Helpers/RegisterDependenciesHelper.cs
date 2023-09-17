using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoDependencyRegistration.Attributes;
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
                .Where(FilterClassesWithRegisterClassAttribute);

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
        private static bool FilterClassesWithRegisterClassAttribute(Type type)
        {
            return type.GetCustomAttributes(typeof(RegisterClass), true).Any() &&
                   !type.IsAbstract && !type.IsGenericType && !type.IsNested;
        }

        private static IEnumerable<ClassesToRegister> MapAssembliesToModel(IEnumerable<Type> classes)
        {
            return classes.Select(x =>
            {
                var attribute = x.CustomAttributes
                    .FirstOrDefault(a => a.AttributeType.FullName.Contains("AutoDependencyRegistration"));

                var serviceLifetime = GetServiceLifetime(attribute?.AttributeType?.FullName ?? "");
                var ignoreInterface = GetIgnoreInterfaceFlag(attribute?.AttributeType?.FullName ?? "");

                return new ClassesToRegister
                {
                    ClassName = x.GetTypeInfo(),
                    InterfaceName = x.GetTypeInfo().ImplementedInterfaces.ToList(),
                    ServiceLifetime = serviceLifetime,
                    IgnoreInterface = ignoreInterface
                };
            });
        }

        private static ServiceLifetime GetServiceLifetime(string customAttribute)
        {
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

        private static bool GetIgnoreInterfaceFlag(string customAttribute)
        {
            return customAttribute.Contains("IgnoreInterface");
        }
    }
}