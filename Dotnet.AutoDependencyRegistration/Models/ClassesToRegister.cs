using System;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Models;

/// <summary>
/// Object used to store the class name, interface name and service
/// lifetime of discovered classes.
/// </summary>
public class ClassesToRegister
{
    public Type? ClassName { get; set; }
    
    public Type? InterfaceName { get; set; }
    
    public ServiceLifetime ServiceLifetime { get; set; }
}