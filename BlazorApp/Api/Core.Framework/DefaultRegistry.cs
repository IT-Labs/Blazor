using Core.Shared.ESB;
using Core.Shared.Managers;
using Core.Shared.Repository;
using Core.Shared.Validation;
using Core.Framework.Repository;
using FluentValidation;
using Lamar.Scanning.Conventions;
using IContainer = Core.Shared.Validation.IContainer;
using IContext = Core.Shared.Repository.IContext;

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
                    //scan.ConnectImplementationsToTypesClosing(typeof(Core.Shared.Validation.IValidator<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IMap<,>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IConsume<>));
                });

            For<IContainer>().Use<InfrastructureContainer>();
        }
    }
}