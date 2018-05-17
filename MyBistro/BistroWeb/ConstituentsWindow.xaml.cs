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
    /// Логика взаимодействия для ConstituentsWindow.xaml
    /// </summary>
    public partial class ConstituentsWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IConstituentService service;
        public ConstituentsWindow(IConstituentService service)
        {
            InitializeComponent();
            this.service = service;
            Loaded += Constituents_Load;
        }
        private void Constituents_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<ConstituentViewModels> list = service.GetList();
                if (list != null)
                {
                    dataGridConstituent.ItemsSource = list;
                    dataGridConstituent.Columns[0].Visibility = Visibility.Hidden;
                    dataGridConstituent.Columns[1].Width = dataGridConstituent.Width - 8;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<ConstituentWindow>();
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
            if (dataGridConstituent.SelectedItem != null)
            {
                var form = Container.Resolve<ConstituentWindow>();
                form.Id = ((ConstituentViewModels)dataGridConstituent.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridConstituent.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((ConstituentViewModels)dataGridConstituent.SelectedItem).Id;
                    try
                    {
                        service.DelElement(id);
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
