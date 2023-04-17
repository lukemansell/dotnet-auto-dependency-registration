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
                .Where(HasRegisterClassAttribute);

            return MapAssembliesToModel(classes);
        }

        public static IEnumerable<Assembly> GetAssemblies()
        {
            return Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
        }

        private static bool HasRegisterClassAttribute(Type type)
        {
            return type.GetCustomAttribute<RegisterClass>(true) != null &&
                   type is { IsAbstract: false, IsGenericType: false, IsNested: false };
        }

        private static IEnumerable<ClassesToRegister> MapAssembliesToModel(IEnumerable<Type> classes)
        {
            return classes.Select(x =>
            {
                var attribute = x.CustomAttributes
                    .FirstOrDefault(a => a.AttributeType.BaseType == typeof(RegisterClass));
                
                return new ClassesToRegister
                {
                    ClassName = x.GetTypeInfo(),
                    InterfaceName = x.GetTypeInfo().ImplementedInterfaces.ToList(),
                    ServiceLifetime = GetServiceLifetime(attribute.AttributeType),
                    IgnoreInterface = CheckForIgnoreInterface(attribute)
                };
            });
        }

        private static ServiceLifetime GetServiceLifetime(Type customAttribute)
        {
            return customAttribute switch
            {
                _ when customAttribute == typeof(RegisterClassAsScoped) => ServiceLifetime.Scoped,
                _ when customAttribute == typeof(RegisterClassAsSingleton) => ServiceLifetime.Singleton,
                _ => ServiceLifetime.Transient
            };
        }

        private static bool CheckForIgnoreInterface(CustomAttributeData? attribute)
        {
            return attribute.NamedArguments.FirstOrDefault(x => x.MemberName == "ExcludeInterface").TypedValue.Value as bool? ?? false;
        }
    }
}