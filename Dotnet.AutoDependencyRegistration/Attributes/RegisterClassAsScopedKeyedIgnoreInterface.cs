using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

/// <summary>
/// [RegisterClassAsScopedKeyedIgnoreInterface("key")] attribute which can be added on top of any
/// class to register it as a keyed scoped service without registering its interfaces.
/// The key is used to identify which implementation to resolve.
/// Requires .NET 8 or later.
/// </summary>
public class RegisterClassAsScopedKeyedIgnoreInterface : RegisterClass
{
    public string ServiceKey { get; }

    public RegisterClassAsScopedKeyedIgnoreInterface(string serviceKey)
    {
        ServiceKey = serviceKey;
        ServiceLifetime = ServiceLifetime.Scoped;
    }
}

