using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

/// <summary>
/// [RegisterClassAsTransientKeyed("key")] attribute which can be added on top of any
/// class to register it as a keyed transient service. The key is used to identify
/// which implementation to resolve when multiple implementations of the same interface exist.
/// Requires .NET 8 or later.
/// </summary>
public class RegisterClassAsTransientKeyed : RegisterClass
{
    public string ServiceKey { get; }

    public RegisterClassAsTransientKeyed(string serviceKey)
    {
        ServiceKey = serviceKey;
        ServiceLifetime = ServiceLifetime.Transient;
    }
}

