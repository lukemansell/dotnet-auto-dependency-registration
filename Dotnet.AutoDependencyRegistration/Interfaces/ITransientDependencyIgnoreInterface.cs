namespace AutoDependencyRegistration.Interfaces;

/// <summary>
/// Marker interface for classes that should be registered as Transient dependencies
/// without registering their implemented interfaces.
/// Classes implementing this interface will be automatically registered with ServiceLifetime.Transient
/// and will ignore any interfaces they implement.
/// </summary>
public interface ITransientDependencyIgnoreInterface
{
}

