using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Attributes;

public class RegisterClassAsTransient : RegisterClass
{
    public RegisterClassAsTransient()
    {
        ServiceLifetime = ServiceLifetime.Transient;
    }
}