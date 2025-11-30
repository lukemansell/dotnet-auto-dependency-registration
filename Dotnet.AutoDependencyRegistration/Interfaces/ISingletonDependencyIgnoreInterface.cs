namespace AutoDependencyRegistration.Interfaces;

/// <summary>
/// Marker interface for classes that should be registered as Singleton dependencies
/// without registering their implemented interfaces.
/// Classes implementing this interface will be automatically registered with ServiceLifetime.Singleton
/// and will ignore any interfaces they implement.
/// </summary>
public interface ISingletonDependencyIgnoreInterface
{
}

