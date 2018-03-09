namespace MyBistroView
{
    partial class FormPutOnRefrigerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelCount = new System.Windows.Forms.Label();
            this.comboBoxConstituent = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.labelConstituent = new System.Windows.Forms.Label();
            this.comboBoxRefrigerator = new System.Windows.Forms.ComboBox();
            this.labelRefrigerator = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(12, 63);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 13);
            this.labelCount.TabIndex = 4;
            this.labelCount.Text = "КоличеCтво:";
            // 
            // comboBoxConstituent
            // 
            this.comboBoxConstituent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConstituent.FormattingEnabled = true;
            this.comboBoxConstituent.Location = new System.Drawing.Point(87, 33);
            this.comboBoxConstituent.Name = "comboBoxConstituent";
            this.comboBoxConstituent.Size = new System.Drawing.Size(217, 21);
            this.comboBoxConstituent.TabIndex = 3;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(218, 86);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(137, 86);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Cохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(87, 60);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(217, 20);
            this.textBoxCount.TabIndex = 5;
            // 
            // labelConstituent
            // 
            this.labelConstituent.AutoSize = true;
            this.labelConstituent.Location = new System.Drawing.Point(12, 36);
            this.labelConstituent.Name = "labelConstituent";
            this.labelConstituent.Size = new System.Drawing.Size(66, 13);
            this.labelConstituent.TabIndex = 2;
            this.labelConstituent.Text = "Компонент:";
            // 
            // comboBoxRefrigerator
            // 
            this.comboBoxRefrigerator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRefrigerator.FormattingEnabled = true;
            this.comboBoxRefrigerator.Location = new System.Drawing.Point(87, 6);
            this.comboBoxRefrigerator.Name = "comboBoxRefrigerator";
            this.comboBoxRefrigerator.Size = new System.Drawing.Size(217, 21);
            this.comboBoxRefrigerator.TabIndex = 1;
            // 
            // labelRefrigerator
            // 
            this.labelRefrigerator.AutoSize = true;
            this.labelRefrigerator.Location = new System.Drawing.Point(12, 9);
            this.labelRefrigerator.Name = "labelRefrigerator";
            this.labelRefrigerator.Size = new System.Drawing.Size(41, 13);
            this.labelRefrigerator.TabIndex = 0;
            this.labelRefrigerator.Text = "холодильник:";
            // 
            // FormPutOnStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 121);
            this.Controls.Add(this.comboBoxRefrigerator);
            this.Controls.Add(this.labelRefrigerator);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.comboBoxConstituent);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.labelConstituent);
            this.Name = "FormPutOnStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пополнение Cклада";
            this.Load += new System.EventHandler(this.FormPutOnRefrigerator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.ComboBox comboBoxConstituent;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label labelConstituent;
        private System.Windows.Forms.ComboBox comboBoxRefrigerator;
        private System.Windows.Forms.Label labelRefrigerator;
    }
}