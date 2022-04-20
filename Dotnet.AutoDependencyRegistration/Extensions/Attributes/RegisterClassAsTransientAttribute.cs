using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Extensions.Attributes;

public class RegisterClassAsTransient : RegisterClass
{
    public RegisterClassAsTransient()
    {
        ServiceLifetime = ServiceLifetime.Transient;
    }
}