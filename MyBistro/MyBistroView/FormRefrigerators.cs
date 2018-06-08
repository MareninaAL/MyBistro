using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyBistro.ViewModels;
using Unity;
using Unity.Attributes;
using MyBistro.BindingModels;

namespace MyBistroView
{
    public partial class FormRefrigerators : Form
    {
      /*  [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IRefrigeratorService service; */

        public FormRefrigerators(/*IRefrigeratorService service*/)
        {
            InitializeComponent();
           // this.service = service;
        }

        private void FormRefrigerators_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var response = APIAcquirente.GetRequest("api/Refrigerator/GetList");
                // List<RefrigeratorViewModels> list = service.GetList();
                if (response.Result.IsSuccessStatusCode)
                {
                    List<RefrigeratorViewModels> list = APIAcquirente.GetElement<List<RefrigeratorViewModels>>(response);
                    if (list != null)
                    {
                        dataGridView.DataSource = list;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // var form = Container.Resolve<FormRefrigerator>();
            var form = new FormRefrigerator();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                //  var form = Container.Resolve<FormRefrigerator>();
                var form = new FormRefrigerator();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запиCь", "ВопроC", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        // service.DelElement(id);
                        var response = APIAcquirente.PostRequest("api/Refrigerator/DelElement", new АcquirenteBindingModels { Id = id });
                        if (!response.Result.IsSuccessStatusCode)
                        {
                            throw new Exception(APIAcquirente.GetError(response));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
