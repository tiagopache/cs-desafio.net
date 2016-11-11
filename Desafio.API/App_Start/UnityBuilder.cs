using Desafio.Application.Contract.Contracts;
using Desafio.Application.Service;
using Desafio.Business.Contract;
using Desafio.BusinessService;
using Desafio.Infrastructure.Data;
using Desafio.Infrastructure.Data.Contexts;
using Desafio.Infrastructure.Data.Repositories;
using Desafio.Infrastructure.DependencyInjection;
using Desafio.Model.Contexts;
using Microsoft.Practices.Unity;

namespace Desafio.API.App_Start
{
    public static class UnityBuilder
    {
        public static void Build(IUnityContainer container)
        {
            InjectFactory.SetContainer(container);

            buildContext(container, new HierarchicalLifetimeManager());
            buildInfrastructure(container, new HierarchicalLifetimeManager());
            buildBusinessServices(container, new HierarchicalLifetimeManager());
            buildApplicationServices(container, new HierarchicalLifetimeManager());
        }

        private static void buildApplicationServices(IUnityContainer container, LifetimeManager lifetimeManager)
        {
            container.RegisterType<IUsuarioApplicationService, UsuarioApplicationService>(lifetimeManager);
        }

        private static void buildBusinessServices(IUnityContainer container, LifetimeManager lifetimeManager)
        {
            container.RegisterType<IUsuarioBusinessService, UsuarioBusinessService>(lifetimeManager);
            container.RegisterType<ITelefoneBusinessService, TelefoneBusinessService>(new HierarchicalLifetimeManager());
        }

        private static void buildInfrastructure(IUnityContainer container, LifetimeManager lifetimeManager)
        {
            container.RegisterType(typeof(IUnitOfWork<>), typeof(UnitOfWork<>), lifetimeManager);
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>), lifetimeManager);
        }

        private static void buildContext(IUnityContainer container, LifetimeManager lifetimeManager)
        {
            container.RegisterType<IDbContext, DesafioDbContext>(lifetimeManager, new InjectionConstructor("DesafioDbContext"));
        }
    }
}