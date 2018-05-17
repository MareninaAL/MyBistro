using MyBistroService.ImplementationsList;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            currentContainer.RegisterType<IАcquirenteService, АcquirenteServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IConstituentService, ConstituentServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICuocoService, CuocoServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISnackService, SnackServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRefrigeratorService, RefrigeratorServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

    }
}
