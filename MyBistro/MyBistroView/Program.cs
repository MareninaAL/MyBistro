using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using MyBistroService.ImplementationsList;
using MyBistroService.Interfaces;
using MyBistroService;
using System.Data.Entity;
using MyBistroService.ImplementationsBD;

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
            currentContainer.RegisterType<DbContext, BistroDbContext>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IАcquirenteService, AcquirenteServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IConstituentService, ConstituentServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICuocoService, CuocoServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISnackService, SnackServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRefrigeratorService, RefrigeratorServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceBD>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IReportService, ReportServiceBD>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
