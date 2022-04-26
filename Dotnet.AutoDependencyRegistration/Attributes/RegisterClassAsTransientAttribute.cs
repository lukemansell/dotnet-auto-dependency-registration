using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

/// <summary>
/// [RegisterClassAsTransient] attribute which can be added on top of any
/// class. Sets ServiceLifetime in the base <see cref="RegisterClass"/>
/// to Transient.
/// </summary>
public class RegisterClassAsTransient : RegisterClass
{
    public RegisterClassAsTransient()
    {
        ServiceLifetime = ServiceLifetime.Transient;
    }
}