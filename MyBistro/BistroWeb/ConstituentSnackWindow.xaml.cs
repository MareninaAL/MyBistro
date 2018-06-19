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
    /// Логика взаимодействия для ConstituentSnackWindow.xaml
    /// </summary>
    public partial class ConstituentSnackWindow : Window
    {
       /* [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IConstituentService service; */
         
        public ConstituentSnackViewModels Model { set { model = value; } get { return model; } }

        private ConstituentSnackViewModels model;
        public ConstituentSnackWindow(/*IConstituentService service*/)
        {
            InitializeComponent();
            Loaded += ConstituentSnack_Load;
         //   this.service = service;
        }

        private void ConstituentSnack_Load(object sender, EventArgs e)
        {
            try
            {
                // List<ConstituentViewModels> list = service.GetList();
                var response = APIClient.GetRequest("api/Constituent/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    comboBoxConstituent.DisplayMemberPath = "ConstituentName";
                    comboBoxConstituent.SelectedValuePath = "Id";
                    comboBoxConstituent.ItemsSource = APIClient.GetElement<List<ConstituentViewModels>>(response);
                    comboBoxConstituent.SelectedItem = null;
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
            if (model != null)
            {
                comboBoxConstituent.IsEnabled = false;
                comboBoxConstituent.SelectedValue = model.ConstituentId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

      

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxConstituent.SelectedValue == null)
            {
                MessageBox.Show("Выберите ингредиент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new ConstituentSnackViewModels
                    {
                        ConstituentId = Convert.ToInt32(comboBoxConstituent.SelectedValue),
                        ConstituentName = comboBoxConstituent.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
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
