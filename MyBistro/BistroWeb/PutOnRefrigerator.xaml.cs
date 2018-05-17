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
        [Unity.Attributes.Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IRefrigeratorService serviceS;

        private readonly IConstituentService serviceC;

        private readonly IMainService serviceM;
        public PutOnRefrigerator(IRefrigeratorService serviceS, IConstituentService serviceC, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
            Loaded += FormPutOnRefrigerator_Load;
        }

        private void FormPutOnRefrigerator_Load(object sender, EventArgs e)
        {
            try
            {
                List<ConstituentViewModels> listC = serviceC.GetList();
                if (listC != null)
                {
                     comboBoxConstituent.DisplayMemberPath = "ConstituentName";
                     comboBoxConstituent.SelectedValuePath = "Id";
                     comboBoxConstituent.ItemsSource = listC;
                     comboBoxConstituent.SelectedItem = null; 
                }
                List<RefrigeratorViewModels> listS = serviceS.GetList();
                if (listS != null)
                {
                     comboBoxRefrigerator.DisplayMemberPath = "RefrigeratorName";
                     comboBoxRefrigerator.SelectedValuePath = "Id";
                     comboBoxRefrigerator.ItemsSource  = listS;
                     comboBoxRefrigerator.SelectedItem = null; 
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
                serviceM.PutConstituentOnRefrigerator(new RefrigeratorConstituentBindingModels
                {
                    ConstituentId = Convert.ToInt32(comboBoxConstituent.SelectedValue),
                    RefrigeratorId = Convert.ToInt32(comboBoxRefrigerator.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });  
                MessageBox.Show("Cохранение прошло уcпешно", "Cообщение", MessageBoxButton.OK );
                DialogResult = true;
                Close();
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
