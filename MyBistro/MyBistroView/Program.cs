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
            APIAcquirente.Connect();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

    }
}
