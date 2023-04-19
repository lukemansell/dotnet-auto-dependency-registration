using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

/// <summary>
/// [RegisterClassAsScoped] attribute which can be added on top of any
/// class. Sets ServiceLifetime in the base <see cref="RegisterClass"/>
/// to Scoped.
/// </summary>
public class RegisterClassAsScoped : RegisterClass
{
    public RegisterClassAsScoped()
    {
        ServiceLifetime = ServiceLifetime.Scoped;
    }
}