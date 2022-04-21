﻿using Dotnet.AutoDependencyRegistration.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration;

public static class RegisterClassesExtension
{
    /// <summary>
    /// Registers all classes which have [RegisterClassAsScoped], [RegisterClassAsSingleton] or [RegisterClassAsTransient]
    /// above them automatically. This scans all assemblies but only registers classes which have one of these attributes.
    /// Having multiple attributes will cause the first one to get used. Classes must have an interface to be registered.
    /// See: <see cref="LogAutoRegister"/> to demo log what classes would be registered without actually registering.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>A string where you can see a list of registered classes if you put a breakpoint over the implementation</returns>
    public static string AutoRegisterDependencies (
        this IServiceCollection services)
    {
        return RegisterDependenciesService.Register(services);
    }
}