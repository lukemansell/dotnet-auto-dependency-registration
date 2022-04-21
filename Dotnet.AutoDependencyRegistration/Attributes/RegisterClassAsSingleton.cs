using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Attributes;

public class RegisterClassAsSingleton: RegisterClass
{
    public RegisterClassAsSingleton()
    {
        ServiceLifetime = ServiceLifetime.Singleton;
    }
}