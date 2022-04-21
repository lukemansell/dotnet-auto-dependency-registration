using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dotnet.AutoDependencyRegistration.Extensions.Attributes;
using Dotnet.AutoDependencyRegistration.Extensions.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Extensions.Helpers;

public static class RegisterDependenciesHelper
{
    /// <summary>
    /// Finds all of the classes which have a base attribute of <see cref="RegisterClass"/>.
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IEnumerable<ClassesToRegister> FindRegisteredClassesByAttribute(IEnumerable<Assembly> assembly)
    {
        var classes = assembly.
            SelectMany(x => x.GetTypes())
            .Where(
                type => type.GetCustomAttributes(typeof(RegisterClass), true).Length > 0
                );
        
        return MapAssembliesToModel(classes);

    }

    /// <summary>
    /// Maps found assemblies to an internal ClassestoRegister object
    /// </summary>
    /// <param name="classes"></param>
    /// <returns></returns>
    private static IEnumerable<ClassesToRegister> MapAssembliesToModel(IEnumerable<Type> classes)
    {
        var mappedClasses = classes.Select((x => new ClassesToRegister()
        {
            ClassName = x,
            InterfaceName = x.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(),
            ServiceLifetime = SetServiceLifetime(x.CustomAttributes?.FirstOrDefault()?.AttributeType?.FullName ?? "")
        }));

        return mappedClasses;
    }

    private static ServiceLifetime SetServiceLifetime(string customAttribute)
    {
        if (customAttribute.Contains(
                "RegisterClassAsScoped"))
        {
            return ServiceLifetime.Scoped;
        }

        if (customAttribute.Contains(
                "RegisterClassAsSingleton"))
        {
            return ServiceLifetime.Singleton;
        }
        
        return ServiceLifetime.Transient;
    }

    
}