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
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IRefrigeratorService serviceS;

        private readonly IConstituentService serviceC;

        private readonly IMainService serviceM;

        public FormPutOnRefrigerator(IRefrigeratorService serviceS, IConstituentService serviceC, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
        }

        private void FormPutOnRefrigerator_Load(object sender, EventArgs e)
        {
            try
            {
                List<ConstituentViewModels> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxConstituent.DisplayMember = "ConstituentName";
                    comboBoxConstituent.ValueMember = "Id";
                    comboBoxConstituent.DataSource = listC;
                    comboBoxConstituent.SelectedItem = null;
                }
                List<RefrigeratorViewModels> listS = serviceS.GetList();
                if (listS != null)
                {
                    comboBoxRefrigerator.DisplayMember = "RefrigeratorName";
                    comboBoxRefrigerator.ValueMember = "Id";
                    comboBoxRefrigerator.DataSource = listS;
                    comboBoxRefrigerator.SelectedItem = null;
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
                serviceM.PutConstituentOnRefrigerator(new RefrigeratorConstituentBindingModels
                {
                    ConstituentId = Convert.ToInt32(comboBoxConstituent.SelectedValue),
                    RefrigeratorId = Convert.ToInt32(comboBoxRefrigerator.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Cохранение прошло уCпешно", "Cообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
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
