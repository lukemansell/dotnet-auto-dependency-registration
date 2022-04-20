using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Extensions.Attributes;

public class RegisterClassAsScoped : RegisterClass
{
    public RegisterClassAsScoped()
    {
        ServiceLifetime = ServiceLifetime.Scoped;
    }
}