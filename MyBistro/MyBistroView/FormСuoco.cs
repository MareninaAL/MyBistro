using MyBistro.BindingModels;
using MyBistro.ViewModels;
using MyBistroService.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace MyBistroView
{
    public partial class FormCuoco : Form
    {
       /* [Dependency]
        public new IUnityContainer Container { get; set; } */ 

        public int Id { set { id = value; } }

      //  private readonly ICuocoService service;

        private int? id;

        public FormCuoco(/*ICuocoService service*/)
        {
            InitializeComponent();
         //   this.service = service;
        }

        private void FormImplementer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var response = APIAcquirente.GetRequest("api/Cuoco/Get/" + id.Value);
                    // CuocoViewModels view = service.GetElement(id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var cuoco = APIAcquirente.GetElement<CuocoViewModels>(response);
                        textBoxFIO.Text = cuoco.CuocoFIO;
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
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    response = APIAcquirente.PostRequest("api/Cuoco/UpdElement", new CuocoBindingModels
                    //  service.UpdElement(new CuocoBindingModels
                    {
                        Id = id.Value,
                        CuocoFIO = textBoxFIO.Text
                    });
                }
                else
                {
                    response = APIAcquirente.PostRequest("api/Cuoco/AddElement", new CuocoBindingModels
                    //  service.AddElement(new CuocoBindingModels
                    {
                        CuocoFIO = textBoxFIO.Text
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
