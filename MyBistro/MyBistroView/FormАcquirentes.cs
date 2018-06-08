using MyBistro.BindingModels;
using MyBistro.ViewModels;
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
using Unity;
using Unity.Attributes;

namespace MyBistroView
{
    public partial class FormАcquirentes : Form
    {
        /*[Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IАcquirenteService service; */

        public FormАcquirentes(/*IАcquirenteService service*/)
        {
            InitializeComponent();
          //  this.service = service;
        }

        private void FormClients_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                //  List<АcquirenteViewModels> list = service.GetList();
                var response = APIAcquirente.GetRequest("api/Аcquirente/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<АcquirenteViewModels> list = APIAcquirente.GetElement<List<АcquirenteViewModels>>(response);
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
          //  var form = Container.Resolve<FormАcquirente>();
          var form = new FormАcquirente();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                //    var form = Container.Resolve<FormАcquirente>();
                var form = new FormАcquirente();
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
                        var response = APIAcquirente.PostRequest("api/Acquirente/DelElement", new АcquirenteBindingModels { Id = id });
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
