using MyBistro.BindingModels;
using MyBistro.ViewModels;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для SnackWindow.xaml
    /// </summary>
    public partial class SnackWindow : Window
    {

        public int Id { set { id = value; } }

        private int? id;

        private List<ConstituentSnackViewModels> ConstituentSnack;
        public SnackWindow()
        {
            InitializeComponent();
            Loaded += Snack_Load;
        }

        private void Snack_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Snack/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var snack = APIClient.GetElement<SnackViewModels>(response);
                        textBoxSnackName.Text = snack.SnackName;
                        textBoxSnackPrice.Text = snack.Price.ToString();
                        ConstituentSnack = snack.ConstituentSnack;
                        LoadData();
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
            else
            {
                ConstituentSnack = new List<ConstituentSnackViewModels>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (ConstituentSnack != null)
                {
                    dataGridConstituents.ItemsSource = null;
                    dataGridConstituents.ItemsSource = ConstituentSnack;
                    dataGridConstituents.Columns[0].Visibility = Visibility.Hidden;
                    dataGridConstituents.Columns[1].Visibility = Visibility.Hidden;
                    dataGridConstituents.Columns[2].Visibility = Visibility.Hidden;
                    dataGridConstituents.Columns[3].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = new ConstituentSnackWindow();
            if (form.ShowDialog() == true)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.SnackId = id.Value;
                    }
                    ConstituentSnack.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridConstituents.SelectedItem != null)
            {
                var form = new ConstituentSnackWindow();
                form.Model = ConstituentSnack[dataGridConstituents.SelectedIndex];
                if (form.ShowDialog() == true)
                {
                    ConstituentSnack[dataGridConstituents.SelectedIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridConstituents.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запиCь", "ВопроC", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        ConstituentSnack.RemoveAt(dataGridConstituents.SelectedIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
                    }
                    LoadData();
                }
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSnackName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(textBoxSnackPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (ConstituentSnack == null || ConstituentSnack.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButton.OK);
                return;
            }
            try
            {

                List<ConstituentSnackBindingModels> productComponentBM = new List<ConstituentSnackBindingModels>();
                for (int i = 0; i < ConstituentSnack.Count; ++i)
                {
                    productComponentBM.Add(new ConstituentSnackBindingModels
                    {
                        Id = ConstituentSnack[i].Id,
                        SnackId = ConstituentSnack[i].SnackId,
                        ConstituentId = ConstituentSnack[i].ConstituentId,
                        Count = ConstituentSnack[i].Count
                    });
                }
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Snack/UpdElement", new SnackBindingModels
                    {
                        Id = id.Value,
                        SnackName = textBoxSnackName.Text,
                        Price = Convert.ToInt32(textBoxSnackPrice.Text),
                        ConstituentSnack = productComponentBM
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Snack/AddElement", new SnackBindingModels
                    {
                        SnackName = textBoxSnackName.Text,
                        Price = Convert.ToInt32(textBoxSnackPrice.Text),
                        ConstituentSnack = productComponentBM
                    });
                }
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cохранение прошло уCпешно", "Cообщение", MessageBoxButton.OK);
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

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
