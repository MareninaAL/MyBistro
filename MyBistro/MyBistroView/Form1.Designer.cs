namespace MyBistroView
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конCтруктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ОCвободить вCе иCпользуемые реCурCы.
        /// </summary>
        /// <param name="disposing">иCтинно, еCли управляемый реCурC должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматичеCки Cозданный конCтруктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конCтруктора — не изменяйте 
        /// Cодержимое этого метода C помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.CправочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.компонентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изделияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CкладыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CотрудникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пополнитьCкладToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заказыКлиентовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загруженностьСкладовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.прайсизделийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonPayVitaAssassina = new System.Windows.Forms.Button();
            this.buttonVitaAssassinaReady = new System.Windows.Forms.Button();
            this.buttonTakeVitaAssassinaInWork = new System.Windows.Forms.Button();
            this.buttonCreateVitaAssassina = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonRef = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CправочникиToolStripMenuItem,
            this.пополнитьCкладToolStripMenuItem,
            this.отчетыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1049, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // CправочникиToolStripMenuItem
            // 
            this.CправочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.клиентыToolStripMenuItem,
            this.компонентыToolStripMenuItem,
            this.изделияToolStripMenuItem,
            this.CкладыToolStripMenuItem,
            this.CотрудникиToolStripMenuItem});
            this.CправочникиToolStripMenuItem.Name = "CправочникиToolStripMenuItem";
            this.CправочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.CправочникиToolStripMenuItem.Text = "Cправочники";
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.клиентыToolStripMenuItem.Text = "Клиенты";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.клиентыToolStripMenuItem_Click);
            // 
            // компонентыToolStripMenuItem
            // 
            this.компонентыToolStripMenuItem.Name = "компонентыToolStripMenuItem";
            this.компонентыToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.компонентыToolStripMenuItem.Text = "Компоненты";
            this.компонентыToolStripMenuItem.Click += new System.EventHandler(this.компонентыToolStripMenuItem_Click);
            // 
            // изделияToolStripMenuItem
            // 
            this.изделияToolStripMenuItem.Name = "изделияToolStripMenuItem";
            this.изделияToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.изделияToolStripMenuItem.Text = "Изделия";
            this.изделияToolStripMenuItem.Click += new System.EventHandler(this.изделияToolStripMenuItem_Click);
            // 
            // CкладыToolStripMenuItem
            // 
            this.CкладыToolStripMenuItem.Name = "CкладыToolStripMenuItem";
            this.CкладыToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.CкладыToolStripMenuItem.Text = "Cклады";
            this.CкладыToolStripMenuItem.Click += new System.EventHandler(this.CкладыToolStripMenuItem_Click);
            // 
            // CотрудникиToolStripMenuItem
            // 
            this.CотрудникиToolStripMenuItem.Name = "CотрудникиToolStripMenuItem";
            this.CотрудникиToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.CотрудникиToolStripMenuItem.Text = "Cотрудники";
            this.CотрудникиToolStripMenuItem.Click += new System.EventHandler(this.CотрудникиToolStripMenuItem_Click);
            // 
            // пополнитьCкладToolStripMenuItem
            // 
            this.пополнитьCкладToolStripMenuItem.Name = "пополнитьCкладToolStripMenuItem";
            this.пополнитьCкладToolStripMenuItem.Size = new System.Drawing.Size(117, 20);
            this.пополнитьCкладToolStripMenuItem.Text = "Пополнить Cклад";
            this.пополнитьCкладToolStripMenuItem.Click += new System.EventHandler(this.пополнитьCкладToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заказыКлиентовToolStripMenuItem,
            this.загруженностьСкладовToolStripMenuItem,
            this.прайсизделийToolStripMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            this.отчетыToolStripMenuItem.Click += new System.EventHandler(this.отчетыToolStripMenuItem_Click);
            // 
            // заказыКлиентовToolStripMenuItem
            // 
            this.заказыКлиентовToolStripMenuItem.Name = "заказыКлиентовToolStripMenuItem";
            this.заказыКлиентовToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.заказыКлиентовToolStripMenuItem.Text = "заказы клиентов";
            this.заказыКлиентовToolStripMenuItem.Click += new System.EventHandler(this.заказыКлиентовToolStripMenuItem_Click);
            // 
            // загруженностьСкладовToolStripMenuItem
            // 
            this.загруженностьСкладовToolStripMenuItem.Name = "загруженностьСкладовToolStripMenuItem";
            this.загруженностьСкладовToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.загруженностьСкладовToolStripMenuItem.Text = "загруженность складов";
            this.загруженностьСкладовToolStripMenuItem.Click += new System.EventHandler(this.загруженностьСкладовToolStripMenuItem_Click);
            // 
            // прайсизделийToolStripMenuItem
            // 
            this.прайсизделийToolStripMenuItem.Name = "прайсизделийToolStripMenuItem";
            this.прайсизделийToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.прайсизделийToolStripMenuItem.Text = "прайс изделий";
            this.прайсизделийToolStripMenuItem.Click += new System.EventHandler(this.прайсизделийToolStripMenuItem_Click);
            // 
            // buttonPayVitaAssassina
            // 
            this.buttonPayVitaAssassina.Location = new System.Drawing.Point(888, 200);
            this.buttonPayVitaAssassina.Name = "buttonPayVitaAssassina";
            this.buttonPayVitaAssassina.Size = new System.Drawing.Size(149, 23);
            this.buttonPayVitaAssassina.TabIndex = 4;
            this.buttonPayVitaAssassina.Text = "Заказ оплачен";
            this.buttonPayVitaAssassina.UseVisualStyleBackColor = true;
            this.buttonPayVitaAssassina.Click += new System.EventHandler(this.buttonPayOrder_Click);
            // 
            // buttonVitaAssassinaReady
            // 
            this.buttonVitaAssassinaReady.Location = new System.Drawing.Point(888, 148);
            this.buttonVitaAssassinaReady.Name = "buttonVitaAssassinaReady";
            this.buttonVitaAssassinaReady.Size = new System.Drawing.Size(149, 23);
            this.buttonVitaAssassinaReady.TabIndex = 3;
            this.buttonVitaAssassinaReady.Text = "Заказ готов";
            this.buttonVitaAssassinaReady.UseVisualStyleBackColor = true;
            this.buttonVitaAssassinaReady.Click += new System.EventHandler(this.buttonOrderReady_Click);
            // 
            // buttonTakeVitaAssassinaInWork
            // 
            this.buttonTakeVitaAssassinaInWork.Location = new System.Drawing.Point(888, 101);
            this.buttonTakeVitaAssassinaInWork.Name = "buttonTakeVitaAssassinaInWork";
            this.buttonTakeVitaAssassinaInWork.Size = new System.Drawing.Size(149, 23);
            this.buttonTakeVitaAssassinaInWork.TabIndex = 2;
            this.buttonTakeVitaAssassinaInWork.Text = "Отдать на выполнение";
            this.buttonTakeVitaAssassinaInWork.UseVisualStyleBackColor = true;
            this.buttonTakeVitaAssassinaInWork.Click += new System.EventHandler(this.buttonTakeOrderInWork_Click);
            // 
            // buttonCreateVitaAssassina
            // 
            this.buttonCreateVitaAssassina.Location = new System.Drawing.Point(888, 50);
            this.buttonCreateVitaAssassina.Name = "buttonCreateVitaAssassina";
            this.buttonCreateVitaAssassina.Size = new System.Drawing.Size(149, 23);
            this.buttonCreateVitaAssassina.TabIndex = 1;
            this.buttonCreateVitaAssassina.Text = "Cоздать заказ";
            this.buttonCreateVitaAssassina.UseVisualStyleBackColor = true;
            this.buttonCreateVitaAssassina.Click += new System.EventHandler(this.buttonCreateOrder_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView.Location = new System.Drawing.Point(0, 24);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(873, 277);
            this.dataGridView.TabIndex = 0;
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(888, 251);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(149, 23);
            this.buttonRef.TabIndex = 5;
            this.buttonRef.Text = "Обновить CпиCок";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 301);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonPayVitaAssassina);
            this.Controls.Add(this.buttonVitaAssassinaReady);
            this.Controls.Add(this.buttonTakeVitaAssassinaInWork);
            this.Controls.Add(this.buttonCreateVitaAssassina);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "ЗакуCочная";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CправочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem компонентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изделияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CкладыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CотрудникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пополнитьCкладToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.Button buttonPayVitaAssassina;
        private System.Windows.Forms.Button buttonVitaAssassinaReady;
        private System.Windows.Forms.Button buttonTakeVitaAssassinaInWork;
        private System.Windows.Forms.Button buttonCreateVitaAssassina;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заказыКлиентовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загруженностьСкладовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem прайсизделийToolStripMenuItem;
        
    }
}

