using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

/// <summary>
/// [RegisterClassAsTransientKeyedIgnoreInterface("key")] attribute which can be added on top of any
/// class to register it as a keyed transient service without registering its interfaces.
/// The key is used to identify which implementation to resolve.
/// Requires .NET 8 or later.
/// </summary>
public class RegisterClassAsTransientKeyedIgnoreInterface : RegisterClass
{
    public string ServiceKey { get; }

    public RegisterClassAsTransientKeyedIgnoreInterface(string serviceKey)
    {
        ServiceKey = serviceKey;
        ServiceLifetime = ServiceLifetime.Transient;
    }
}

