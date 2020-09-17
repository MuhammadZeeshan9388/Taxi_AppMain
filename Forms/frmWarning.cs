using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace Taxi_AppMain
{
    public partial class frmWarning : Form
    {
        private string ReminderValue;
        public frmWarning()
        {
            InitializeComponent();

          
            this.Shown += new EventHandler(frmReminder_Shown);

          //  txtHeader.Text = "PDA LISTENER IP is not defined in Settings";
        }


    

        void frmReminder_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }

        private void ShowReminder()
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Diagnostics.Process.GetProcessesByName("MapApp").Count() == 0)
                {
                    try
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        if (Environment.OSVersion.Version.Major >= 6)
                        {
                           p.StartInfo.Verb = "runas";
                        }

                        //if (System.Diagnostics.Debugger.IsAttached)
                        //{
                        //      p.StartInfo.FileName =@"C:\Program Files (x86)\Eurosoft Tech\MapAppSetup\MapApp.exe";
                            
                        //}
                      //  else
                       // {

                            p.StartInfo.FileName = Application.StartupPath.Replace("Treasure Cab System", "MapAppSetup") + "\\MapApp.exe";
                       // }
                     
                        p.StartInfo.UseShellExecute = true;
                      
                        p.Start();

                       // System.Diagnostics.Process.Start(Application.StartupPath.Replace("Treasure Cab System", "MapAppSetup") + "\\MapApp.exe");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            Close();
        }

       
    }
}
