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
    /// Логика взаимодействия для SnacksWindow.xaml
    /// </summary>
    public partial class SnacksWindow : Window
    {
        public SnacksWindow()
        {
            InitializeComponent();
            Loaded += Snacks_Load;
        }

        private void Snacks_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var response = APIClient.GetRequest("api/Snack/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<SnackViewModels> list = APIClient.GetElement<List<SnackViewModels>>(response);
                    if (list != null)
                    {
                        dataGridSnack.ItemsSource = list;
                        dataGridSnack.Columns[0].Visibility = Visibility.Hidden;
                        dataGridSnack.Columns[1].Width = DataGridLength.Auto;
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

        private void buttonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = new SnackWindow();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSnack.SelectedItem != null)
            {
                var form = new SnackWindow();
                form.Id = ((SnackViewModels)dataGridSnack.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSnack.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int id = ((SnackViewModels)dataGridSnack.SelectedItem).Id;
                    try
                    {
                        var response = APIClient.PostRequest("api/Snack/DelElement", new SnackBindingModels { Id = id });
                        if (!response.Result.IsSuccessStatusCode)
                        {
                            throw new Exception(APIClient.GetError(response));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK );
                    }
                    LoadData();
                }
            }
        }
    }
}
