using System;
using Autofac;
using mobile.Interfaces;
using mobile.Repository;
using mobile.Services;
using mobile.Services.Data;
using mobile.ViewModels;

namespace mobile.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<HomePageViewModel>();
            builder.RegisterType<MainPageViewModel>();
            builder.RegisterType<ChildrenPageModelView>();
            builder.RegisterType<ProfilePageModelView>();
            builder.RegisterType<TripsDetailPageModelView>();
            builder.RegisterType<TripsPageModelView>();


            //services - data
            builder.RegisterType<ChildDataService>().As<IChildDataService>();


            //services - general
            builder.RegisterType<NavigationService>().As<INavigationService>();

            //General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
