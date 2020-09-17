using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;

namespace Taxi_AppMain
{
    public partial class frmInformation : Form
    {
        private int _UniqueId;

        public frmInformation(int uniqueId, string caption,string contents)
        {
            InitializeComponent();

            this.txtTitle.Text = caption;
            this.txtDetails.Text = contents;
            this._UniqueId = uniqueId;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmInformation_KeyDown);


            this.Shown += new EventHandler(frmInformation_Shown);

        }

        void frmInformation_Shown(object sender, EventArgs e)
        {

          
            if(AppVars.listUserRights.Count(c => c.functionId == "UPDATE INFORMATION") > 0)
            {

                btnUpdate.Visible = true;
                txtDetails.ReadOnly = false;

            }

        }

        void frmInformation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDetails.Text.Trim().Length == 0)
            {
                MessageBox.Show("Required : Information");

            }
            else
            {

                try
                {

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_RunProcedure("update gen_company set CompanyInformation='" + txtDetails.Text.Trim() + "' where id=" + this._UniqueId);


                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }


        }
    }
}
