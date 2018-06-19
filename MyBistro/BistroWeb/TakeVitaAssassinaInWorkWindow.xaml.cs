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
    /// Логика взаимодействия для TakeVitaAssassinaInWorkWindow.xaml
    /// </summary>
    public partial class TakeVitaAssassinaInWorkWindow : Window
    {

        public int Id { set { id = value; } }

        private int? id;

        public TakeVitaAssassinaInWorkWindow()
        {
            InitializeComponent();
            Loaded += TakeVitaAssassinaInWorkWindow_Load;
        }

        private void TakeVitaAssassinaInWorkWindow_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButton.OK);
                    Close();
                }
                var response = APIClient.GetRequest("api/Cuoco/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<CuocoViewModels> list = APIClient.GetElement<List<CuocoViewModels>>(response);
                    if (list != null)
                    {
                        comboBoxCuocoName.DisplayMemberPath = "CuocoFIO";
                        comboBoxCuocoName.SelectedValuePath = "Id";
                        comboBoxCuocoName.ItemsSource = list;
                        comboBoxCuocoName.SelectedItem = null;
                    }
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

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxCuocoName.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Main/TakeVitaAssassinarInWork", new VitaAssassinaBindingModels
                {
                    Id = id.Value,
                    CuocoId = Convert.ToInt32(comboBoxCuocoName.SelectedValue)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK);
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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
