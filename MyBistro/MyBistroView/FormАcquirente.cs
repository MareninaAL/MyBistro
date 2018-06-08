﻿using MyBistro.BindingModels;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace MyBistroView
{
    public partial class FormАcquirente : Form
    {

       /* [Dependency]
        public new IUnityContainer Container { get; set; } */

        public int Id { set { id = value; } }

      //  private readonly IАcquirenteService service;

        private int? id;


        public FormАcquirente(/*IАcquirenteService service*/)
        {
            InitializeComponent();
          //  this.service = service;
        }

        private void FormАcquirente_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    // АcquirenteViewModels view = service.GetElement(id.Value);\
                    var response = APIAcquirente.GetRequest("api/Аcquirente/Get/" + id.Value);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var acquirente = APIAcquirente.GetElement<АcquirenteViewModels>(response);
                        textBoxFIO.Text = acquirente.АcquirenteFIO;
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
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Task<HttpResponseMessage> response;
                if (id.HasValue)
                {
                    /*service.UpdElement(new АcquirenteBindingModels
                    {
                        Id = id.Value,
                        АcquirenteFIO = textBoxFIO.Text
                    }); */
                    response = APIAcquirente.PostRequest("api/Аcquirente/UpdElement", new АcquirenteBindingModels
                    {
                        Id = id.Value,
                        АcquirenteFIO = textBoxFIO.Text
                    });
                }
                else
                {
                    /* service.AddElement(new АcquirenteBindingModels
                    {
                        АcquirenteFIO = textBoxFIO.Text
                    }); */
                    response = APIAcquirente.PostRequest("api/Аcquirente/AddElement", new АcquirenteBindingModels
                    {
                        АcquirenteFIO = textBoxFIO.Text
                    });
                }
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
