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
    public partial class FormRefrigeratorsLoad : Form
    {
        public FormRefrigeratorsLoad()
        {
            InitializeComponent();
        }

        private void FormStocksLoad_Load(object sender, EventArgs e)
        {
            try
            {
                /* var response = APIAcquirente.GetRequest("api/Report/GetRefregiratorsLoad");
                  if (response.Result.IsSuccessStatusCode)
                  { */
                dataGridView.Rows.Clear();
                foreach (var elem in Task.Run(() => APIAcquirente.GetRequestData<List<RefregiratorsLoadViewModel>>("api/Report/GetRefregiratorsLoad")).Result)
                {
                    dataGridView.Rows.Add(new object[] { elem.RefregiratorName, "", "" });
                    foreach (var listElem in elem.Constituent)
                    {
                        dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                    }
                    dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalCount });
                    dataGridView.Rows.Add(new object[] { });
                }
                /*    } 
                    else
                   {
                       throw new Exception(APIAcquirente.GetError(response));
                   }  */
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

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                /* try
                 {
                     var response = APIAcquirente.PostRequest("api/Report/SaveRefregiratorsLoad", new ReportBindingModel
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
                 } */
                string fileName = sfd.FileName;
                Task task = Task.Run(() => APIAcquirente.PostRequestData("api/Report/SaveRefregiratorsLoad", new ReportBindingModel
                {
                    FileName = fileName
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Выполнено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
            }
        }
    }
}
