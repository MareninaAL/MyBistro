﻿using MyBistro.BindingModels;
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
    /// Логика взаимодействия для ConstituentWindow.xaml
    /// </summary>
    public partial class ConstituentWindow : Window
    {

        public int Id { set { id = value; } }

        private int? id;
        public ConstituentWindow()
        {
            InitializeComponent();
            Loaded += Constituent_Load;
        }

        private void Constituent_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIClient.GetRequest("api/Constituent/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var constituent = APIClient.GetElement<ConstituentViewModels>(response);
                        textBoxConstituentName.Text = constituent.ConstituentName;
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
        }


        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxConstituentName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIClient.PostRequest("api/Constituent/UpdElement", new ConstituentBindingModels
                    {
                        Id = id.Value,
                        ConstituentName = textBoxConstituentName.Text
                    });
                }
                else
                {
                    response = APIClient.PostRequest("api/Constituent/AddElement", new ConstituentBindingModels
                    {
                        ConstituentName = textBoxConstituentName.Text
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
