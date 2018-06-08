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
    public partial class FormTakeVitaAssassinaInWork : Form
    {
      /*  [Dependency]
        public new IUnityContainer Container { get; set; } */ 

        public int Id { set { id = value; } }

        //private readonly ICuocoService serviceI;

        //private readonly IMainService serviceM;

        private int? id;
         
        public FormTakeVitaAssassinaInWork(/*ICuocoService serviceI, IMainService serviceM*/)
        {
            InitializeComponent();
            /*this.serviceI = serviceI;
            this.serviceM = serviceM; */
        }

        private void FormTakeVitaAssassinaInWork_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                //List<CuocoViewModels> listI = serviceI.GetList();
                var response = APIAcquirente.GetRequest("api/Cuoco/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<CuocoViewModels> list = APIAcquirente.GetElement<List<CuocoViewModels>>(response);
                    if (list != null)
                    {
                        comboBoxImplementer.DisplayMember = "CuocoFIO";
                        comboBoxImplementer.ValueMember = "Id";
                        comboBoxImplementer.DataSource = list;
                        comboBoxImplementer.SelectedItem = null;
                    }
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxImplementer.SelectedValue == null)
            {
                MessageBox.Show("Выберите иCполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                /*  serviceM.TakeVitaAssassinarInWork(new VitaAssassinaBindingModels
                  {
                      Id = id.Value,
                      CuocoId = Convert.ToInt32(comboBoxImplementer.SelectedValue)
                  });
                  MessageBox.Show("Cохранение прошло уCпешно", "Cообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  DialogResult = DialogResult.OK;
                  Close(); */
                var response = APIAcquirente.PostRequest("api/Main/TakeVitaAssassinarInWork", new VitaAssassinaBindingModels
                {
                    Id = id.Value,
                    CuocoId = Convert.ToInt32(comboBoxImplementer.SelectedValue)
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
