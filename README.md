# Strix Framework

### Strix is an open-source, modular, configurable, and easy-in-use framework for building complex bots with C#.

> # 🚧 Work In Progress 🚧
> Current sources represents a set of ideas and features which will be implemented in the future.
> Almost all library parts is on ide
> Some descriptions of features are available below in next paragraphs.

#### Strix consist of set of libraries, oriented on simplifying bot development to make it more easily for developers.

#### Framework's general concepts are:

- **Modularity**. Framework is designed to be modular and extensible to provide developers a huge field in development complex bot applications and provides easy integration to an already existing codebase. Each component is fully configurable and could be included or excluded via a few lines of code.
  
- **Fluent-design**. All libraries, features, and extensions are built in fluent interface, which makes them fully configurable and more convenient to developers.

- **Pipeline**. Core library processing process is based on `Pipeline`s. For each `Update`'s `Entity` could be set as many `Pipeline`'s as you want. Also, if for one `Entity` type are set two or more `Pipeline`'s, by default, they'll be executed one-by-one, but execution policy could configured on your needs when you building pipeline.

- **Reflection**. All handlers, commands and other processing stuff are registering using **Reflection**. Each handler or command method, decorated with specified in configuration `Attribute`s, will be registered via in-built assembly and type scanner. Also, type activation and method execution is performing with using **Dependency Injection**, which provides maximal extensibility for each handler or command.

## Information about features and libraries

Each feature library consists of Abstractions project, and its realization project, for example: **Strix.Commands.Abstractions** and **Strix.Commands**.

Already existing features (mostly, just projects where they'll be implemented in the future):

- **Reflection** - **Core-required**. Provides scanning Types, Assemblies, Type activation, Method arguments retrieving (retrieving and creating arguments from DependencyInjection Container).
- **Resolving** - **Core-required**. Provides intelligent and light wrapper over existing DependencyInjection libraries. Supports few providers.
    - **Providers**
        - **Autofac** - Support for [Autofac](https://autofac.org).
        - **Microsoft.Extensions.DependencyInjection** - Support for [Microsoft Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage).
- **Commands** - Support for commands processing.
  - **Parsing** - Support for parsing command arguments. Fully configurable extension.
- **FileSystem** - Support for working with filesystems.
  - **Providers**
    - **Firebase** - Support for **Firebase Cloud Storage**.
    - **System** - Provides Windows filesystem wrapper.
- **Handlers** - Support for handlers - common **Update** processing workers.
- **Hosting** - Provides few libraries to simplify deployment in some environments.
  - **Console** - Provides deployment helpful stuff for **Console Applications**.
  - **Web** - Provides deployment helpful stuff for **ASP.NET Core Applications**.
- **Localization** - Built-in support for localization. Supports few localization sources.
  - **Providers**
    - **Json** - Provides loading localization data from JSON files.
    - **Xml** - Provides loading localization data from Xml files.
    - **Yaml** - Provides loading localization data from Yaml files.
- **Progress** - Creation, management and tracking progresses.
- **Sessions** - Provides sessions support. Allows to make stateless interfaces for retrieving information from the user. More information will be available later.
- **Widgets** - Provides stateful widgets support. Helps with interactions with user, displaying some progress, operations, etc. More information will be available later.

Soon will be added new features, and information about them.