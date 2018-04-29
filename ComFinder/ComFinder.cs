using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ComFinder
{

    public class ComFinder
    {
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem settingsMenuItem;
        private System.ComponentModel.IContainer components;
        private SettingsForm frmSettings;


        [STAThread]
        static void Main()
        {
            ComFinder c = new ComFinder();
            Application.Run();
        }

        public ComFinder()
        {
            string puttyTimer;
            string path;
            string puttyParams;
            AppConfiguration.loadDefualtConfiguration(out path, out puttyParams, out puttyTimer);

            Timer timer = new Timer();
            if (puttyTimer == "")
            {
                MessageBox.Show("no Timer set");
                timer.Interval = (1000); // default timer 1 sec
            }
            else
            {
                MessageBox.Show("Timer Interval is"+ puttyTimer);
                timer.Interval = (Int32.Parse(puttyTimer)); // refresh as timer in settings
            }
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            this.components = new System.ComponentModel.Container();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.settingsMenuItem = new System.Windows.Forms.MenuItem();

            // Initialize contextMenu1
            // add com ports
          /*  foreach (string s in SerialPort.GetPortNames())
            {
                MenuItem m = new MenuItem();
                m.Text = s;
                m.Click += new System.EventHandler(this.ComPort_DoubleClick);
                m.Tag = s; /* save the com port as the tag 
                this.contextMenu1.MenuItems.Add(m);
            }
        */
            this.contextMenu1.MenuItems.Add("-");
            // Initialize menuItem1
            this.settingsMenuItem.Text = "&Settings";
            this.settingsMenuItem.Click += new System.EventHandler(this.contextSettings_Click);

            this.contextMenu1.MenuItems.Add(settingsMenuItem);

            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.contextExit_Click);

            this.contextMenu1.MenuItems.Add(exitMenuItem);


            // Create the NotifyIcon.
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);

            notifyIcon1.Icon = new Icon("appicon.ico");

            notifyIcon1.ContextMenu = this.contextMenu1;

            notifyIcon1.Text = "COM Port Finder, Version: " + typeof(ComFinder).Assembly.GetName().Version;
            notifyIcon1.Visible = true;

            // Handle the DoubleClick event to activate the form.
            notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            
        }


        private void ComPort_DoubleClick(object Sender, EventArgs e)
        {
            string path;
            string puttyParams;
            string puttyTimer;
            AppConfiguration.loadDefualtConfiguration(out path, out puttyParams,out puttyTimer);
            string com = (string)((MenuItem)Sender).Tag;
            if (path != "")
            {
                if(puttyParams!="")
                {
                    puttyParams = " -sercfg " + puttyParams;
                }
                System.Diagnostics.Process.Start(path, "-serial " +com + puttyParams);
            }
            
        }

        private void notifyIcon1_DoubleClick(object Sender, EventArgs e)
        {
            //// Show the form when the user double clicks on the notify icon.

            //// Set the WindowState to normal if the form is minimized.
            //if (this.WindowState == FormWindowState.Minimized)
            //    this.WindowState = FormWindowState.Normal;

            //// Activate the form.
            //this.Activate();
        }

        private void contextExit_Click(object Sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            notifyIcon1.Dispose();
            Application.Exit();
        }

        private void contextSettings_Click(object Sender, EventArgs e)
        {
            if(frmSettings == null )
            {
                frmSettings = new SettingsForm();
            }
            if(!frmSettings.Visible)
            {
                frmSettings.ShowDialog();

            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            
            foreach (string s in SerialPort.GetPortNames())
            {
                MenuItem m = new MenuItem();
                m.Text = s;
                m.Click += new System.EventHandler(this.ComPort_DoubleClick);
                m.Tag = s; /* save the com port as the tag */
                this.contextMenu1.MenuItems.Add(m);
            }
        }
    }
}
