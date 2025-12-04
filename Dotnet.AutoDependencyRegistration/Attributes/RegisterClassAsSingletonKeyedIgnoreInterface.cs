using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

/// <summary>
/// [RegisterClassAsSingletonKeyedIgnoreInterface("key")] attribute which can be added on top of any
/// class to register it as a keyed singleton service without registering its interfaces.
/// The key is used to identify which implementation to resolve.
/// Requires .NET 8 or later.
/// </summary>
public class RegisterClassAsSingletonKeyedIgnoreInterface : RegisterClass
{
    public string ServiceKey { get; }

    public RegisterClassAsSingletonKeyedIgnoreInterface(string serviceKey)
    {
        ServiceKey = serviceKey;
        ServiceLifetime = ServiceLifetime.Singleton;
    }
}

