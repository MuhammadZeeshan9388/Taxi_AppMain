using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Taxi_AppMain.Forms
{
    public partial class frmLoadingScreen : Form
    {
        public frmLoadingScreen()
        {
            //InitializeComponent();
            this.Load += new EventHandler(frmLoadingScreen_Load);
            //System.Threading.Thread loginThread = new System.Threading.Thread(new ThreadStart(LoadImage));
            //loginThread.IsBackground = true;
            //loginThread.Start();
         //   LoadImage();
            this.Shown += new EventHandler(frmLoadingScreen_Shown);
            InitializeComponent();
        }

        void frmLoadingScreen_Shown(object sender, EventArgs e)
        {
          //  InitializeComponent();
        }

        void frmLoadingScreen_Load(object sender, EventArgs e)
        {
          //  this.BringToFront();
       
         
        }


        private void LoadImage()
        {
            if (this.InvokeRequired)
            {
                DisplayProgressBar d = new DisplayProgressBar(Loading);
                this.BeginInvoke(d);
            }
            else
            {
                Loading();

            }


        }
        delegate void DisplayProgressBar();

        private void Loading()
        {

          //  pictureBox1.Load(@"D:\Images\logos\chiswickcars.png");
//

        }
    }
}
