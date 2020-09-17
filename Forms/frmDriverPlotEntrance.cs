using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_BLL;
using DAL;
using Utils;
using Taxi_Model;
using Telerik.WinControls.Enumerations;
using System.Data.Linq;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmDriverPlotEntrance : UI.SetupBase
    {
        DriverPlotEntranceBO objDriverPlotEntrance;
        public frmDriverPlotEntrance()
        {
            InitializeComponent();
            objDriverPlotEntrance = new DriverPlotEntranceBO();
            this.SetProperties((INavigation)objDriverPlotEntrance);
            this.Load += new EventHandler(frmDriverPlotEntrance_Load);
            this.ddlDriver.SelectedValueChanged += new EventHandler(ddlDriver_SelectedValueChanged);
        }

        void ddlDriver_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int DriverId = ddlDriver.SelectedValue.ToInt();
                if (DriverId > 0)
                {
                    var query = General.GetObject<Fleet_DriverPlotEntrance>(c => c.Driverid == DriverId);
                    if (query != null)
                    {
                        dtpFromTime.Value = query.Fromdatetime;
                        dtpTillTime.Value = query.Tilldatetime;
                    }
                    else
                    {
                        dtpTillTime.Value = null;
                        dtpFromTime.Value = null;
                    }
                }
            }
            catch
            { }
        }

        void frmDriverPlotEntrance_Load(object sender, EventArgs e)
        {
            FillDriver();
            DefaultDate();
        }
        private void DefaultDate()
        {
            dtpFromTime.Value = null;
            dtpTillTime.Value = null;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void FillDriver()
        {
            ComboFunctions.FillDriverNoCombo(ddlDriver);
        }

        public override void Save()
        {
            try
            {
                string Error = string.Empty;
                int DriverId = ddlDriver.SelectedValue.ToInt();

                if (DriverId == 0)
                {
                    Error = "Required : Driver";
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                var query = General.GetObject<Fleet_DriverPlotEntrance>(c => c.Driverid == DriverId);
                if (query != null)
                {
                    objDriverPlotEntrance.GetByPrimaryKey(query.Id);
                    objDriverPlotEntrance.Edit();                
                }
                else
                {
                    objDriverPlotEntrance.New();
                }
                objDriverPlotEntrance.Current.Driverid = DriverId;
                objDriverPlotEntrance.Current.Fromdatetime = dtpFromTime.Value;
                objDriverPlotEntrance.Current.Tilldatetime = dtpTillTime.Value;
                objDriverPlotEntrance.Save();
                this.Close();
            }
            catch (Exception ex)
            {
                if (objDriverPlotEntrance.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objDriverPlotEntrance.ShowErrors());
                }
                else
                {

                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
