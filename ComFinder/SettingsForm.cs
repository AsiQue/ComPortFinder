using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComFinder
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnFindPuttyPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Find Putty.exe location...";
            ofd.Filter = "putty.exe|putty.exe";

            if(ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtPuttyPath.Text = ofd.FileName;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            /* load current settings */
            string path, puttyConf;
            AppConfiguration.loadDefualtConfiguration(out path, out puttyConf);
            txtPuttyPath.Text = path;
            txtPuttyParams.Text = puttyConf;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppConfiguration.saveConfiguration(txtPuttyPath.Text, txtPuttyParams.Text);
            MessageBox.Show("Configuration Saved");
            this.Close();
        }
    }
}
