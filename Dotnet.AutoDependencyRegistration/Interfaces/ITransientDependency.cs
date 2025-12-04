namespace AutoDependencyRegistration.Interfaces;

/// <summary>
/// Marker interface for classes that should be registered as Transient dependencies.
/// Classes implementing this interface will be automatically registered with ServiceLifetime.Transient.
/// </summary>
public interface ITransientDependency
{
}

