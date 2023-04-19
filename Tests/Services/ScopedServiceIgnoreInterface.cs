using AutoDependencyRegistration.Attributes;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tests.Services;

[RegisterClassAsScoped(ExcludeInterface = true)]
public class ScopedServiceIgnoreInterface : ActionFilterAttribute
{
    public string DemoService()
    {
        return "Hi!";
    }
}