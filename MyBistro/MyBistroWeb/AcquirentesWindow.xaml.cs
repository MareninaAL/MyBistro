﻿using MyBistro.ViewModels;
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
    /// Логика взаимодействия для AcquirentesWindow.xaml
    /// </summary>
    public partial class AcquirentesWindow : Window
    {
        [Dependency]
        public  IUnityContainer Container { get; set; }

        private readonly IАcquirenteService service;
        public AcquirentesWindow(IАcquirenteService service)
        {
            InitializeComponent();
            this.service = service;
            Loaded += Clients_Load;
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<АcquirenteViewModels> list = service.GetList();
                if (list != null)
                {
                    dataGridAcquirente.ItemsSource = list;
                    dataGridAcquirente.Columns[0].Visibility = Visibility.Hidden;
                    dataGridAcquirente.Columns[1].Width = dataGridAcquirente.Width - 9;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<АcquirenteWindow>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridAcquirente.SelectedItem != null )
            {
                var form = Container.Resolve<АcquirenteWindow>();
                form.Id = ((АcquirenteViewModels)dataGridAcquirente.SelectedItem).Id;
                if (form.ShowDialog() == true)
                {
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
            if (dataGridAcquirente.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запиCь", "ВопроC", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int id = ((АcquirenteViewModels)dataGridAcquirente.SelectedItem).Id;
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
                    }
                    LoadData();
                }
            }
        }
    }
}
