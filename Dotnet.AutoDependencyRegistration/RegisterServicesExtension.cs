using AutoDependencyRegistration.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration;

public static class RegisterClassesExtension
{
    /// <summary>
    /// Registers all classes which have [RegisterClassAsScoped], [RegisterClassAsSingleton] or [RegisterClassAsTransient]
    /// above them automatically. This scans all assemblies but only registers classes which have one of these attributes.
    /// Having multiple attributes will cause the first one to get used. Using [RegisterClass] registers the class as
    /// transient.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>A string where you can see a list of registered classes if you put a breakpoint over the implementation</returns>
    public static string AutoRegisterDependencies (
        this IServiceCollection services)
    {
        return RegisterDependenciesService.Register(services);
    }
}