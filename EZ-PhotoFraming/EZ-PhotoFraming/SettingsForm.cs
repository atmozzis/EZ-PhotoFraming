using System;
using System.Windows.Forms;

namespace EZ_PhotoFraming
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = panel1.BackColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                panel1.BackColor = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = label5.Font;
            fontDialog1.Color = label5.ForeColor;
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label5.Font = fontDialog1.Font;
                label5.ForeColor = fontDialog1.Color;
            }
        }
    }
}
