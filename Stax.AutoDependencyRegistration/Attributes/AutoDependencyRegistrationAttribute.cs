using System;
using Microsoft.Extensions.DependencyInjection;

namespace AutoDependencyRegistration.Attributes;

/// <summary>
/// Base attribute, used by <see cref="RegisterClassAsScoped"/>, <see cref="RegisterClassAsSingleton"/> and
/// <see cref="RegisterClassAsTransient"/>. If applied to any class, the service lifetime will be set to transient by
/// default. The same as using  <see cref="RegisterClassAsTransient"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RegisterClass : Attribute
{
    internal RegisterClass()
    {
        
    }
    
    public RegisterClass(bool excludeInterface)
    {
        ExcludeInterface = excludeInterface;
    }
    
    protected ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;
    
    public bool ExcludeInterface { get; set; }
}