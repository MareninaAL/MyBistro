using MyBistroService;
using MyBistroService.ImplementationsBD;
using MyBistroService.ImplementationsList;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace BistroWeb
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    { /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            var application = new App();
            application.Run(container.Resolve<MainWindow>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, BistroDbContext>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IАcquirenteService, AcquirenteServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IConstituentService, ConstituentServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICuocoService, CuocoServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISnackService, SnackServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRefrigeratorService, RefrigeratorServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceBD>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

    }
}
