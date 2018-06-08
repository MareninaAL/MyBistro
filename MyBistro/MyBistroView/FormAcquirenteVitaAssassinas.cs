using Microsoft.Reporting.WinForms;
using MyBistroService.BindingModels;
using MyBistroService.Interfaces;
using MyBistroService.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace MyBistroView
{
    public partial class FormAcquirenteVitaAssassinas : Form
    {

        public FormAcquirenteVitaAssassinas()
        {
            InitializeComponent();
        }

        private void FormAcquirenteVitaAssassinas_Load(object sender, EventArgs e)
        {

            this.reportViewer.RefreshReport();
        }

        private void buttonInPDF_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog

            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                /* try
                 {
                     var response = APIAcquirente.PostRequest("api/Report/SaveAcquirenteVitaAssassinas", new ReportBindingModel
                     {
                         FileName = sfd.FileName,
                         DateFrom = dateTimePickerFrom.Value,
                         DateTo = dateTimePickerTo.Value
                     });
                     if (response.Result.IsSuccessStatusCode)
                     {
                         MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     }
                     else
                     {
                         throw new Exception(APIAcquirente.GetError(response));
                     }
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 } */
                string fileName = sfd.FileName;
                Task task = Task.Run(() => APIAcquirente.PostRequestData("api/Report/SaveAcquirenteVitaAssassinas", new ReportBindingModel
                {
                    FileName = fileName,
                    DateFrom = dateTimePickerFrom.Value,
                    DateTo = dateTimePickerTo.Value
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Список заказов сохранен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information),
                TaskContinuationOptions.OnlyOnRanToCompletion);

                task.ContinueWith((prevTask) =>
                {
                    var ex = (Exception)prevTask.Exception;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod", "c " + dateTimePickerFrom.Value.ToShortDateString() +
                " по " + dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);

                /*var response = APIAcquirente.PostRequest("api/Report/GetAcquirenteVitaAssassinas", new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.Value,
                    DateTo = dateTimePickerTo.Value
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    var dataSource = APIAcquirente.GetElement<List<AcquirenteVitaAssassinaModel>>(response);
                    ReportDataSource source = new ReportDataSource("DataSetOrders", dataSource);
                    reportViewer.LocalReport.DataSources.Add(source);
                }
                else
                {
                    throw new Exception(APIAcquirente.GetError(response));
                } */
                var dataSource = Task.Run(() => APIAcquirente.PostRequestData<ReportBindingModel, List<AcquirenteVitaAssassinaModel>>("api/Report/GetAcquirenteVitaAssassinas",
                   new ReportBindingModel
                   {
                       DateFrom = dateTimePickerFrom.Value,
                       DateTo = dateTimePickerTo.Value
                   })).Result;
                ReportDataSource source = new ReportDataSource("DataSetOrders", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport(); 
                
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
