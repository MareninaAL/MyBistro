using Microsoft.Win32;
using MyBistro.ViewModels;
using MyBistroService.BindingModels;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;

namespace BistroWeb
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Unity.Attributes.Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IMainService service;
        private readonly IReportService reportService;

        public MainWindow(IMainService service, IReportService reportService)
        {
            InitializeComponent();
            this.reportService = reportService;
            this.service = service;
        }

        private void LoadData()
        {
            try
            {
                List<VitaAssassinaViewModels> list = service.GetList();
                if (list != null)
                {
                    dataGrid.ItemsSource = list;
                    dataGrid.Columns[0].Visibility = Visibility.Hidden;
                    dataGrid.Columns[1].Visibility = Visibility.Hidden;
                    dataGrid.Columns[3].Visibility = Visibility.Hidden;
                    dataGrid.Columns[5].Visibility = Visibility.Hidden;
                    dataGrid.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<PutOnRefrigerator>();
            form.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<AcquirentesWindow>();
            form.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<CuocosWindow>();
            form.ShowDialog();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<SnacksWindow>();
            form.ShowDialog();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<ConstituentsWindow>();
            form.ShowDialog();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<RefrigeratorsWindow>();
            form.ShowDialog();
        }

        private void buttonCreateVitaAssassina_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<CreateVitaAssassinaWindow>();
            form.ShowDialog();
            LoadData();
        }
        
        private void buttonTakeVitaAssassinaInWork_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var form = Container.Resolve<TakeVitaAssassinaInWorkWindow>();
                form.Id = ((VitaAssassinaViewModels)dataGrid.SelectedItem).Id;
                form.ShowDialog();
                LoadData();
            }
        }

        private void buttonVitaAssassinaReady_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                int id = ((VitaAssassinaViewModels)dataGrid.SelectedItem).Id;
                try
                {
                    service.FinishVitaAssassina(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonPayVitaAssassina_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                int id = ((VitaAssassinaViewModels)dataGrid.SelectedItem).Id;
                try
                {
                    service.PayVitaAssassina(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    reportService.SaveRefregiratorsLoad(new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        { 
            var form = Container.Resolve<AcquirenteVitaAssassinasWindow>();
            form.ShowDialog();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };

            if (sfd.ShowDialog() == true)
            {

                try
                {

                    reportService.SaveSnackPrice(new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
