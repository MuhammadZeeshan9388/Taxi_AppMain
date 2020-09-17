using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Taxi_AppMain
{
    public partial class frmAddRemDescription : Form
    {

        private bool _saved;

        public bool Saved
        {
            get { return _saved; }
            set { _saved = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        public frmAddRemDescription()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmAddRemDescription_Load);
        }

        void frmAddRemDescription_Load(object sender, EventArgs e)
        {
            txtRemovalDescription.Text = this.Description;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Saved = true;

                this.Description = txtRemovalDescription.Text;

                this.Close();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
    }
}
