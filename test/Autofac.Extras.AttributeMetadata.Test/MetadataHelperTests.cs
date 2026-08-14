// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.Composition;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;

namespace Autofac.Extras.AttributeMetadata.Test;

public class MetadataHelperTests
{
    [Fact]
    public void GetMetadata_MultipleWeakTypedAttributes()
    {
        var metadata = MetadataHelper.GetMetadata(typeof(CombinationalWeakTypedScenario)).ToList();

        Assert.Equal(2, metadata.Count);
        Assert.Equal("Hello", metadata.FirstOrDefault(p => p.Key == "Name").Value);
        Assert.Equal(42, metadata.FirstOrDefault(p => p.Key == "Age").Value);
    }

    [Fact]
    public void GetMetadata_NoMatchingAttributes()
    {
        var metadata = MetadataHelper.GetMetadata(typeof(MetadataModuleScenario)).ToList();

        Assert.Empty(metadata);
    }

    [Fact]
    public void GetMetadata_NullTargetType()
    {
        Assert.Throws<ArgumentNullException>(() => MetadataHelper.GetMetadata(null!));
    }

    [Fact]
    public void GetMetadata_SingleWeakTypedAttribute()
    {
        var metadata = MetadataHelper.GetMetadata(typeof(WeakTypedScenario)).ToList();

        Assert.Single(metadata);
        Assert.Equal("Hello", metadata.FirstOrDefault(p => p.Key == "Name").Value);
    }

    [Fact]
    public void GetMetadata_TypedMetadataNotFound()
    {
        var metadata = MetadataHelper.GetMetadata<INameMetadata>(typeof(MetadataModuleScenario)).ToList();

        Assert.Empty(metadata);
    }

    [Fact]
    public void GetMetadata_TypedMetadataNullTargetType()
    {
        Assert.Throws<ArgumentNullException>(() => MetadataHelper.GetMetadata<INameAndAgeMetadata>(null!));
    }

    [Fact]
    public void GetMetadata_TypedMetadataOnType()
    {
        var metadata = MetadataHelper.GetMetadata<INameAndAgeMetadata>(typeof(StrongTypedScenario)).ToList();

        Assert.Equal(2, metadata.Count);
        Assert.Equal("Hello", metadata.FirstOrDefault(p => p.Key == "Name").Value);
        Assert.Equal(42, metadata.FirstOrDefault(p => p.Key == "Age").Value);
    }

    [Fact]
    public void GetProperties_MetadataProvider()
    {
        // An attribute implementing IMetadataProvider supplies its own metadata rather than
        // having its properties reflected over.
        var metadata = MetadataHelper.GetProperties(new ProvidedMetadataAttribute(), typeof(MetadataProviderScenario)).ToList();

        Assert.Equal(2, metadata.Count);
        Assert.Equal("Value1", metadata.FirstOrDefault(p => p.Key == "Key1").Value);
        Assert.Equal("Value2", metadata.FirstOrDefault(p => p.Key == "Key2").Value);
    }

    [Fact]
    public void GetProperties_NullInstanceType()
    {
        Assert.Throws<ArgumentNullException>(() => MetadataHelper.GetProperties(new NameMetadataAttribute("Hello"), null!));
    }

    [Fact]
    public void GetProperties_NullTarget()
    {
        Assert.Throws<ArgumentNullException>(() => MetadataHelper.GetProperties(null!, typeof(WeakTypedScenario)));
    }

    [Fact]
    public void GetProperties_WriteOnlyProperty()
    {
        // Only publicly readable properties become metadata pairs, so a write-only property
        // contributes nothing.
        var attribute = new WriteOnlyMetadataAttribute
        {
            Value = "Hello",
        };

        var metadata = MetadataHelper.GetProperties(attribute, typeof(WeakTypedScenario));

        Assert.Empty(metadata);
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    private sealed class WriteOnlyMetadataAttribute : Attribute
    {
        private string? _value;

        public string Value
        {
            set => _value = value;
        }

        public override string ToString() => _value ?? string.Empty;
    }
}
