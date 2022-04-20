using System;

namespace Dotnet.AutoDependencyRegistration.Extensions.Attributes;
using Microsoft.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Class)]
public class RegisterClass : Attribute
{
    public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;
}