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
    public partial class FormConstituentSnack : Form
    {
        
        public ConstituentSnackViewModels Model { set { model = value; } get { return model; } }
        

        private ConstituentSnackViewModels model;

        public FormConstituentSnack()
        {
            InitializeComponent();
        }

        private void FormConstituentSnack_Load(object sender, EventArgs e)
        {
            try
            {
                comboBoxConstituent.DisplayMember = "ConstituentName";
                      comboBoxConstituent.ValueMember = "Id";
                      comboBoxConstituent.DataSource = Task.Run(() => APIAcquirente.GetRequestData<List<ConstituentViewModels>>("api/Constituent/GetList")).Result;
                comboBoxConstituent.SelectedItem = null;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxConstituent.Enabled = false;
                comboBoxConstituent.SelectedValue = model.ConstituentId;
                textBoxCount.Text = model.Count.ToString();
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
            try
            {
                if (model == null)
                {
                    model = new ConstituentSnackViewModels
                    {
                        ConstituentId = Convert.ToInt32(comboBoxConstituent.SelectedValue),
                        ConstituentName = comboBoxConstituent.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
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
            Close();
        }
    }
}
