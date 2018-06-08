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

        public int Id { set { id = value; } }
        

        private int? id;

        public FormRefrigerator()
        {
            InitializeComponent();
        }

        private void FormRefrigerator_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                        var stock = Task.Run(() => APIAcquirente.GetRequestData<RefrigeratorViewModels>("api/Refrigerator/Get/" + id.Value)).Result;
                    textBoxName.Text = stock.RefrigeratorName;
                        dataGridView.DataSource = stock.RefrigeratorConstituent;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].Visible = false;
                        dataGridView.Columns[2].Visible = false;
                        dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                
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
            string name = textBoxName.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIAcquirente.PostRequestData("api/Refrigerator/UpdElement", new RefrigeratorBindingModels
                {
                    Id = id.Value,
                    RefrigeratorName = name
                }));
            }
            else
            {
                task = Task.Run(() => APIAcquirente.PostRequestData("api/Refrigerator/AddElement", new RefrigeratorBindingModels
                {
                    RefrigeratorName = name
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
            Close();
        }
    }
}
