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
    public partial class FormSnack : Form
    {

        public int Id { set { id = value; } }
        

        private int? id;

        private List<ConstituentSnackViewModels> ConstituentSnack;

        public FormSnack()
        {
            InitializeComponent();
        }

        private void FormSnack_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                 /*   var response = APIAcquirente.GetRequest("api/Snack/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    { */ 
                        var snack = Task.Run(() => APIAcquirente.GetRequestData<SnackViewModels>("api/Snack/Get/" + id.Value)).Result;
                    textBoxName.Text = snack.SnackName;
                        textBoxPrice.Text = snack.Price.ToString();
                        ConstituentSnack = snack.ConstituentSnack;
                        LoadData();
                 //   }
                    /* else
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
            else
            {
                ConstituentSnack = new List<ConstituentSnackViewModels>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (ConstituentSnack != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = ConstituentSnack;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            var form = new FormConstituentSnack();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.SnackId = id.Value;
                    }
                    ConstituentSnack.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormConstituentSnack();
                form.Model = ConstituentSnack[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ConstituentSnack[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
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
                    try
                    {
                        ConstituentSnack.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ConstituentSnack == null || ConstituentSnack.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /* try
             {

                 List<ConstituentSnackBindingModels> productComponentBM = new List<ConstituentSnackBindingModels>();
                 for (int i = 0; i < ConstituentSnack.Count; ++i)
                 {
                     productComponentBM.Add(new ConstituentSnackBindingModels
                     {
                         Id = ConstituentSnack[i].Id,
                         SnackId = ConstituentSnack[i].SnackId,
                         ConstituentId = ConstituentSnack[i].ConstituentId,
                         Count = ConstituentSnack[i].Count
                     });
                 }
                 Task<HttpResponseMessage> response;
                 if (id.HasValue)
                 {
                     response = APIAcquirente.PostRequest("api/Snack/UpdElement", new SnackBindingModels
                     {
                         Id = id.Value,
                         SnackName = textBoxName.Text,
                         Price = Convert.ToInt32(textBoxPrice.Text),
                         ConstituentSnack = productComponentBM
                     });
                 }
                 else
                 {
                     response = APIAcquirente.PostRequest("api/Snack/AddElement", new SnackBindingModels
                     {
                           SnackName = textBoxName.Text,
                           Price = Convert.ToInt32(textBoxPrice.Text),
                           ConstituentSnack = productComponentBM
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
            List<ConstituentSnackBindingModels> productComponentBM = new List<ConstituentSnackBindingModels>();
            for (int i = 0; i < ConstituentSnack.Count; ++i)
            {
                productComponentBM.Add(new ConstituentSnackBindingModels
                {
                    Id = ConstituentSnack[i].Id,
                    SnackId = ConstituentSnack[i].SnackId,
                    ConstituentId = ConstituentSnack[i].ConstituentId,
                    Count = ConstituentSnack[i].Count
                });
            }
            string name = textBoxName.Text;
            int price = Convert.ToInt32(textBoxPrice.Text);
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIAcquirente.PostRequestData("api/Snack/UpdElement", new SnackBindingModels
                {
                    Id = id.Value,
                    SnackName = name,
                    Price = price,
                    ConstituentSnack = productComponentBM
                }));
            }
            else
            {
                task = Task.Run(() => APIAcquirente.PostRequestData("api/Snack/AddElement", new SnackBindingModels
                {
                    SnackName = name,
                    Price = price,
                    ConstituentSnack = productComponentBM
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
           // DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
