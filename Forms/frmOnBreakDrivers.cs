using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace Taxi_AppMain.Forms
{
    public partial class frmOnBreakDrivers : BaseForm
    {
        public frmOnBreakDrivers()
        {
            InitializeComponent();
        }
        private void frmOnBreakDrivers_Load(object sender, EventArgs e)
        {
            LoadOnBreakDriver();

            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowGroupPanel = false;
            grdLister.AutoCellFormatting = false;
            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;

            grdLister.Columns["ID"].IsVisible = false;
            grdLister.Columns["Driver"].Width = 300;
            grdLister.Columns["BreakTime"].Width = 150;

        }
        private void LoadOnBreakDriver()
        {
            try
            {
                var data1 = General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK).AsEnumerable();
                var query = (from a in data1
                             select new
                             {
                                 ID = a.Id,
                                 Driver = a.Fleet_Driver.DriverNo + "-" +  a.Fleet_Driver.DriverName,
                                 BreakTime = a.OnBreakDateTime,

                             }).OrderByDescending(c => c.BreakTime);

                grdLister.DataSource = query.ToList();

                
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

       
    }
}