using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using System.Net;
using System.Xml.Linq;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;

namespace Taxi_AppMain
{
    public partial class frmAttributesFilter : UI.SetupBase
    {
        List<DriverList> drvList = new List<DriverList>();
        public frmAttributesFilter()
        {
            InitializeComponent();
            FillCombo();
            FormatGrid();
            this.Load += new EventHandler(frmDriverAttributes_Load);
            this.ddlAttributes.SelectedValueChanged += new EventHandler(ddlAttributes_SelectedValueChanged);
            this.KeyDown += new KeyEventHandler(frmDriverAttributes_KeyDown);
            this.optAvailableDrivers.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(optAvailableDrivers_ToggleStateChanged);
            this.optLoginDrivers.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(optLoginDrivers_ToggleStateChanged);
            this.optAllDrivers.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(optAllDrivers_ToggleStateChanged);
        }

        void optAllDrivers_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                FilterDriver();
            }
        }

        void optLoginDrivers_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                FilterDriver();
            }
        }

        void optAvailableDrivers_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                FilterDriver();
            }
        }

        void frmDriverAttributes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        void ddlAttributes_SelectedValueChanged(object sender, EventArgs e)
        {
            FilterDriver();   
        }

        private void FilterDriver()
        {
            try
            {
                string Attributes = ddlAttributes.SelectedValue.ToStr();
                if (string.IsNullOrEmpty(Attributes))
                {
                    grdLister.Rows.Clear();
                    return;

                }
                drvList.Clear();
                if (optLoginDrivers.IsChecked)
                {
                    var Loginlist = (from a in General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true)
                                     select new
                                     {
                                         DriverId = a.DriverId
                                     }).ToList();

                    var list = (from a in General.GetQueryable<Fleet_Driver>(c => c.AttributeValues.Contains(Attributes))
                                select new
                                {
                                    Id = a.Id,
                                    DriverNo = a.DriverNo,
                                    DriverName = a.DriverName,
                                    VehicleType = a.VehicleTypeId != null ? a.Fleet_VehicleType.VehicleType : ""

                                }).ToList();


                    foreach (var item in Loginlist)
                    {
                        var obj = list.Where(c => c.Id == item.DriverId).FirstOrDefault(); ;
                        if (obj != null)
                        {
                            drvList.Add(new DriverList { Id = obj.Id, DriverNo = obj.DriverNo, DriverName = obj.DriverName, VehicleType = obj.VehicleType });
                        }
                    }
                }
                else if (optAvailableDrivers.IsChecked)
                {
                    var Loginlist = (from a in General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE)
                                     select new
                                     {
                                         DriverId = a.DriverId
                                     }).ToList();

                    var list = (from a in General.GetQueryable<Fleet_Driver>(c => c.AttributeValues.Contains(Attributes))
                                select new
                                {
                                    Id = a.Id,
                                    DriverNo = a.DriverNo,
                                    DriverName = a.DriverName,
                                    VehicleType = a.VehicleTypeId != null ? a.Fleet_VehicleType.VehicleType : ""

                                }).ToList();


                    foreach (var item in Loginlist)
                    {
                        var obj = list.Where(c => c.Id == item.DriverId).FirstOrDefault(); ;
                        if (obj != null)
                        {
                            drvList.Add(new DriverList { Id = obj.Id, DriverNo = obj.DriverNo, DriverName = obj.DriverName, VehicleType = obj.VehicleType });
                        }
                    }
                }
                else
                {
                    var list = (from a in General.GetQueryable<Fleet_Driver>(c => c.AttributeValues.Contains(Attributes))
                                select new
                                {
                                    Id = a.Id,
                                    DriverNo = a.DriverNo,
                                    DriverName = a.DriverName,
                                    VehicleType = a.VehicleTypeId != null ? a.Fleet_VehicleType.VehicleType : ""

                                }).ToList();

                    foreach (var item in list)
                    {

                        drvList.Add(new DriverList { Id = item.Id, DriverNo = item.DriverNo, DriverName = item.DriverName, VehicleType = item.VehicleType });

                    }
                }

                grdLister.Rows.Clear();
                grdLister.RowCount = drvList.Count;
                for (int i = 0; i < drvList.Count; i++)
                {
                    grdLister.Rows[i].Cells[COLS.DriverNo].Value = drvList[i].DriverNo;
                    grdLister.Rows[i].Cells[COLS.DriverName].Value = drvList[i].DriverName;
                    grdLister.Rows[i].Cells[COLS.VehicleType].Value = drvList[i].VehicleType;
                }
                //grdLister.DataSource = drvList;
                //grdLister.Columns["Id"].IsVisible = false;
                //grdLister.Columns["DriverNo"].Width = 170;
                //grdLister.Columns["DriverName"].Width = 200;
                //grdLister.Columns["VehicleType"].Width = 200;
                //grdLister.Columns["DriverNo"].HeaderText = "Driver No";
                //grdLister.Columns["DriverName"].HeaderText = "Driver Name";
                //grdLister.Columns["VehicleType"].HeaderText = "Vehicle Type";
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public void FillCombo()
        {
            ComboFunctions.FillAttributeCombo(ddlAttributes);
        }
        void frmDriverAttributes_Load(object sender, EventArgs e)
        {

        }
        public struct COLS
        {
            public static string DriverNo = "DriverNo";
            public static string DriverName = "DriverName";
            public static string VehicleType = "VehicleType";
        }
        public void FormatGrid()
        {

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.DriverNo;
            col.HeaderText = "Driver No";
            col.Width = 170;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.DriverName;
            col.HeaderText = "Driver Name";
            col.Width = 200;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.VehicleType;
            col.HeaderText = "Vehicle Type";
            col.Width = 200;
            grdLister.Columns.Add(col);

        }
    }
    public class DriverList
    {
        public int Id { get; set; }
        public string DriverNo { get; set; }
        public string DriverName { get; set; }
        public string VehicleType { get; set; }
    }
    
}
