using AutoDependencyRegistration.Attributes;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tests.Services;

[RegisterClassAsScopedIgnoreInterface]
public class ScopedServiceIgnoreInterface : ActionFilterAttribute
{
    public string DemoService()
    {
        return "Hi!";
    }
}