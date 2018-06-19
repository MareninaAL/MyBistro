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

using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using MyBistroService.BindingModels;
using MyBistroService.Interfaces;
using Unity;
using Unity.Attributes;
using MyBistroService.ViewModels;

namespace BistroWeb
{
    /// <summary>
    /// Логика взаимодействия для AcquirenteVitaAssassinasWindow.xaml
    /// </summary>
    public partial class AcquirenteVitaAssassinasWindow : Window
    {
       /* [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IReportService service; */

        public AcquirenteVitaAssassinasWindow(/*IReportService service*/)
        {
            InitializeComponent();
            //this.service = service;
        }

        private void buttonMake_Click_1(object sender, RoutedEventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate >= dateTimePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                reportViewer.LocalReport.ReportEmbeddedResource = "BistroWeb.Report1.rdlc";
                ReportParameter parameter = new ReportParameter("ReportParameter1",
                                            "c " + Convert.ToDateTime(dateTimePickerFrom.SelectedDate).ToString("dd-MM") +
                                            " по " + Convert.ToDateTime(dateTimePickerTo.SelectedDate).ToString("dd-MM"));
                reportViewer.LocalReport.SetParameters(parameter);


                // var dataSource = service.GetAcquirenteVitaAssassinas(new ReportBindingModel
                var response = APIClient.PostRequest("api/Report/GetAcquirenteVitaAssassinas", new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.SelectedDate,
                    DateTo = dateTimePickerTo.SelectedDate
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    var dataSource = APIClient.GetElement<List<AcquirenteVitaAssassinaModel>>(response);
                    ReportDataSource source = new ReportDataSource("DataSet1", dataSource);
                    reportViewer.LocalReport.DataSources.Add(source);
                }
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }



                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonToPdf_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate >= dateTimePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    //  service.SaveAcquirenteVitaAssassinas(new ReportBindingModel
                    var response = APIClient.PostRequest("api/Report/SaveAcquirenteVitaAssassinas", new ReportBindingModel
                    {
                        FileName = sfd.FileName,
                        DateFrom = dateTimePickerFrom.SelectedDate,
                        DateTo = dateTimePickerTo.SelectedDate
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                     }else
                    {
                        throw new Exception(APIClient.GetError(response));

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
