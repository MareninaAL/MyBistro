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
       /* [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IReportService service; */ 

        public FormAcquirenteVitaAssassinas(/*IReportService service */)
        {
            InitializeComponent();
           // this.service = service;
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
                try
                {
                    /*service.SaveAcquirenteVitaAssassinas(new ReportBindingModel

                    {
                        FileName = sfd.FileName,
                        DateFrom = dateTimePickerFrom.Value,
                        DateTo = dateTimePickerTo.Value
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information); */
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
                }
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

                /* var dataSource = service.GetAcquirenteVitaAssassinas(new ReportBindingModel
                 {
                     DateFrom = dateTimePickerFrom.Value,
                     DateTo = dateTimePickerTo.Value
                 });
                 ReportDataSource source = new ReportDataSource("DataSetOrders", dataSource);
                 reportViewer.LocalReport.DataSources.Add(source);
                 reportViewer.RefreshReport(); */
                var response = APIAcquirente.PostRequest("api/Report/GetAcquirenteVitaAssassinas", new ReportBindingModel
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
                }

                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
