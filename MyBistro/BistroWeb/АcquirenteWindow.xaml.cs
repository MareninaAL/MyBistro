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
    /// Логика взаимодействия для АcquirenteWindow.xaml
    /// </summary>
    public partial class АcquirenteWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IАcquirenteService service;

        private int? id;
        public АcquirenteWindow(IАcquirenteService service)
        {
            InitializeComponent();
            this.service = service;
            Loaded += Аcquirente_Load;
        }

        private void Аcquirente_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    АcquirenteViewModels view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxАcquirenteFIO.Text = view.АcquirenteFIO;
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
            if (string.IsNullOrEmpty(textBoxАcquirenteFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new АcquirenteBindingModels
                    {
                        Id = id.Value,
                        АcquirenteFIO = textBoxАcquirenteFIO.Text
                    });
                }
                else
                {
                    service.AddElement(new АcquirenteBindingModels
                    {
                        АcquirenteFIO = textBoxАcquirenteFIO.Text
                    });
                }
                MessageBox.Show("Cохранение прошло уCпешно", "Cообщение", MessageBoxButton.OK);
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
