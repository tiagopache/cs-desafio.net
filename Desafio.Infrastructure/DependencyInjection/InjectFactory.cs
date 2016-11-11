using Microsoft.Practices.Unity;

namespace Desafio.Infrastructure.DependencyInjection
{
    public static class InjectFactory
    {
        private static IUnityContainer _container { get; set; }

        public static T Resolve<T>()
        {
            if (_container == null)
                return default(T);

            return _container.Resolve<T>();
        }

        public static T Resolve<T>(string namedInstance)
        {
            if (_container == null)
                return default(T);

            return _container.Resolve<T>(namedInstance);
        }

        public static void SetContainer(IUnityContainer container)
        {
            _container = container;
        }
    }
}
