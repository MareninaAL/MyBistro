using MyBistro.BindingModels;
using MyBistro.ViewModels;
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
using System.Windows.Shapes;
using Unity;
using Unity.Attributes;

namespace BistroWeb
{
    /// <summary>
    /// Логика взаимодействия для RefrigeratorsWindow.xaml
    /// </summary>
    public partial class RefrigeratorsWindow : Window
    {
        public RefrigeratorsWindow()
        {
            InitializeComponent();
            Loaded += Refrigerator_Load;
        }

        private void Refrigerator_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var response = APIClient.GetRequest("api/Refrigerator/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<RefrigeratorViewModels> list = APIClient.GetElement<List<RefrigeratorViewModels>>(response);
                    if (list != null)
                    {
                        dataGridRefrigerator.ItemsSource = list;
                        dataGridRefrigerator.Columns[0].Visibility = Visibility.Hidden;
                        dataGridRefrigerator.Columns[1].Width = DataGridLength.Auto;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK );
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = new RefrigeratorWindow();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void buttonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridRefrigerator.SelectedItem != null)
            {
                var form = new RefrigeratorWindow();
                form.Id = ((RefrigeratorViewModels)dataGridRefrigerator.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridRefrigerator.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int id = ((RefrigeratorViewModels)dataGridRefrigerator.SelectedItem).Id;
                    try
                    {
                        var response = APIClient.PostRequest("api/Refrigerator/DelElement", new RefrigeratorBindingModels { Id = id });
                        if (!response.Result.IsSuccessStatusCode)
                        {
                            throw new Exception(APIClient.GetError(response));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
                    }
                    LoadData();
                }
            }
        }
    }
}
