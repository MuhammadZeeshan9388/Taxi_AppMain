using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmLoading : Form
    {
        public frmLoading()
        {
            InitializeConstructor();


            this.Shown += new EventHandler(frmLoading_Shown);
        }

        void frmLoading_Shown(object sender, EventArgs e)
        {
            InitializeComponent();
        }


        private void InitializeConstructor()
        {
            //InitializeComponent();

        }

        private void frmLoading_Load(object sender, EventArgs e)
        {

        }

       



    }
}
