using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Attributes;

/// <summary>
/// [RegisterClassAsSingleton] attribute which can be added on top of any
/// class. Sets ServiceLifetime in the base <see cref="RegisterClass"/>
/// to Singleton.
/// </summary>
public class RegisterClassAsSingleton: RegisterClass
{
    public RegisterClassAsSingleton()
    {
        ServiceLifetime = ServiceLifetime.Singleton;
    }
}