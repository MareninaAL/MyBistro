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
    /// Логика взаимодействия для CuocoWindow.xaml
    /// </summary>
    public partial class CuocoWindow : Window
    {

        public int Id { set { id = value; } }

        private int? id;
        public CuocoWindow()
        {
            InitializeComponent();
            Loaded += Cuoco_Load;
        }

        private void Cuoco_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Cuoco/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var cuoco = APIClient.GetElement<CuocoViewModels>(response);
                        textBoxCuocoFIO.Text = cuoco.CuocoFIO;
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
            if (string.IsNullOrEmpty(textBoxCuocoFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Cuoco/UpdElement", new CuocoBindingModels
                    {
                        Id = id.Value,
                        CuocoFIO = textBoxCuocoFIO.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Cuoco/AddElement", new CuocoBindingModels
                    {
                        CuocoFIO = textBoxCuocoFIO.Text
                    });
                }
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cохранение прошло уCпешно", "Cообщение", MessageBoxButton.OK);
                    DialogResult = true;
                    Close();
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

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
