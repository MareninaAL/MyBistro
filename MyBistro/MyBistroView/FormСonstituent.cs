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

        public int Id { set { id = value; } }
        

        private int? id;

        public FormConstituent()
        {
            InitializeComponent();
        }

        private void FormComponent_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    /* var response = APIAcquirente.GetRequest("api/Constituent/Get/" + id.Value);
                     if (response.Result.IsSuccessStatusCode)
                     {
                         var component = APIAcquirente.GetElement<ConstituentViewModels>(response);
                         textBoxName.Text = component.ConstituentName;
                     }
                     else
                     {
                         throw new Exception(APIAcquirente.GetError(response));
                     } */
                    var component = Task.Run(() => APIAcquirente.GetRequestData<ConstituentViewModels>("api/Constituent/Get/" + id.Value)).Result;
                    textBoxName.Text = component.ConstituentName;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /* try
             {
                 Task<HttpResponseMessage> response;
                 if (id.HasValue)
                 {
                     response = APIAcquirente.PostRequest("api/Constituent/UpdElement", new ConstituentBindingModels
                     {
                         Id = id.Value,
                         ConstituentName = textBoxName.Text
                     }); 
                 }
                 else
                 {
                     response = APIAcquirente.PostRequest("api/Constituent/AddElement", new ConstituentBindingModels
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
             } */
            string name = textBoxName.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIAcquirente.PostRequestData("api/Constituent/UpdElement", new ConstituentBindingModels
                {
                    Id = id.Value,
                    ConstituentName = name
                }));
            }
            else
            {
                task = Task.Run(() => APIAcquirente.PostRequestData("api/Constituent/AddElement", new ConstituentBindingModels
                {
                    ConstituentName = name
                }));
            }

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
