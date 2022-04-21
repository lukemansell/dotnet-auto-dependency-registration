using System;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RegisterClass : Attribute
{
    public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;
}