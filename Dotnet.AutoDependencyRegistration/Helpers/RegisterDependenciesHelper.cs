using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoDependencyRegistration.Attributes;
using AutoDependencyRegistration.Models;
using Microsoft.Extensions.DependencyInjection;

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
            return Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
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
                    .FirstOrDefault(a => a.AttributeType.BaseType == typeof(RegisterClass));
                
                var serviceLifetime = GetServiceLifetime(attribute.AttributeType);
                var ignoreInterface = CheckForIgnoreInterface(attribute);

                return new ClassesToRegister
                {
                    ClassName = x.GetTypeInfo(),
                    InterfaceName = x.GetTypeInfo().ImplementedInterfaces.ToList(),
                    ServiceLifetime = serviceLifetime,
                    IgnoreInterface = ignoreInterface
                };
            });
        }

        private static ServiceLifetime GetServiceLifetime(Type customAttribute)
        {
            var declaredAttribute = customAttribute;
            if (declaredAttribute == typeof(RegisterClassAsScoped))
            {
                return ServiceLifetime.Scoped;
            }

            if (declaredAttribute == typeof(RegisterClassAsSingleton))
            {
                return ServiceLifetime.Singleton;
            }

            return ServiceLifetime.Transient;
        }

        private static bool CheckForIgnoreInterface(CustomAttributeData? attribute)
        {
            return attribute.NamedArguments.Select(x => x.MemberName == "ExcludeInterface").FirstOrDefault();
        }
    }
}