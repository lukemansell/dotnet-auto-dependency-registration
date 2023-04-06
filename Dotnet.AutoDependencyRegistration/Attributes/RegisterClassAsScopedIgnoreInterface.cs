using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

public class RegisterClassAsScopedIgnoreInterface : RegisterClass
{
    public RegisterClassAsScopedIgnoreInterface()
    {
        ServiceLifetime = ServiceLifetime.Scoped;
    }
}