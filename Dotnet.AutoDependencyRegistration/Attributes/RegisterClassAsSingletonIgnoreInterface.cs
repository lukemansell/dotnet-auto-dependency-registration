using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

public class RegisterClassAsSingletonIgnoreInterface : RegisterClass
{
    public RegisterClassAsSingletonIgnoreInterface()
    {
        ServiceLifetime = ServiceLifetime.Singleton;
    }
}