# Attribute Based Dependency Injection for .NET

[![Version](https://img.shields.io/nuget/vpre/stax.autodependencyinjectionregistration.svg)](https://www.nuget.org/packages/stax.autodependencyinjectionregistration)
[![Downloads](https://img.shields.io/nuget/dt/stax.autodependencyinjectionregistration.svg)](https://www.nuget.org/packages/stax.autodependencyinjectionregistration)
---

## Summary
This [NuGet library](https://www.nuget.org/packages/Stax.AutoDependencyInjectionRegistration/) helps to easily register classes without having to add a whole bunch of lines such as `service.AddScoped<IService, Service>()`. In projects which contain a number of services, this can inflate your code with potentially tens to hundreds of lines.

Auto Dependency Registration makes this easy. All you need is to add `services.AutoRegisterDependencies();` within your programs `ConfigureServices` method and then use one of three registration methods:

1. **Attribute-Based**: Add attributes like `[RegisterClassAsTransient]` above your classes
2. **Interface-Based**: Implement marker interfaces like `ITransientDependency` 
3. **Keyed Services** (Requires .NET 8+): Use keyed attributes like `[RegisterClassAsTransientKeyed("key")]` for multiple implementations of the same interface

Dotnet Auto Dependency Registration will take care of the rest without you having to specify assemblies or service name structures for it to pick up.

## Why I have written this
While they are a handful of auto DI registration solutions out there, not many of them make it as easy as we were hoping in a project I was working on as we were using a number of projects and didn't want to have to specify a bunch of assemblies and also service name structures to automatically pick up.

I've also been heavily interested in dependency injection and wanted to play around with how it worked and how I could make a lightweight solution to reduce the need to specify in your `Program.cs` file a whole bunch of services to register. I love the ability to reduce code and believe auto DI registration extension methods are of great use.

## How to use

#### Current solution
Usually your `Program.cs` file will look something like this:

```
services.AddScoped<IService, Service>();
services.AddTransient<IService2, Service2>();
services.AddSingleton<IService3, Service3>();
services.AddScoped<IService4, Service4>();
services.AddTransient<IService5, Service5>();
services.AddSingleton<IService6, Service6>();
services.AddScoped<IService7, Service7>();
services.AddTransient<IService8, Service8>();
services.AddSingleton<IService9, Service9>();

etc
```
#### How this extension method helps
Using this extension simplifies this greatly.

Within your `Program.cs` simply add:

```c#
services.AutoRegisterDependencies();
```

Depending on your version of .NET, the containing method example may be:

.NET 5:
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AutoRegisterDependencies();
 }
 ```

.NET 6:

```c#
builder.services.AutoRegisterDependencies();
```

From there, you have multiple ways to register your classes:

### Attribute-Based Registration

Add attributes on top of your classes:

```
[RegisterClassAsScoped] - Register as scoped
[RegisterClassAsTransient] - Register as transient
[RegisterClassAsSingleton] - Register as singleton
[RegisterClassAsScopedIgnoreInterface] - Register as scoped but ignore registering the interface
[RegisterClassAsTransientIgnoreInterface] - Register as transient but ignore registering the interface
[RegisterClassAsSingletonIgnoreInterface] - Register as singleton but ignore registering the interface
```

_You are also able to use the base attribute `[RegisterClass]` which defaults to transient._

### Interface-Based Registration

Alternatively, you can implement marker interfaces instead of using attributes:

```csharp
public class MyService : IMyService, ITransientDependency
{
    // Implementation
}
```

Available marker interfaces:
- `ITransientDependency` - Register as transient
- `IScopedDependency` - Register as scoped
- `ISingletonDependency` - Register as singleton
- `ITransientDependencyIgnoreInterface` - Register as transient but ignore registering the interface
- `IScopedDependencyIgnoreInterface` - Register as scoped but ignore registering the interface
- `ISingletonDependencyIgnoreInterface` - Register as singleton but ignore registering the interface

### Keyed Services (Requires .NET 8+)

For keyed service registration (useful when you have multiple implementations of the same interface), use keyed attributes:

```csharp
[RegisterClassAsTransientKeyed("ServiceA")]
public class ServiceA : IService
{
    // Implementation
}

[RegisterClassAsTransientKeyed("ServiceB")]
public class ServiceB : IService
{
    // Implementation
}
```

Available keyed attributes:
- `[RegisterClassAsTransientKeyed("key")]` - Register as transient with a key
- `[RegisterClassAsScopedKeyed("key")]` - Register as scoped with a key
- `[RegisterClassAsSingletonKeyed("key")]` - Register as singleton with a key
- `[RegisterClassAsTransientKeyedIgnoreInterface("key")]` - Register as transient with a key but ignore registering the interface
- `[RegisterClassAsScopedKeyedIgnoreInterface("key")]` - Register as scoped with a key but ignore registering the interface
- `[RegisterClassAsSingletonKeyedIgnoreInterface("key")]` - Register as singleton with a key but ignore registering the interface

To resolve keyed services in .NET 8+:

```csharp
// Using IKeyedServiceProvider
var serviceA = keyedServiceProvider.GetRequiredKeyedService<IService>("ServiceA");
var serviceB = keyedServiceProvider.GetRequiredKeyedService<IService>("ServiceB");

// Or using [FromKeyedServices] attribute in constructor injection
public class MyController
{
    public MyController([FromKeyedServices("ServiceA")] IService service)
    {
        // service will be the ServiceA implementation
    }
}
```

You are able to register classes which have interfaces and classes which don't have an interface.

On startup you will see Information logs showing you what classes have been registered and with which ServiceLifetime, in the format of: "`ClassName`, `InterfaceName` has been registered as `ServiceLifetime`." or "`ClassName` has been registered as `ServiceLifetime`."

#### Practical examples

**Attribute-Based Registration:**

`Class.cs`
```c#
[RegisterClassAsSingleton]
public class Class1 : IClass1
{
    public string Demo()
    {
        return "Hi!";
    }
}
```

**Interface-Based Registration:**

`Class2.cs`
```c#
public class Class2 : IClass2, ITransientDependency
{
    public string Demo()
    {
        return "Hi!";
    }
}
```

**Keyed Services (Requires .NET 8+):**

`ServiceA.cs`
```c#
[RegisterClassAsTransientKeyed("ServiceA")]
public class ServiceA : IService
{
    public string Process()
    {
        return "Processing with ServiceA";
    }
}
```

`ServiceB.cs`
```c#
[RegisterClassAsTransientKeyed("ServiceB")]
public class ServiceB : IService
{
    public string Process()
    {
        return "Processing with ServiceB";
    }
}
```

**Class without interface:**

`Class3.cs`
```c#
[RegisterClassAsTransient]
public class Class3
{
    public string Demo()
    {
        return "Hi!";
    }
}
```

`Program.cs`
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AutoRegisterDependencies();
}
```

This will cause log entries in console on startup:

```
Class1, IClass1 has been registered as Singleton
Class2, IClass2 has been registered as Transient
ServiceA, IService has been registered as Transient with key 'ServiceA'
ServiceB, IService has been registered as Transient with key 'ServiceB'
Class3 has been registered as Transient
```

I also try to circumvent potential mistakes such as: 

`Class1.cs`
```c#
[RegisterClassAsTransient]
[RegisterClassAsSingleton]
public class Class1 : IClass1
{
    public string DemoService()
    {
        return "Hi!";
    }
}
```

In this situation the first attribute will always be used, so the class in this example will be registered as Transient.
