using System.Data.Entity;
using SimpleInjector;
using Status.Core.Repositories;
using Status.Core.Services;
using Status.Core.Services.Implementation;
using Status.DAL;
using Status.DAL.Repositories;

namespace Status.Web
{
    public static class SimpleInjectorConfig
    {
        private static Container _container;

        public static Container CreateContainer(StatusContext context)
        {
            var container = new Container();

            Database.SetInitializer(new StatusContextInitializer());

            container.Register<IStateLogRepository>(() => new EfStateLogRepository(context), Lifestyle.Singleton);
            container.Register<IStatusService, DefaultStatusService>();

            container.Verify();

            _container = container;

            return _container;
        }

        
        public static TService GetService<TService>() where TService : class
        {
            return _container.GetInstance<TService>();
        }
    }

}