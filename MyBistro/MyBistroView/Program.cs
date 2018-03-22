using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using MyBistroService.ImplementationsList;
using MyBistroService.Interfaces;

namespace MyBistroView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
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
