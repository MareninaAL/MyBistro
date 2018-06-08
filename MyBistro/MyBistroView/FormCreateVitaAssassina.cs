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
       /* [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IАcquirenteService serviceC;

        private readonly ISnackService serviceP;

        private readonly IMainService serviceM; */ 

        public FormCreateVitaAssassina(/*IАcquirenteService serviceC, ISnackService serviceP, IMainService serviceM*/)
        {
            InitializeComponent();
          /*  this.serviceC = serviceC;
            this.serviceP = serviceP;
            this.serviceM = serviceM; */
        }

        private void FormCreateVitaAssassina_Load(object sender, EventArgs e)
        {
            try
            {
                var responseC = APIAcquirente.GetRequest("api/Аcquirente/GetList");
                // List<АcquirenteViewModels> listC = serviceC.GetList();
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<АcquirenteViewModels> list = APIAcquirente.GetElement<List<АcquirenteViewModels>>(responseC);
                    if (list != null)
                    {
                        comboBoxАcquirente.DisplayMember = "АcquirenteFIO";
                        comboBoxАcquirente.ValueMember = "Id";
                        comboBoxАcquirente.DataSource = list;
                        comboBoxАcquirente.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIAcquirente.GetError(responseC));
                }

                var responseP = APIAcquirente.GetRequest("api/Snack/GetList");
                if (responseP.Result.IsSuccessStatusCode)
                {
                    List<SnackViewModels> list = APIAcquirente.GetElement<List<SnackViewModels>>(responseP);
                    // List<SnackViewModels> listP = serviceP.GetList();
                    if (list != null)
                    {
                        comboBoxSnack.DisplayMember = "SnackName";
                        comboBoxSnack.ValueMember = "Id";
                        comboBoxSnack.DataSource = list;
                        comboBoxSnack.SelectedItem = null;
                    }
                }

                else
                {
                    throw new Exception(APIAcquirente.GetError(responseP));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxSnack.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    /* int id = Convert.ToInt32(comboBoxSnack.SelectedValue);
                     SnackViewModels product = serviceP.GetElement(id);
                     int count = Convert.ToInt32(textBoxCount.Text);
                     textBoxSum.Text = (count * product.Price).ToString(); */
                    int id = Convert.ToInt32(comboBoxProduct.SelectedValue);
                    var responseP = APIAcquirente.GetRequest("api/Snack/Get/" + id);
                    if (responseP.Result.IsSuccessStatusCode)
                    {
                        SnackViewModels product = APIAcquirente.GetElement<SnackViewModels>(responseP);
                        int count = Convert.ToInt32(textBoxCount.Text);
                        textBoxSum.Text = (count * (int)product.Price).ToString();
                    }
                    else
                    {
                        throw new Exception(APIAcquirente.GetError(responseP));
                    }
                }
                catch (Exception ex)
                {
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
            try
            {
                /* serviceM.CreateVitaAssassina(new VitaAssassinaBindingModels
                 {
                     АcquirenteId = Convert.ToInt32(comboBoxАcquirente.SelectedValue),
                     SnackId = Convert.ToInt32(comboBoxSnack.SelectedValue),
                     Count = Convert.ToInt32(textBoxCount.Text),
                     Sum = Convert.ToInt32(textBoxSum.Text)
                 });
                 MessageBox.Show("Cохранение прошло уCпешно", "Cообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 DialogResult = DialogResult.OK;
                 Close(); */
                var response = APIAcquirente.PostRequest("api/Main/CreateVitaAssassina", new VitaAssassinaBindingModels
                {
                    АcquirenteId = Convert.ToInt32(comboBoxАcquirente.SelectedValue),
                    SnackId = Convert.ToInt32(comboBoxSnack.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
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
