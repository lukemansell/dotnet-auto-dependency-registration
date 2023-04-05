using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

public class RegisterClassAsTransientIgnoreInterface : RegisterClass
{
    public RegisterClassAsTransientIgnoreInterface()
    {
        ServiceLifetime = ServiceLifetime.Transient;
    }
}