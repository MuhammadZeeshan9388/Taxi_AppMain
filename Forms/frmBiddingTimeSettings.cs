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
using UI;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls.Data;
using DAL;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmBiddingTimeSettings : UI.SetupBase
    {
        SysPolicyBO objMaster;
        public frmBiddingTimeSettings()
        {
            InitializeComponent();
            objMaster = new SysPolicyBO();
            this.SetProperties((INavigation)objMaster);
            this.Load += new EventHandler(frmBiddingTimeSettings_Load);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void DefaultDate()
        {
            dtpStartFrom.Value = DateTime.Now;
            dtpTill.Value = DateTime.Now;
        }
        void frmBiddingTimeSettings_Load(object sender, EventArgs e)
        {
            DefaultDate();
            Gen_SysPolicy obj = General.GetQueryable<Gen_SysPolicy>(null).FirstOrDefault();
            if (obj != null)
            {
                objMaster.GetByPrimaryKey(obj.Id);
                objMaster.Edit();
                DisplayRecord();
            }
        }
        public override void Save()
        {
            try
            {
                string StartFrom = string.Empty;
                string Till = string.Empty;
                DateTime? dtFrom = null;
                DateTime? dtTill = null;
                Gen_SysPolicy obj = General.GetQueryable<Gen_SysPolicy>(null).FirstOrDefault();
                if (obj != null)
                {
                    objMaster.GetByPrimaryKey(obj.Id);
                    objMaster.Edit();
                }


                dtFrom = dtpStartFrom.Value.ToDateTimeorNull();
                dtFrom = dtFrom.Value.Date + new TimeSpan(dtFrom.Value.Hour, dtFrom.Value.Minute, 0);


                dtTill = dtpTill.Value.ToDateTimeorNull();
                dtTill = dtTill.Value.Date+ new TimeSpan(dtTill.Value.Hour,dtTill.Value.Minute,0);


                if (objMaster.Current.Gen_SysPolicy_BiddingTimeSettings.Count == 0)
                     objMaster.Current.Gen_SysPolicy_BiddingTimeSettings.Add(new Gen_SysPolicy_BiddingTimeSetting());
                  
              
                    objMaster.Current.Gen_SysPolicy_BiddingTimeSettings[0].SysPolicyId = objMaster.Current.Id;
                    objMaster.Current.Gen_SysPolicy_BiddingTimeSettings[0].StartFrom = dtFrom;
                    objMaster.Current.Gen_SysPolicy_BiddingTimeSettings[0].TillTo = dtTill;
                    objMaster.Current.Gen_SysPolicy_BiddingTimeSettings[0].AnyTime = chkAnyTime.Checked.ToBool();
               
                objMaster.Save();
              



            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }

        private void chkAnyTime_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ShowAnyTime(chkAnyTime.Checked.ToBool());
        }
        private void ShowAnyTime(bool AnyTime)
        {
            if (AnyTime == true)
            {
                dtpStartFrom.Value = DateTime.Now.ToDate();
                dtpTill.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);


                dtpStartFrom.Enabled = false;
                dtpTill.Enabled = false;
            }
            else
            {
                dtpStartFrom.Enabled = true;
                dtpTill.Enabled = true;
            }
        
        }

        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;

            if (objMaster.Current.Gen_SysPolicy_BiddingTimeSettings.Count > 0)
            {

                chkAnyTime.Checked = objMaster.Current.Gen_SysPolicy_BiddingTimeSettings[0].AnyTime.ToBool();
                dtpStartFrom.Value = objMaster.Current.Gen_SysPolicy_BiddingTimeSettings[0].StartFrom;
                dtpTill.Value = objMaster.Current.Gen_SysPolicy_BiddingTimeSettings[0].TillTo;
                ShowAnyTime(chkAnyTime.Checked.ToBool());
            }
        }

       
    }
}
