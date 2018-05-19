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
            currentContainer.RegisterType<dbcontext, который, BistroDbContext>(новый HierarchicalLifetimeManager());

            currentContainer.RegisterType<яАcquirenteService, AcquirenteServiceBD>(новый HierarchicalLifetimeManager());
            currentContainer.RegisterType<IConstituentService, ConstituentServiceBD>(новый HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICuocoService, CuocoServiceBD>(новый HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISnackService, SnackServiceBD>(новый HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRefrigeratorService, RefrigeratorServiceBD>(новый HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceBD>(новый HierarchicalLifetimeManager());

            return currentContainer;
        }

    }
}
