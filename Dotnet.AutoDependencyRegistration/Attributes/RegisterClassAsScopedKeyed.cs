using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

/// <summary>
/// [RegisterClassAsScopedKeyed("key")] attribute which can be added on top of any
/// class to register it as a keyed scoped service. The key is used to identify
/// which implementation to resolve when multiple implementations of the same interface exist.
/// Requires .NET 8 or later.
/// </summary>
public class RegisterClassAsScopedKeyed : RegisterClass
{
    public string ServiceKey { get; }

    public RegisterClassAsScopedKeyed(string serviceKey)
    {
        ServiceKey = serviceKey;
        ServiceLifetime = ServiceLifetime.Scoped;
    }
}

