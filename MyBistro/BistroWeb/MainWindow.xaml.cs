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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                var response = APIClient.GetRequest("api/Main/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<VitaAssassinaViewModels> list = APIClient.GetElement<List<VitaAssassinaViewModels>>(response);
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
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var form = new PutOnRefrigerator();
            form.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var form = new AcquirentesWindow();
            form.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var form = new CuocosWindow();
            form.ShowDialog();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            var form = new SnacksWindow();
            form.ShowDialog();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            var form = new ConstituentsWindow();
            form.ShowDialog();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            var form = new RefrigeratorsWindow();
            form.ShowDialog();
        }

        private void buttonCreateVitaAssassina_Click(object sender, RoutedEventArgs e)
        {
            var form = new CreateVitaAssassinaWindow();
            form.ShowDialog();
            LoadData();
        }
        
        private void buttonTakeVitaAssassinaInWork_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var form = new TakeVitaAssassinaInWorkWindow();
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
                    var response = APIClient.PostRequest("api/Main/FinishVitaAssassina", new VitaAssassinaViewModels
                    {
                        Id = id
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        LoadData();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(response));
                    }
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
                    var response = APIClient.PostRequest("api/Main/PayVitaAssassina", new VitaAssassinaViewModels
                    {
                        Id = id
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        LoadData();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(response));
                    }
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
                    var response = APIClient.PostRequest("api/Report/SaveRefregiratorsLoad", new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(response));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        { 
            var form = new AcquirenteVitaAssassinasWindow();
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
                    
                    var response = APIClient.PostRequest("api/Report/SaveSnackPrice", new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                    else
                    {
                        throw new Exception(APIClient.GetError(response));
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
