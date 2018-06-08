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
       /* [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IRefrigeratorService serviceS;

        private readonly IConstituentService serviceC;

        private readonly IMainService serviceM; */

        public FormPutOnRefrigerator(/*IRefrigeratorService serviceS, IConstituentService serviceC, IMainService serviceM */)
        {
            InitializeComponent();
           /* this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM; */
        }

        private void FormPutOnRefrigerator_Load(object sender, EventArgs e)
        {
            try
            {
                // List<ConstituentViewModels> listC = serviceC.GetList();
                var responseC = APIAcquirente.GetRequest("api/Constituent/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<ConstituentViewModels> list = APIAcquirente.GetElement<List<ConstituentViewModels>>(responseC);
                    if (list != null)
                    {
                        comboBoxConstituent.DisplayMember = "ConstituentName";
                        comboBoxConstituent.ValueMember = "Id";
                        comboBoxConstituent.DataSource = list;
                        comboBoxConstituent.SelectedItem = null;
                    }

                }

                else
                {
                    throw new Exception(APIAcquirente.GetError(responseC));
                }

                var responseS = APIAcquirente.GetRequest("api/Refrigerator/GetList");
                if (responseS.Result.IsSuccessStatusCode)
                {
                    List<RefrigeratorViewModels> list = APIAcquirente.GetElement<List<RefrigeratorViewModels>>(responseS);
                    // List<RefrigeratorViewModels> listS = serviceS.GetList();
                    if (list != null)
                    {
                        comboBoxRefrigerator.DisplayMember = "RefrigeratorName";
                        comboBoxRefrigerator.ValueMember = "Id";
                        comboBoxRefrigerator.DataSource = list;
                        comboBoxRefrigerator.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIAcquirente.GetError(responseC));
                }
            }

            catch (Exception ex)
            {
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
                /*serviceM.PutConstituentOnRefrigerator(new RefrigeratorConstituentBindingModels
                {
                    ConstituentId = Convert.ToInt32(comboBoxConstituent.SelectedValue),
                    RefrigeratorId = Convert.ToInt32(comboBoxRefrigerator.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Cохранение прошло уCпешно", "Cообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close(); */
                var response = APIAcquirente.PostRequest("api/Main/PutConstituentOnRefrigerator", new RefrigeratorConstituentBindingModels
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
