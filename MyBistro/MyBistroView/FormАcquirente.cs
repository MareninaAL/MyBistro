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
    public partial class FormАcquirente : Form
    {
        

        public int Id { set { id = value; } }

        private int? id;


        public FormАcquirente()
        {
            InitializeComponent();
        }

        private void FormАcquirente_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    /* var response = APIAcquirente.GetRequest("api/Аcquirente/Get/" + id.Value);
                     if (response.Result.IsSuccessStatusCode)
                     {
                         var acquirente = APIAcquirente.GetElement<АcquirenteViewModels>(response);
                         textBoxFIO.Text = acquirente.АcquirenteFIO;
                     }

                     else
                     {
                         throw new Exception(APIAcquirente.GetError(response));
                     } */
                    var acquirente = Task.Run(() => APIAcquirente.GetRequestData<АcquirenteViewModels>("api/Acquirente/Get/" + id.Value)).Result;
                    textBoxFIO.Text = acquirente.АcquirenteFIO;
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
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /* try
             {
                 Task<HttpResponseMessage> response;
                 if (id.HasValue)
                 {
                     response = APIAcquirente.PostRequest("api/Аcquirente/UpdElement", new АcquirenteBindingModels
                     {
                         Id = id.Value,
                         АcquirenteFIO = textBoxFIO.Text
                     });
                 }
                 else
                 {
                     response = APIAcquirente.PostRequest("api/Аcquirente/AddElement", new АcquirenteBindingModels
                     {
                         АcquirenteFIO = textBoxFIO.Text
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
             } */
            string fio = textBoxFIO.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIAcquirente.PostRequestData("api/Acquirente/UpdElement", new АcquirenteBindingModels
                {
                    Id = id.Value,
                    АcquirenteFIO = fio
                }));
            }
            else
            {
                task = Task.Run(() => APIAcquirente.PostRequestData("api/Acquirente/AddElement", new АcquirenteBindingModels
                {
                    АcquirenteFIO = fio
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
