using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Status.DAL;
using Status.Web;

namespace Maintenance.Web.Tests.Config
{
    public class Setup
    {
        private DbConnection _connnection;

        [TestInitialize]
        public void Init()
        {
            _connnection = Effort.DbConnectionFactory.CreateTransient();
            var context = new StatusContext(_connnection);
            SimpleInjectorConfig.CreateContainer(context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _connnection = null;
        }

        protected static TService GetService<TService>() where TService: class
        {
            return SimpleInjectorConfig.GetService<TService>();
        }
    }
}