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
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICuocoService serviceС;

        private readonly IMainService serviceM;

        private int? id;

        public TakeVitaAssassinaInWorkWindow(ICuocoService serviceС, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceС = serviceС;
            this.serviceM = serviceM;
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
                List<CuocoViewModels> listС = serviceС.GetList();
                if (listС != null)
                {
                    comboBoxCuocoName.DisplayMemberPath = "CuocoFIO";
                    comboBoxCuocoName.SelectedValuePath = "Id";
                    comboBoxCuocoName.ItemsSource = listС;
                    comboBoxCuocoName.SelectedItem = null;
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
                serviceM.TakeVitaAssassinarInWork(new VitaAssassinaBindingModels
                {
                    Id = id.Value,
                    CuocoId = Convert.ToInt32(comboBoxCuocoName.SelectedValue)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK);
                DialogResult = true;
                Close();
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
