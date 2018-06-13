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
    /// Логика взаимодействия для CuocoWindow.xaml
    /// </summary>
    public partial class CuocoWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICuocoService service;

        private int? id;
        public CuocoWindow(ICuocoService service)
        {
            InitializeComponent();
            this.service = service;
            Loaded += Cuoco_Load;
        }

        private void Cuoco_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CuocoViewModels view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxCuocoFIO.Text = view.CuocoFIO;
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
                if (id.HasValue)
                {
                    service.UpdElement(new CuocoBindingModels
                    {
                        Id = id.Value,
                        CuocoFIO = textBoxCuocoFIO.Text
                    });
                }
                else
                {
                    service.AddElement(new CuocoBindingModels
                    {
                        CuocoFIO = textBoxCuocoFIO.Text
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
