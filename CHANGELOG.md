# Dotnet Auto Dependency Registration Extension Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.1.0] - 2023-04-16
### Fixed
* Classes with no interface were not being correctly registered after some refactoring in 3.0.0

## [3.0.0] - 2023-04-06
### Added
* Support for registering classes but ignoring their interface. Three new attributes added:
  * [RegisterClassAsSingletonIgnoreInterface]
  * [RegisterClassAsScopedIgnoreInterface]
  * [RegisterClassAsTransientIgnoreInterface]
### Fixed
* Classes with no interfaces were not being registered correctly.

## [2.1.0] - 2022-05-26
### Added
* Support for registering classes with multiple interfaces.
### Changed
* Changed what classes/interfaces can be registered to solve an issue with classes not being registered properly. Generic and abstract classes are no longer registered. This will be addressed in the future.

## [2.0.1] - 2022-05-01
### Fixed
* Registration of classes could get into a state where some classes which had other services injected into them would register as transient by default instead of the desired service lifetime.

## [2.0.0] - 2022-04-27
Stable release.
### Changed
* Updated root namespace from Dotnet.AutoDependencyRegistration to AutoDependencyRegistration. Breaking change.

## [1.1.0-alpha] - 2022-04-26
### Added
* Ability to register services which don't have an interface.

## [1.0.0-alpha] - 2022-04-24
* Initial release.