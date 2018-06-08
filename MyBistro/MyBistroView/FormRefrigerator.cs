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
using MyBistroService.Interfaces;
using MyBistro.ViewModels;
using MyBistro.BindingModels;
using System.Net.Http;

namespace MyBistroView
{
    public partial class FormRefrigerator : Form
    {
        /*[Dependency]
        public new IUnityContainer Container { get; set; } */

        public int Id { set { id = value; } }

    //    private readonly IRefrigeratorService service;

        private int? id;

        public FormRefrigerator(/*IRefrigeratorService service*/)
        {
            InitializeComponent();
          //  this.service = service;
        }

        private void FormRefrigerator_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIAcquirente.GetRequest("api/Refrigerator/Get/" + id.Value);
                    //RefrigeratorViewModels view = service.GetElement(id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var stock = APIAcquirente.GetElement<RefrigeratorViewModels>(response);
                        textBoxName.Text = stock.RefrigeratorName;
                        dataGridView.DataSource = stock.RefrigeratorConstituent;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].Visible = false;
                        dataGridView.Columns[2].Visible = false;
                        dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    /*service.UpdElement(new RefrigeratorBindingModels
                    {
                        Id = id.Value,
                        RefrigeratorName = textBoxName.Text
                    }); */
                    response = APIAcquirente.PostRequest("api/Refrigerator/UpdElement", new RefrigeratorBindingModels
                    {
                        Id = id.Value,
                        RefrigeratorName = textBoxName.Text
                    });
                }
                else
                {
                    /*service.AddElement(new RefrigeratorBindingModels
                    {
                        RefrigeratorName = textBoxName.Text
                    }); */
                    response = APIAcquirente.PostRequest("api/Refrigerator/AddElement", new RefrigeratorBindingModels
                    {
                        RefrigeratorName = textBoxName.Text
                    });
                }
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
