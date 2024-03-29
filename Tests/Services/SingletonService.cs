﻿using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsSingleton]
public class SingletonService : ISingletonService
{
    public string DemoService()
    {
        return "Hi!";
    }
}