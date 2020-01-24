namespace RevitDevelopmentFoudation.CodeInTangsengjiewa.Test.UIs
{
    partial class ColumnTypesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.symbolCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "柱子族类型";
            // 
            // symbolCombo
            // 
            this.symbolCombo.FormattingEnabled = true;
            this.symbolCombo.Location = new System.Drawing.Point(136, 59);
            this.symbolCombo.Margin = new System.Windows.Forms.Padding(4);
            this.symbolCombo.Name = "symbolCombo";
            this.symbolCombo.Size = new System.Drawing.Size(343, 23);
            this.symbolCombo.TabIndex = 2;

            // 
            // ColumnTypesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 161);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.symbolCombo);
            this.Name = "ColumnTypesForm";
            this.Text = "ColumnTypesForm";
            this.Load += new System.EventHandler(this.ColumnTypesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox symbolCombo;
    }
}