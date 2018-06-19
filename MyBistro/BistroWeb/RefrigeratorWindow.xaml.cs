using MyBistro.BindingModels;
using MyBistro.ViewModels;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для RefrigeratorWindow.xaml
    /// </summary>
    public partial class RefrigeratorWindow : Window
    {

        public int Id { set { id = value; } }

        private int? id;

        public RefrigeratorWindow()
        {
            InitializeComponent();
            Loaded += Refrigerator_Load;
        }

        private void Refrigerator_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Refrigerator/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var refrigerator = APIClient.GetElement<RefrigeratorViewModels>(response);
                        textBoxRefrigeratorName.Text = refrigerator.RefrigeratorName;
                        dataGridConstituents.ItemsSource = refrigerator.RefrigeratorConstituent;
                        dataGridConstituents.Columns[0].Visibility = Visibility.Hidden;
                        dataGridConstituents.Columns[1].Visibility = Visibility.Hidden;
                        dataGridConstituents.Columns[2].Visibility = Visibility.Hidden;
                        dataGridConstituents.Columns[3].Width = dataGridConstituents.Width - 8;
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
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRefrigeratorName.Text))
            {
                MessageBox.Show("Заполните Название", "Ошибка", MessageBoxButton.OK);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Refrigerator/UpdElement", new RefrigeratorBindingModels
                    {
                        Id = id.Value,
                        RefrigeratorName = textBoxRefrigeratorName.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Refrigerator/AddElement", new RefrigeratorBindingModels
                    {
                        RefrigeratorName = textBoxRefrigeratorName.Text
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
