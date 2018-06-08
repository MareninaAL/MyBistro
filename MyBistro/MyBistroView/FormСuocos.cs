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
    public partial class FormCuocos : Form
    {
       /* [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ICuocoService service; */ 

        public FormCuocos (/*ICuocoService service*/ )
        {
            InitializeComponent();
           // this.service = service;
        }

        private void FormCuocos_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // List<CuocoViewModels> list = service.GetList();
                var response = APIAcquirente.GetRequest("api/Cuoco/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<CuocoViewModels> list = APIAcquirente.GetElement<List<CuocoViewModels>>(response);
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
            //   var form = Container.Resolve<FormCuoco>();
            var form = new FormCuoco();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                // var form = Container.Resolve<FormCuoco>();
                var form = new FormCuoco();
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
                        //      service.DelElement(id);
                        var response = APIAcquirente.PostRequest("api/Cuoco/DelElement", new АcquirenteBindingModels { Id = id });
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
