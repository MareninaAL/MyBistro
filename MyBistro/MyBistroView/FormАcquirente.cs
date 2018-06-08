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
using System.Text.RegularExpressions;
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
                    var acquirente = Task.Run(() => APIAcquirente.GetRequestData<АcquirenteViewModels>("api/Аcquirente/Get/" + id.Value)).Result;
                    textBoxFIO.Text = acquirente.АcquirenteFIO;
                    textBoxMail.Text = acquirente.Mail;
                    dataGridView.DataSource = acquirente.Messages;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            string fio = textBoxFIO.Text;
            string mail = textBoxMail.Text;
            if (!string.IsNullOrEmpty(mail))
            {
                if (!Regex.IsMatch(mail, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                {
                    MessageBox.Show("Неверный формат для электронной почты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIAcquirente.PostRequestData("api/Аcquirente/UpdElement", new АcquirenteBindingModels
                {
                    Id = id.Value,
                    АcquirenteFIO = fio,
                    Mail = mail
                }));
            }
            else
            {
                task = Task.Run(() => APIAcquirente.PostRequestData("api/Аcquirente/AddElement", new АcquirenteBindingModels
                {
                    АcquirenteFIO = fio,
                    Mail = mail
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
