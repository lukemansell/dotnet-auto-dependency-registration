# Dotnet Auto Dependency Registration Extension

**Version 1.0.0-alpha:** This package is currently in an alpha pre-release. Feel free to install and play around with this package on your own project, but I would advise not putting it into a production application until 2.0.0 when it is considered stable.

[![Version](https://img.shields.io/nuget/vpre/stax.autodependencyinjectionregistration.svg)](https://www.nuget.org/packages/stax.autodependencyinjectionregistration)
[![Downloads](https://img.shields.io/nuget/dt/stax.autodependencyinjectionregistration.svg)](https://www.nuget.org/packages/stax.autodependencyinjectionregistration)

---

## Summary
This [NuGet library](https://www.nuget.org/packages/Stax.AutoDependencyInjectionRegistration/) helps to easily register classes without having to add a whole bunch of lines such as `service.AddScoped<IService, Service>()`. In projects which contain a number of services, this can inflate your code with potentially tens to hundreds of lines.

Dotnet Auto Dependency Registration Extension makes this easy. All you need is to add `services.AutoRegisterDependencies();` within your programs `ConfigureServices` method and then add attributes above your classes to register them as either Transient, Scoped or Singleton (examples below of how to do this). Dotnet Auto Dependency Registration will take care of the rest without you having to specify assemblies or service name structures for it to pick up.

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

From there, on top of your classes you have three attribute options:

```
[RegisterClassAsScoped] - Register as scoped
[RegisterClassAsTransient] - Register as transient
[RegisterClassAsSingleton] - Register as singleton
```

_You are also able to use the base attribute `[RegisterClass]` which defaults to transient._

You are able to register classes which have interfaces and classes which don't have an interface.

On startup you will see Information logs showing you what classes have been registered and with which ServiceLifetime, in the format of: "`ClassName`, `InterfaceName` has been registered as `ServiceLifetime`." or "`ClassName` has been registered as `ServiceLifetime`."

#### Practical example

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

`Class2.cs`
```c#
[RegisterClassAsTransient]
public class Class2 : IClass2
{
    public string Demo()
    {
        return "Hi!";
    }
}
```

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

This will cause two log entries in console on startup:

```
Class1, IClass1 has been registered as Singleton
Class2, IClass2 has been registered as Transient
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

