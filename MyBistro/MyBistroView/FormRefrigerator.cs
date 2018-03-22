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

namespace MyBistroView
{
    public partial class FormRefrigerator : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IRefrigeratorService service;

        private int? id;

        public FormRefrigerator(IRefrigeratorService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormRefrigerator_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    RefrigeratorViewModels view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.RefrigeratorName;
                        dataGridView.DataSource = view.RefrigeratorConstituent;
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
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new RefrigeratorBindingModels
                    {
                        Id = id.Value,
                        RefrigeratorName = textBoxName.Text
                    });
                }
                else
                {
                    service.AddElement(new RefrigeratorBindingModels
                    {
                        RefrigeratorName = textBoxName.Text
                    });
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
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
