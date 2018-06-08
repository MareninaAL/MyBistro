using MyBistro.BindingModels;
using MyBistro.ViewModels;
using MyBistroService.BindingModels;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
namespace MyBistroView
{
    public partial class FormMain : Form
    {
      /*  [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMainService service;
        private readonly IReportService reportService; */

        public FormMain(/*IMainService service, IReportService reportService*/)
        {
            InitializeComponent();
           /* this.service = service;
            this.reportService = reportService; */
        }

        private void LoadData()
        {
            try
            {
                var response = APIAcquirente.GetRequest("api/Main/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<VitaAssassinaViewModels> list = APIAcquirente.GetElement<List<VitaAssassinaViewModels>>(response);
                    if (list != null)
                    {
                        dataGridView.DataSource = list;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].Visible = false;
                        dataGridView.Columns[3].Visible = false;
                        dataGridView.Columns[5].Visible = false;
                        dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // var form = Container.Resolve<FormАcquirentes>();
            var form = new FormАcquirentes();
            form.ShowDialog();
        }

        private void компонентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // var form = Container.Resolve<FormConstituents>();

            var form = new FormConstituents();
            form.ShowDialog();
        }

        private void изделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // var form = Container.Resolve<FormSnacks>();
            var form = new FormSnacks();
            form.ShowDialog();
        }

        private void CкладыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var form = Container.Resolve<FormRefrigerators>();
            var form = new FormRefrigerators();
            form.ShowDialog();
        }

        private void CотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var form = Container.Resolve<FormCuocos>();
            var form = new FormCuocos();
            form.ShowDialog();
        }

        private void пополнитьCкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // var form = Container.Resolve<FormPutOnRefrigerator>();
            var form = new FormPutOnRefrigerator();
            form.ShowDialog();
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            //   var form = Container.Resolve<FormCreateVitaAssassina>();
            var form = new FormCreateVitaAssassina();
            form.ShowDialog();
            LoadData();
        }

        private void buttonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                //  var form = Container.Resolve<FormTakeVitaAssassinaInWork>();
                //  form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                var form = new FormTakeVitaAssassinaInWork
                {
                    Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value)
                };
                form.ShowDialog();
                LoadData();
            }
        }

        private void buttonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    // service.FinishVitaAssassina(id);
                    // LoadData();
                    var response = APIAcquirente.PostRequest("api/Main/FinishVitaAssassina", new VitaAssassinaBindingModels
                    {
                        Id = id
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        LoadData();
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

        private void buttonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    // service.PayVitaAssassina(id);
                    //  LoadData();
                    var response = APIAcquirente.PostRequest("api/Main/.PayVitaAssassina", new VitaAssassinaBindingModels
                    {
                        Id = id
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        LoadData();
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

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void прайсизделийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    /* reportService.SaveSnackPrice(new ReportBindingModel
                     {
                         FileName = sfd.FileName
                     });
                     MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information); */
                    var response = APIAcquirente.PostRequest("api/Report/SaveSnackPrice", new ReportBindingModel
                    {
                        FileName = sfd.FileName
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

        private void загруженностьСкладовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormRefrigeratorsLoad();
            form.ShowDialog();
        }

        private void заказыКлиентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormAcquirenteVitaAssassinas();
            form.ShowDialog();
        }
    }
}
