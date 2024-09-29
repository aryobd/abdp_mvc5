using abdp.Data;
using abdp.Data.Infrastructure;
using abdp.Data.IRepository;
using abdp.Data.Repository;

//using Dsf.Olss.Data;
//using Dsf.Olss.Data.Infrastructure;

using abdp.Service.IServices;
using abdp.Service.Services;

using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace abdp.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IDatabaseFactory, DatabaseFactory>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            //container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //container.RegisterType<IHubActivator, HubActivator>();

            container.RegisterType<IMaintenanceCategoryRepository, MaintenanceCategoryRepository>();
            container.RegisterType<IMaintenanceItemRepository, MaintenanceItemRepository>();

            container.RegisterType<IMaintenanceCategoryService, MaintenanceCategoryService>();
            container.RegisterType<IMaintenanceItemService, MaintenanceItemService>();

            container.RegisterType<IEmployeeService, EmployeeService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
