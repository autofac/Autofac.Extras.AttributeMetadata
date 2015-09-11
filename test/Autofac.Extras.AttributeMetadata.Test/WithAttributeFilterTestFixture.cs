using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Autofac.Features.Metadata;
using Autofac.Features.OwnedInstances;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class WithAttributeFilterTestFixture
    {
        [Fact]
        public void multiple_filter_types_can_be_used_on_one_component()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AttributedMetadataModule());
            builder.RegisterType<MsBuildAdapter>().As<IAdapter>();
            builder.RegisterType<DteAdapter>().As<IAdapter>();
            builder.RegisterType<ToolWindowAdapter>().As<IAdapter>();
            builder.RegisterType<ConsoleLogger>().Keyed<ILogger>("Solution");
            builder.RegisterType<FileLogger>().Keyed<ILogger>("Other");
            builder.RegisterType<SolutionExplorerMixed>().WithAttributeFilter();

            var container = builder.Build();

            var allAdapters = container.Resolve<IEnumerable<IAdapter>>().Count();
            Assert.Equal(3, allAdapters);

            var explorer = container.Resolve<SolutionExplorerMixed>();
            Assert.Equal(2, explorer.Adapters.Count);
            Assert.IsType<ConsoleLogger>(explorer.Logger);
        }

        [Fact]
        public void verify_key_filter_is_applied_on_constructor_dependency_single()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().Keyed<ILogger>("Solution");
            builder.RegisterType<FileLogger>().Keyed<ILogger>("Other");
            builder.RegisterType<SolutionExplorerKeyed>().WithAttributeFilter();

            var container = builder.Build();
            var explorer = container.Resolve<SolutionExplorerKeyed>();

            Assert.IsType<ConsoleLogger>(explorer.Logger);
        }

        [Fact]
        public void verify_key_filter_is_applied_on_constructor_dependency_multiple()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MsBuildAdapter>().Keyed<IAdapter>("Solution");
            builder.RegisterType<DteAdapter>().Keyed<IAdapter>("Solution");
            builder.RegisterType<ToolWindowAdapter>().Keyed<IAdapter>("Other");
            builder.RegisterType<SolutionExplorerKeyed>().WithAttributeFilter();

            var container = builder.Build();

            var otherAdapters = container.ResolveKeyed<IEnumerable<IAdapter>>("Other").Count();

            Assert.Equal(1, otherAdapters);

            var explorer = container.Resolve<SolutionExplorerKeyed>();

            Assert.Equal(2, explorer.Adapters.Count);
        }

        [Fact]
        public void verify_key_filter_is_applied_on_constructor_dependency_single_with_lazy()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().Keyed<ILogger>("Manager");
            builder.RegisterType<FileLogger>().Keyed<ILogger>("Other");
            builder.RegisterType<ManagerWithLazySingle>().WithAttributeFilter();

            var container = builder.Build();
            var resolved = container.Resolve<ManagerWithLazySingle>();

            Assert.IsType<ConsoleLogger>(resolved.Logger.Value);
        }

        [Fact]
        public void verify_key_filter_is_applied_on_constructor_dependency_single_with_meta()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().Keyed<ILogger>("Manager");
            builder.RegisterType<FileLogger>().Keyed<ILogger>("Other");
            builder.RegisterType<ManagerWithMetaSingle>().WithAttributeFilter();

            var container = builder.Build();
            var resolved = container.Resolve<ManagerWithMetaSingle>();

            Assert.IsType<ConsoleLogger>(resolved.Logger.Value);
        }

        [Fact]
        public void verify_key_filter_is_applied_on_constructor_dependency_single_with_owned()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().Keyed<ILogger>("Manager");
            builder.RegisterType<FileLogger>().Keyed<ILogger>("Other");
            builder.RegisterType<ManagerWithOwnedSingle>().WithAttributeFilter();

            var container = builder.Build();
            var resolved = container.Resolve<ManagerWithOwnedSingle>();

            Assert.IsType<ConsoleLogger>(resolved.Logger.Value);
        }

        [Fact]
        public void verify_key_filter_is_applied_on_constructor_dependency_many_with_lazy()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().Keyed<ILogger>("Manager");
            builder.RegisterType<SqlLogger>().Keyed<ILogger>("Manager");
            builder.RegisterType<FileLogger>().Keyed<ILogger>("Other");
            builder.RegisterType<ManagerWithLazyMany>().WithAttributeFilter();

            var container = builder.Build();
            var resolved = container.Resolve<ManagerWithLazyMany>();

            Assert.Equal(2, resolved.Loggers.Count());
        }

        [Fact]
        public void verify_key_filter_is_applied_on_constructor_dependency_many_with_meta()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().Keyed<ILogger>("Manager");
            builder.RegisterType<SqlLogger>().Keyed<ILogger>("Manager");
            builder.RegisterType<FileLogger>().Keyed<ILogger>("Other");
            builder.RegisterType<ManagerWithMetaMany>().WithAttributeFilter();

            var container = builder.Build();
            var resolved = container.Resolve<ManagerWithMetaMany>();

            Assert.Equal(2, resolved.Loggers.Count());
        }

        [Fact]
        public void verify_key_filter_is_applied_on_constructor_dependency_many_with_owned()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().Keyed<ILogger>("Manager");
            builder.RegisterType<SqlLogger>().Keyed<ILogger>("Manager");
            builder.RegisterType<FileLogger>().Keyed<ILogger>("Other");
            builder.RegisterType<ManagerWithOwnedMany>().WithAttributeFilter();

            var container = builder.Build();
            var resolved = container.Resolve<ManagerWithOwnedMany>();

            Assert.Equal(2, resolved.Loggers.Count());
        }

        [Fact]
        public void verify_metadata_filter_is_applied_on_constructor_dependency_single()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AttributedMetadataModule());
            builder.RegisterType<ConsoleLogger>().WithMetadata("LoggerName", "Solution").As<ILogger>();
            builder.RegisterType<FileLogger>().WithMetadata("LoggerName", "Other").As<ILogger>();
            builder.RegisterType<SolutionExplorerMetadata>().WithAttributeFilter();

            var container = builder.Build();
            var explorer = container.Resolve<SolutionExplorerMetadata>();

            Assert.IsType<ConsoleLogger>(explorer.Logger);
        }

        [Fact]
        public void verify_metadata_filter_is_applied_on_constructor_dependency_multiple()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AttributedMetadataModule());
            builder.RegisterType<MsBuildAdapter>().As<IAdapter>();
            builder.RegisterType<DteAdapter>().As<IAdapter>();
            builder.RegisterType<ToolWindowAdapter>().As<IAdapter>();
            builder.RegisterType<SolutionExplorerMetadata>().WithAttributeFilter();

            var container = builder.Build();

            var allAdapters = container.Resolve<IEnumerable<IAdapter>>().Count();

            Assert.Equal(3, allAdapters);

            var explorer = container.Resolve<SolutionExplorerMetadata>();

            Assert.Equal(2, explorer.Adapters.Count);
        }

        [Fact]
        public void verify_components_that_are_not_used_do_not_get_activated()
        {
            int adapterActivationCount = 0;
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AttributedMetadataModule());
            builder.RegisterType<MsBuildAdapter>().As<IAdapter>().OnActivating(h => adapterActivationCount++);
            builder.RegisterType<DteAdapter>().As<IAdapter>().OnActivating(h => adapterActivationCount++);
            builder.RegisterType<ToolWindowAdapter>().As<IAdapter>().OnActivating(h => adapterActivationCount++);
            builder.RegisterType<SolutionExplorerMetadata>().WithAttributeFilter();

            var container = builder.Build();
            container.Resolve<SolutionExplorerMetadata>();
            Assert.Equal(2, adapterActivationCount);
        }

        public interface ILogger
        {
        }

        public class ConsoleLogger : ILogger
        {
        }

        public class FileLogger : ILogger
        {
        }

        public class SqlLogger : ILogger
        {
        }

        public interface IAdapter
        {
        }

        [Adapter("Solution")]
        public class MsBuildAdapter : IAdapter
        {
        }

        [Adapter("Solution")]
        public class DteAdapter : IAdapter
        {
        }

        [Adapter("Other")]
        public class ToolWindowAdapter : IAdapter
        {
        }

        public class ManagerWithLazySingle
        {
            public ManagerWithLazySingle([WithKey("Manager")] Lazy<ILogger> logger)
            {
                this.Logger = logger;
            }

            public Lazy<ILogger> Logger { get; set; }
        }

        public class ManagerWithMetaSingle
        {
            public ManagerWithMetaSingle([WithKey("Manager")] Meta<ILogger, EmptyMetadataAttribute> logger)
            {
                this.Logger = logger;
            }

            public Meta<ILogger, EmptyMetadataAttribute> Logger { get; set; }
        }

        public class ManagerWithOwnedSingle
        {
            public ManagerWithOwnedSingle([WithKey("Manager")] Owned<ILogger> logger)
            {
                this.Logger = logger;
            }

            public Owned<ILogger> Logger { get; set; }
        }

        public class ManagerWithLazyMany
        {
            public ManagerWithLazyMany([WithKey("Manager")] IEnumerable<Lazy<ILogger>> loggers)
            {
                this.Loggers = loggers;
            }

            public IEnumerable<Lazy<ILogger>> Loggers { get; set; }
        }

        public class ManagerWithMetaMany
        {
            public ManagerWithMetaMany([WithKey("Manager")] IEnumerable<Meta<ILogger, EmptyMetadataAttribute>> loggers)
            {
                this.Loggers = loggers;
            }

            public IEnumerable<Meta<ILogger, EmptyMetadataAttribute>> Loggers { get; set; }
        }

        public class ManagerWithOwnedMany
        {
            public ManagerWithOwnedMany([WithKey("Manager")] IEnumerable<Owned<ILogger>> loggers)
            {
                this.Loggers = loggers;
            }

            public IEnumerable<Owned<ILogger>> Loggers { get; set; }
        }

        public class SolutionExplorerKeyed
        {
            public SolutionExplorerKeyed(
            [WithKey("Solution")] IEnumerable<IAdapter> adapters,
            [WithKey("Solution")] ILogger logger)
            {
                this.Adapters = adapters.ToList();
                this.Logger = logger;
            }

            public List<IAdapter> Adapters { get; set; }

            public ILogger Logger { get; set; }
        }

        public class SolutionExplorerMetadata
        {
            public SolutionExplorerMetadata(
            [WithMetadata("Target", "Solution")] IEnumerable<IAdapter> adapters,
            [WithMetadata("LoggerName", "Solution")] ILogger logger)
            {
                this.Adapters = adapters.ToList();
                this.Logger = logger;
            }

            public List<IAdapter> Adapters { get; set; }

            public ILogger Logger { get; set; }
        }

        public class SolutionExplorerMixed
        {
            public SolutionExplorerMixed(
            [WithMetadata("Target", "Solution")] IEnumerable<IAdapter> adapters,
            [WithKey("Solution")] ILogger logger)
            {
                this.Adapters = adapters.ToList();
                this.Logger = logger;
            }

            public List<IAdapter> Adapters { get; set; }

            public ILogger Logger { get; set; }
        }

        [MetadataAttribute]
        public class AdapterAttribute : Attribute
        {
            public AdapterAttribute(string target)
            {
                this.Target = target;
            }

            public AdapterAttribute(IDictionary<string, object> metadata)
            {
                this.Target = (string)metadata["Target"];
            }

            public string Target { get; set; }
        }

        [MetadataAttribute]
        public class EmptyMetadataAttribute : Attribute
        {
        }
    }
}