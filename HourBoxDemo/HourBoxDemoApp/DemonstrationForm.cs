using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HourBoxDemoApp
{
    public partial class DemonstrationForm : Form
    {
        public DemonstrationForm()
        {
            InitializeComponent();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => System.Diagnostics.Process.Start("https://github.com/mooshmore");

        private async void Form1_Load(object sender, EventArgs e)
        {
            hourBox1.TimeSpan = DateTime.Now.TimeOfDay;
            
            await Task.Delay(14);
            hourBox1.Focus();
        }

        private void hourBox1_TextChanged(object sender, EventArgs e)
        {
            label8.Text = hourBox1.Value;
            label9.Text = hourBox1.TimeSpan.ToString();
        }
    }
}
