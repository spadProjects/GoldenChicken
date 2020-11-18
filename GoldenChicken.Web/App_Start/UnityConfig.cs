using System.Web.Mvc;
using GoldenChicken.Infrastructure.Repositories;
using GoldenChicken.Web.Controllers;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace GoldenChicken.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<ArticleCategoriesRepository>();
            container.RegisterType<ArticlesRepository>();
            container.RegisterType<UsersRepository>();
            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}