# Autofac.Extras.AttributeMetadata

Attribute metadata support for Autofac IoC

[![Build status](https://github.com/autofac/Autofac.Extras.AttributeMetadata/actions/workflows/main.yml/badge.svg)](https://github.com/autofac/Autofac.Extras.AttributeMetadata/actions/workflows/main.yml) [![codecov](https://codecov.io/gh/Autofac/Autofac.Extras.AttributeMetadata/branch/develop/graph/badge.svg)](https://codecov.io/gh/Autofac/Autofac.Extras.AttributeMetadata) [![NuGet](https://img.shields.io/nuget/v/Autofac.Extras.AttributeMetadata.svg)](https://nuget.org/packages/Autofac.Extras.AttributeMetadata)

Please file issues and pull requests for this package in this repository rather than in the Autofac core repo.

- [Documentation](https://autofac.readthedocs.io/en/latest/advanced/metadata.html)
- [NuGet](https://www.nuget.org/packages/Autofac.Extras.AttributeMetadata)
- [Contributing](https://autofac.readthedocs.io/en/latest/contributors.html)
- [Open in Visual Studio Code](https://open.vscode.dev/autofac/Autofac.Extras.AttributeMetadata)

## Quick Start

Once you've added a reference to the `Autofac.Extras.AttributeMetadata` package, you can start by creating your aggregate service interface. The idea is that, instead of injecting several individual services into a consumer, you have a single aggregate that gets injected, where each property is one of the dependencies:

```csharp
public interface IMyAggregateService
{
  IFirstService FirstService { get; }
  ISecondService SecondService { get; }
}
```

Update your consumer to take in the aggregate:

```csharp
public class SomeController
{
  private readonly IMyAggregateService _aggregateService;

  public SomeController(IMyAggregateService aggregateService)
  {
    _aggregateService = aggregateService;
  }
}
```

Finally, make sure you register the individual dependencies, the aggregate service interface, and your consumer.

```csharp
var builder = new ContainerBuilder();
builder.RegisterAggregateService<IMyAggregateService>();
builder.Register(/*...*/).As<IFirstService>();
builder.Register(/*...*/).As<ISecondService>();
builder.RegisterType<SomeController>();
var container = builder.Build();
```

When you resolve the consumer, the aggregate service will be injected and you can use the properties on that. This allows you to add new dependencies to the interface without changing all of your consumers.

## Get Help

**Need help with Autofac?** We have [a documentation site](https://autofac.readthedocs.io/) as well as [API documentation](https://autofac.org/apidoc/). We're ready to answer your questions on [Stack Overflow](https://stackoverflow.com/questions/tagged/autofac) or check out the [discussion forum](https://groups.google.com/forum/#forum/autofac).
