using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using System.IO;
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;

namespace Taxi_AppMain
{
    public partial class rptfrmDriverLog : UI.SetupBase
    {
        int Id = 0;
        public rptfrmDriverLog(int DriverId)
        {
            InitializeComponent();
            this.Id = DriverId;
            this.Load += new EventHandler(rptfrmDriverLog_Load);
            this.KeyDown += new KeyEventHandler(rptfrmDriverLog_KeyDown);
        }

        void rptfrmDriverLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        void rptfrmDriverLog_Load(object sender, EventArgs e)
        {
            ShowDriverLog();    
        }

        private void ShowDriverLog()
        {
            try
            {
                var list = (from a in General.GetQueryable<Fleet_Driver_Log>(c => c.DriverId == Id)
                            orderby a.UpdateDate descending
                            select new
                            {
                                a.BeforeUpdate,
                                a.AfterUpdate,
                                a.User,
                                a.UpdateDate
                            }).ToList();
                grdDriverLog.DataSource = list;
                grdDriverLog.Columns["BeforeUpdate"].Width = 250;
                grdDriverLog.Columns["AfterUpdate"].Width = 250;
                grdDriverLog.Columns["User"].Width = 140;
                grdDriverLog.Columns["UpdateDate"].Width = 140;

                grdDriverLog.Columns["BeforeUpdate"].HeaderText = "Before Update";
                grdDriverLog.Columns["AfterUpdate"].HeaderText = "After Update";
                //grdDriverLog.Columns["User"].Width = 140;
                grdDriverLog.Columns["UpdateDate"].HeaderText = "Update Date";

                (grdDriverLog.Columns["UpdateDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm ss";
                (grdDriverLog.Columns["UpdateDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm ss}";
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
         
    }
}
