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
    public partial class FormPutOnRefrigerator : Form
    {

        public FormPutOnRefrigerator()
        {
            InitializeComponent();
        }

        private void FormPutOnRefrigerator_Load(object sender, EventArgs e)
        {
            try
            {
               /* var responseC = APIAcquirente.GetRequest("api/Constituent/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                { */
                    List<ConstituentViewModels> listC = Task.Run(() => APIAcquirente.GetRequestData<List<ConstituentViewModels>>("api/Constituent/GetList")).Result;
                    if (listC != null)
                    {
                        comboBoxConstituent.DisplayMember = "ConstituentName";
                        comboBoxConstituent.ValueMember = "Id";
                        comboBoxConstituent.DataSource = listC;
                        comboBoxConstituent.SelectedItem = null;
                    }

                /*}

                else
                {
                    throw new Exception(APIAcquirente.GetError(responseC));
                }

                var responseS = APIAcquirente.GetRequest("api/Refrigerator/GetList");
                if (responseS.Result.IsSuccessStatusCode)
                { */
                    List<RefrigeratorViewModels> listR = Task.Run(() => APIAcquirente.GetRequestData<List<RefrigeratorViewModels>>("api/Refrigerator/GetList")).Result;
                if (listR != null)
                    {
                        comboBoxRefrigerator.DisplayMember = "RefrigeratorName";
                        comboBoxRefrigerator.ValueMember = "Id";
                        comboBoxRefrigerator.DataSource = listR;
                        comboBoxRefrigerator.SelectedItem = null;
                    }
              /*  }
                else
                {
                    throw new Exception(APIAcquirente.GetError(responseC));
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле КоличеCтво", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxConstituent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxRefrigerator.SelectedValue == null)
            {
                MessageBox.Show("Выберите Cклад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                /*var response = APIAcquirente.PostRequest("api/Main/PutConstituentOnRefrigerator", new RefrigeratorConstituentBindingModels
                {
                    ConstituentId = Convert.ToInt32(comboBoxConstituent.SelectedValue),
                    RefrigeratorId = Convert.ToInt32(comboBoxRefrigerator.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    throw new Exception(APIAcquirente.GetError(response));
                } */
                int componentId = Convert.ToInt32(comboBoxComponent.SelectedValue);
                int stockId = Convert.ToInt32(comboBoxStock.SelectedValue);
                int count = Convert.ToInt32(textBoxCount.Text);
                Task task = Task.Run(() => APIAcquirente.PostRequestData("api/Main/PutConstituentOnRefrigerator", new RefrigeratorConstituentBindingModels
                {
                    ConstituentId = componentId,
                    RefrigeratorId = stockId,
                    Count = count
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Склад пополнен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
          //  DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
