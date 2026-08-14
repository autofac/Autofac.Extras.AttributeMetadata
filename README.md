# Autofac.Extras.AttributeMetadata

Attribute metadata support for Autofac IoC

[![Build status](https://github.com/autofac/Autofac.Extras.AttributeMetadata/actions/workflows/main.yml/badge.svg)](https://github.com/autofac/Autofac.Extras.AttributeMetadata/actions/workflows/main.yml) [![codecov](https://codecov.io/gh/Autofac/Autofac.Extras.AttributeMetadata/branch/develop/graph/badge.svg)](https://codecov.io/gh/Autofac/Autofac.Extras.AttributeMetadata) [![NuGet](https://img.shields.io/nuget/v/Autofac.Extras.AttributeMetadata.svg)](https://nuget.org/packages/Autofac.Extras.AttributeMetadata)

Please file issues and pull requests for this package in this repository rather than in the Autofac core repo.

- [Documentation](https://autofac.readthedocs.io/en/latest/advanced/metadata.html)
- [NuGet](https://www.nuget.org/packages/Autofac.Extras.AttributeMetadata)
- [Contributing](https://autofac.readthedocs.io/en/latest/contributors.html)
- [Open in Visual Studio Code](https://open.vscode.dev/autofac/Autofac.Extras.AttributeMetadata)

## Quick Start

First, create a metadata attribute - any `System.Attribute` that has MEF's `[MetadataAttribute]` applied. Every publicly readable property becomes one metadata name/value pair, so the attribute below provides `Age` metadata:

```csharp
[MetadataAttribute]
public class AgeMetadataAttribute : Attribute
{
  public int Age { get; private set; }

  public AgeMetadataAttribute(int age)
  {
    Age = age;
  }
}
```

Apply it to the component implementation rather than to the service interface:

```csharp
public interface IArtwork
{
  void Display();
}

[AgeMetadata(100)]
public class CenturyArtwork : IArtwork
{
  public void Display() { ... }
}
```

Then register the `AttributedMetadataModule` so the container reads those attributes, and consume the metadata as you would any other:

```csharp
var builder = new ContainerBuilder();
builder.RegisterModule<AttributedMetadataModule>();
builder.RegisterType<CenturyArtwork>().As<IArtwork>();
var container = builder.Build();

var artwork = container.Resolve<IEnumerable<Meta<IArtwork>>>()
                       .First(a => a.Metadata["Age"].Equals(100));
artwork.Value.Display();
```

[You can read more details in the documentation.](https://autofac.readthedocs.io/en/latest/advanced/metadata.html)

## Get Help

**Need help with Autofac?** We have [a documentation site](https://autofac.readthedocs.io/) as well as [API documentation](https://autofac.org/apidoc/). We're ready to answer your questions on [Stack Overflow](https://stackoverflow.com/questions/tagged/autofac) or check out the [discussion forum](https://groups.google.com/forum/#forum/autofac).
