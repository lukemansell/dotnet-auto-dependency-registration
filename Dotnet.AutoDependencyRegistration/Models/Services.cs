using System;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.AutoDependencyRegistration.Models;

public class ClassesToRegister
{
    public Type? ClassName { get; set; }
    
    public Type? InterfaceName { get; set; }
    
    public ServiceLifetime ServiceLifetime { get; set; }
}