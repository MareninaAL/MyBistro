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
    /// Логика взаимодействия для CreateVitaAssassinaWindow.xaml
    /// </summary>
    public partial class CreateVitaAssassinaWindow : Window
    {
        public CreateVitaAssassinaWindow()
        {
            InitializeComponent();
            Loaded += CreateVitaAssassinaWindow_Load;
            comboBoxSnack.SelectionChanged += comboBoxSnack_SelectionChanged;
            comboBoxSnack.SelectionChanged += new SelectionChangedEventHandler(comboBoxSnack_SelectionChanged);
        }

        private void CalcSum()
        {
            if (comboBoxSnack.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxSnack.SelectedValue);
                    var responseP = APIClient.GetRequest("api/Snack/Get/" + id);
                    if (responseP.Result.IsSuccessStatusCode)
                    {
                        SnackViewModels snack = APIClient.GetElement<SnackViewModels>(responseP);
                        int count = Convert.ToInt32(textBoxCount.Text);
                        textBoxSumm.Text = (count * (int)snack.Price).ToString();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(responseP));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
                }
            }
        }

        private void CreateVitaAssassinaWindow_Load(object sender, EventArgs e)
        {
            try
            {
                var responseC = APIClient.GetRequest("api/Аcquirente/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<АcquirenteViewModels> list = APIClient.GetElement<List<АcquirenteViewModels>>(responseC);
                    if (list != null)
                    {
                        comboBoxAcquirente.DisplayMemberPath = "AcquirenteFIO";
                        comboBoxAcquirente.SelectedValuePath = "Id";
                        comboBoxAcquirente.ItemsSource = list;
                        comboBoxAcquirente.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseP = APIClient.GetRequest("api/Snack/GetList");
                if (responseP.Result.IsSuccessStatusCode)
                {
                    List<SnackViewModels> list = APIClient.GetElement<List<SnackViewModels>>(responseP);
                    if (list != null)
                {
                    comboBoxSnack.DisplayMemberPath = "SnackName";
                    comboBoxSnack.SelectedValuePath = "Id";
                    comboBoxSnack.ItemsSource = list;
                    comboBoxSnack.SelectedItem = null;
                }
            }  else
                {
                throw new Exception(APIClient.GetError(responseC));
            }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (comboBoxAcquirente.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (comboBoxSnack.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButton.OK);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Main/CreateVitaAssassina", new VitaAssassinaBindingModels
                {
                    АcquirenteId = Convert.ToInt32(comboBoxAcquirente.SelectedValue),
                    SnackId = Convert.ToInt32(comboBoxSnack.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSumm.Text)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

     

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcSum();
        }

        private void comboBoxSnack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalcSum();
        }
    }
}
