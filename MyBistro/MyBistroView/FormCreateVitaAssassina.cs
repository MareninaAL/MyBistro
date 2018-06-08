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
    public partial class FormCreateVitaAssassina : Form
    {

        public FormCreateVitaAssassina()
        {
            InitializeComponent();
        }

        private void FormCreateVitaAssassina_Load(object sender, EventArgs e)
        {
            try
            {
                    List<АcquirenteViewModels> listA = Task.Run(() => APIAcquirente.GetRequestData<List<АcquirenteViewModels>>("api/Аcquirente/GetList")).Result;
                if (listA != null)
                    {
                        comboBoxАcquirente.DisplayMember = "АcquirenteFIO";
                        comboBoxАcquirente.ValueMember = "Id";
                        comboBoxАcquirente.DataSource = listA;
                        comboBoxАcquirente.SelectedItem = null;
                    }
                    List<SnackViewModels> listC = Task.Run(() => APIAcquirente.GetRequestData<List<SnackViewModels>>("api/Snack/GetList")).Result;
                if (listC != null)
                    {
                        comboBoxSnack.DisplayMember = "SnackName";
                        comboBoxSnack.ValueMember = "Id";
                        comboBoxSnack.DataSource = listC;
                        comboBoxSnack.SelectedItem = null;
                    }
                  
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

        private void CalcSum()
        {
            if (comboBoxSnack.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxSnack.SelectedValue);
                    SnackViewModels product = Task.Run(() => APIAcquirente.GetRequestData<SnackViewModels>("api/Snack/Get/" + id)).Result;
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * (int)product.Price).ToString();
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

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле КоличеCтво", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxАcquirente.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxSnack.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int clientId = Convert.ToInt32(comboBoxАcquirente.SelectedValue);
            int productId = Convert.ToInt32(comboBoxSnack.SelectedValue);
            int count = Convert.ToInt32(textBoxCount.Text);
            int sum = Convert.ToInt32(textBoxSum.Text);
            Task task = Task.Run(() => APIAcquirente.PostRequestData("api/Main/CreateVitaAssassina", new VitaAssassinaBindingModels
            {
                АcquirenteId = clientId,
                SnackId = productId,
                Count = count,
                Sum = sum
            }));

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
