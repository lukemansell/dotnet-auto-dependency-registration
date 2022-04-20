using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Extensions.Attributes;

public class RegisterClassAsSingleton: RegisterClass
{
    public RegisterClassAsSingleton()
    {
        ServiceLifetime = ServiceLifetime.Singleton;
    }
}