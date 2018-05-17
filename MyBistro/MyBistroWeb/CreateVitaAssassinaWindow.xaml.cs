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
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IАcquirenteService serviceA;

        private readonly ISnackService serviceS;

        private readonly IMainService serviceM;
        public CreateVitaAssassinaWindow(IАcquirenteService serviceA, ISnackService serviceS, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceA = serviceA;
            this.serviceS = serviceS;
            this.serviceM = serviceM;
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
                    SnackViewModels snack = serviceS.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSumm.Text = (count * snack.Price).ToString();
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
                List<АcquirenteViewModels> listA = serviceA.GetList();
                if (listA != null)
                {
                    comboBoxAcquirente.DisplayMemberPath = "АcquirenteFIO";
                    comboBoxAcquirente.SelectedValuePath = "Id";
                    comboBoxAcquirente.ItemsSource = listA;
                    comboBoxAcquirente.SelectedItem = null;
                }
                List<SnackViewModels> listS = serviceS.GetList();
                if (listS != null)
                {
                    comboBoxSnack.DisplayMemberPath = "SnackName";
                    comboBoxSnack.SelectedValuePath = "Id";
                    comboBoxSnack.ItemsSource = listS;
                    comboBoxSnack.SelectedItem = null;
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
                serviceM.CreateVitaAssassina(new VitaAssassinaBindingModels
                {
                    АcquirenteId = Convert.ToInt32(comboBoxAcquirente.SelectedValue),
                    SnackId = Convert.ToInt32(comboBoxSnack.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSumm.Text)

                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
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

        private void comboBoxSnack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalcSum();
        }

        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcSum();
        }
    }
}
