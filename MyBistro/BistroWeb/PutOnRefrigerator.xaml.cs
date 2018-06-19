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

namespace BistroWeb
{
    /// <summary>
    /// Логика взаимодействия для PutOnRefrigerator.xaml
    /// </summary>
    public partial class PutOnRefrigerator : Window
    {
        public PutOnRefrigerator()
        {
            InitializeComponent();
            Loaded += FormPutOnRefrigerator_Load;
        }

        private void FormPutOnRefrigerator_Load(object sender, EventArgs e)
        {
            try
            {
                var responseC = APIClient.GetRequest("api/Constituent/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<ConstituentViewModels> list = APIClient.GetElement<List<ConstituentViewModels>>(responseC);
                    if (list != null)
                    {
                        comboBoxConstituent.DisplayMemberPath = "ConstituentName";
                        comboBoxConstituent.SelectedValuePath = "Id";
                        comboBoxConstituent.ItemsSource = list;
                        comboBoxConstituent.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseS = APIClient.GetRequest("api/Refrigerator/GetList");
                if (responseS.Result.IsSuccessStatusCode)
                {
                    List<RefrigeratorViewModels> list = APIClient.GetElement<List<RefrigeratorViewModels>>(responseS);
                    if (list != null)
                    {
                        comboBoxRefrigerator.DisplayMemberPath = "RefrigeratorName";
                        comboBoxRefrigerator.SelectedValuePath = "Id";
                        comboBoxRefrigerator.ItemsSource = list;
                        comboBoxRefrigerator.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseS));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK );
            }
        }

            private void button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количеcтво", "Ошибка", MessageBoxButton.OK );
                return;
            }
            if (comboBoxConstituent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (comboBoxRefrigerator.SelectedValue == null)
            {
                MessageBox.Show("Выберите Cклад", "Ошибка", MessageBoxButton.OK );
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Main/PutConstituentOnRefrigerator", new RefrigeratorConstituentBindingModels
                {
                    ConstituentId = Convert.ToInt32(comboBoxConstituent.SelectedValue),
                    RefrigeratorId = Convert.ToInt32(comboBoxRefrigerator.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cохранение прошло уcпешно", "Cообщение", MessageBoxButton.OK);
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
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK );
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
