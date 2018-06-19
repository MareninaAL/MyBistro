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
    /// Логика взаимодействия для CuocosWindow.xaml
    /// </summary>
    public partial class CuocosWindow : Window
    {
        public CuocosWindow()
        {
            InitializeComponent();
            Loaded += Cuoco_Load; 
        }

        private void Cuoco_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var response = APIClient.GetRequest("api/Cuoco/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<CuocoViewModels> list = APIClient.GetElement<List<CuocoViewModels>>(response);
                    if (list != null)
                    {
                        dataGridCuoco.ItemsSource = list;
                        dataGridCuoco.Columns[0].Visibility = Visibility.Hidden;
                        dataGridCuoco.Columns[1].Width = dataGridCuoco.Width - 8;
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


        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = new CuocoWindow();
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
            if (dataGridCuoco.SelectedItem != null)
            {
                var form = new CuocoWindow();
                form.Id = ((CuocoViewModels)dataGridCuoco.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCuoco.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int id = ((CuocoViewModels)dataGridCuoco.SelectedItem).Id;
                    try
                    {
                        var response = APIClient.PostRequest("api/Cuoco/DelElement", new CuocoBindingModels { Id = id });
                        if (!response.Result.IsSuccessStatusCode)
                        {
                            throw new Exception(APIClient.GetError(response));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }
    }
}
