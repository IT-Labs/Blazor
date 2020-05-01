using BlazorApp.Shared.ESB;
using BlazorApp.Shared.Managers;
using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Validation;
using Core.Framework.Repository;
using Lamar.Scanning.Conventions;
using IContainer = BlazorApp.Shared.Validation.IContainer;
using IContext = BlazorApp.Shared.Repository.IContext;

namespace Core.Framework
{
    public class DefaultRegistry : Lamar.ServiceRegistry
    {
        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.AssembliesFromApplicationBaseDirectory(x => x.GetName().Name.StartsWith("BlazorApp") || x.GetName().Name.StartsWith("Core"));
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new FindAllTypesFilter(typeof(IContainer)));
                    scan.With(new FindAllTypesFilter(typeof(IContext)));
                    scan.With(new FindAllTypesFilter(typeof(IFileManager)));
                    scan.With(new FindAllTypesFilter(typeof(DomainRepository)));
                    scan.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IMap<,>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IConsume<>));
                });

            For<IContainer>().Use<InfrastructureContainer>();
        }
    }
}