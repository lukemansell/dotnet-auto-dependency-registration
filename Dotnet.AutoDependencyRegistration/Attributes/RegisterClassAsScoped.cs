using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Attributes;

public class RegisterClassAsScoped : RegisterClass
{
    public RegisterClassAsScoped()
    {
        ServiceLifetime = ServiceLifetime.Scoped;
    }
}