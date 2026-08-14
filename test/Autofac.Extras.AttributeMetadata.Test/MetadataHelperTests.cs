// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.Composition;
using Autofac.Extras.AttributeMetadata.Test.Stubs;

namespace Autofac.Extras.AttributeMetadata.Test;

public class MetadataHelperTests
{
    [Fact]
    public void GetMetadata_MultipleReflectedAttributes()
    {
        var metadata = MetadataHelper.GetMetadata(typeof(CombinedComponent)).ToList();

        Assert.Equal(2, metadata.Count);
        Assert.Equal("Hello", metadata.FirstOrDefault(p => p.Key == "Data").Value);
        Assert.Equal(42, metadata.FirstOrDefault(p => p.Key == "Count").Value);
    }

    [Fact]
    public void GetMetadata_NoMatchingAttributes()
    {
        var metadata = MetadataHelper.GetMetadata(typeof(ProgrammaticComponent)).ToList();

        Assert.Empty(metadata);
    }

    [Fact]
    public void GetMetadata_NullTargetType()
    {
        Assert.Throws<ArgumentNullException>(() => MetadataHelper.GetMetadata(null!));
    }

    [Fact]
    public void GetMetadata_SingleReflectedAttribute()
    {
        var metadata = MetadataHelper.GetMetadata(typeof(ReflectedComponent)).ToList();

        Assert.Single(metadata);
        Assert.Equal("Hello", metadata.FirstOrDefault(p => p.Key == "Data").Value);
    }

    [Fact]
    public void GetMetadata_TypedViewNotFound()
    {
        var metadata = MetadataHelper.GetMetadata<IDataView>(typeof(ProgrammaticComponent)).ToList();

        Assert.Empty(metadata);
    }

    [Fact]
    public void GetMetadata_TypedViewNullTargetType()
    {
        Assert.Throws<ArgumentNullException>(() => MetadataHelper.GetMetadata<IDataAndCountView>(null!));
    }

    [Fact]
    public void GetMetadata_TypedViewOnAttribute()
    {
        var metadata = MetadataHelper.GetMetadata<IDataAndCountView>(typeof(TypedComponent)).ToList();

        Assert.Equal(2, metadata.Count);
        Assert.Equal("Hello", metadata.FirstOrDefault(p => p.Key == "Data").Value);
        Assert.Equal(42, metadata.FirstOrDefault(p => p.Key == "Count").Value);
    }

    [Fact]
    public void GetProperties_MetadataProvider()
    {
        // A provider attribute supplies its own dictionary rather than having properties reflected.
        var metadata = MetadataHelper.GetProperties(new ProvidedDataAttribute(), typeof(ProvidedComponent)).ToList();

        Assert.Equal(2, metadata.Count);
        Assert.Equal("Value1", metadata.FirstOrDefault(p => p.Key == "Key1").Value);
        Assert.Equal("Value2", metadata.FirstOrDefault(p => p.Key == "Key2").Value);
    }

    [Fact]
    public void GetProperties_NullInstanceType()
    {
        Assert.Throws<ArgumentNullException>(() => MetadataHelper.GetProperties(new DataAttribute("Hello"), null!));
    }

    [Fact]
    public void GetProperties_NullTarget()
    {
        Assert.Throws<ArgumentNullException>(() => MetadataHelper.GetProperties(null!, typeof(ReflectedComponent)));
    }

    [Fact]
    public void GetProperties_WriteOnlyProperty()
    {
        // Only publicly readable properties become metadata pairs, so a write-only property
        // contributes nothing.
        var attribute = new WriteOnlyAttribute
        {
            Data = "Hello",
        };

        var metadata = MetadataHelper.GetProperties(attribute, typeof(ReflectedComponent));

        Assert.Empty(metadata);
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    private sealed class WriteOnlyAttribute : Attribute
    {
        private string? _data;

        public string Data
        {
            set => _data = value;
        }

        public override string ToString() => _data ?? string.Empty;
    }
}
