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
    public partial class FormSnacks : Form
    {

        public FormSnacks()
        {
            InitializeComponent();
        }

        private void FormSnacks_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
               /* var response = APIAcquirente.GetRequest("api/Snack/GetList");
                if (response.Result.IsSuccessStatusCode)
                { */ 
                    List<SnackViewModels> list = Task.Run(() => APIAcquirente.GetRequestData<List<SnackViewModels>>("api/Snack/GetList")).Result;
                if (list != null)
                    {
                        dataGridView.DataSource = list;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
             /*   }
                else
                {
                    throw new Exception(APIAcquirente.GetError(response));
                } */ 
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormSnack();
            /*  if (form.ShowDialog() == DialogResult.OK)
              {
                  LoadData();
              } */
            form.ShowDialog();
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                /*  var form = new FormSnack();
                  form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                  if (form.ShowDialog() == DialogResult.OK)
                  {
                      LoadData();
                  } */
                var form = new FormSnack
                {
                    Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value)
                };
                form.ShowDialog();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запиCь", "ВопроC", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    /*  try
                      {
                          var response = APIAcquirente.PostRequest("api/Snack/DelElement", new АcquirenteBindingModels { Id = id });
                          if (!response.Result.IsSuccessStatusCode)
                          {
                              throw new Exception(APIAcquirente.GetError(response));
                          }
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                      LoadData(); */
                    Task task = Task.Run(() => APIAcquirente.PostRequestData("api/Snack/DelElement", new АcquirenteBindingModels { Id = id }));

                    task.ContinueWith((prevTask) => MessageBox.Show("Запись удалена. Обновите список", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information),
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

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
