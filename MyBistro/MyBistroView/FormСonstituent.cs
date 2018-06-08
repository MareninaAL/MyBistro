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
    public partial class FormConstituent : Form
    {
       /* [Dependency]
        public new IUnityContainer Container { get; set; } */ 

        public int Id { set { id = value; } }

        //private readonly IConstituentService service;

        private int? id;

        public FormConstituent(/*IConstituentService service*/ )
        {
            InitializeComponent();
           // this.service = service;
        }

        private void FormComponent_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    // ConstituentViewModels view = service.GetElement(id.Value);
                    var response = APIAcquirente.GetRequest("api/Constituent/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var component = APIAcquirente.GetElement<ConstituentViewModels>(response);
                        textBoxName.Text = component.ConstituentName;
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
                    response = APIAcquirente.PostRequest("api/Constituent/UpdElement", new ConstituentBindingModels
                    //   service.UpdElement(new ConstituentBindingModels
                    {
                        Id = id.Value,
                        ConstituentName = textBoxName.Text
                    }); 
                }
                else
                {
                    response = APIAcquirente.PostRequest("api/Constituent/AddElement", new ConstituentBindingModels
                    //service.AddElement(new ConstituentBindingModels
                    {
                        ConstituentName = textBoxName.Text
                    }); 
                }
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cохранение прошло уCпешно", "Cообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
