using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoDependencyRegistration.Attributes;
using AutoDependencyRegistration.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Helpers;

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
            .Where(type => type.GetCustomAttributes(typeof(RegisterClass), true).Length > 0)
            .Where(x => !x.IsAbstract && !x.IsInterface && !x.IsGenericType);
        
        return MapAssembliesToModel(classes);
    }
    
    /// <summary>
    /// Returns an list of assemblies found by checking the base directory for all DLLs.
    /// </summary>
    /// <returns>List of assemblies</returns>
    public static IEnumerable<Assembly> GetAssemblies()
    {
        return Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
    }

    /// <summary>
    /// Maps found assemblies to an ClassesToRegister object
    /// </summary>
    /// <param name="classes"></param>
    /// <returns></returns>
    private static IEnumerable<ClassesToRegister> MapAssembliesToModel(IEnumerable<Type> classes)
    {
        var mappedClasses = classes.Select((x => new ClassesToRegister
        {
            ClassName = x.GetTypeInfo(),
            InterfaceName = x.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(),
            ServiceLifetime = SetServiceLifetime(x.CustomAttributes?.FirstOrDefault(a => a.AttributeType.FullName.Contains("AutoDependencyRegistration"))?.AttributeType?.FullName ?? "")
        }));

        return mappedClasses;
    }

    /// <summary>
    /// Maps the name of the attribute above the class to a <see cref="ServiceLifetime"/>
    /// </summary>
    /// <param name="customAttribute">The custom attribute above the class for auto registration</param>
    /// <returns></returns>
    private static ServiceLifetime SetServiceLifetime(string customAttribute)
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
}