using System;
using System.Windows.Forms;

namespace CodeInTangsengjiewa.Test.UIs
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            this.Text = "唐僧解瓦工具箱";
            this.label1.Text = $"本工具为**内部工具\n旨在提高bim建模效率\n提升数字化建模水平。";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}